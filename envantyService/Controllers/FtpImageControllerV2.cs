using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using K4os.Compression.LZ4.Streams;
using System;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.IO.Compression;
using SixLabors.ImageSharp.Formats.Jpeg;
using Microsoft.Extensions.Caching.Memory;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FtpImageControllerV2 : ControllerBase
    {


        private readonly string ftpHost = "37.148.210.25";

        private readonly string ftpUser = "administrator";

        private readonly string ftpPassword = "Ti4!Gq2#Wd0#Ld0#";

        private readonly IMemoryCache _cache;

        public FtpImageControllerV2(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet("GetImageAsBinary/{folderName}/{fileName}")]
        public IActionResult GetImageAsBinary2(string folderName, string fileName)
        {
            string cacheKey = $"{folderName}/{fileName}";

            // Cache kontrolü
            if (_cache.TryGetValue(cacheKey, out string? base64Data))
            {
                return Ok(new
                {
                    FileName = fileName,
                    ContentType = "application/octet-stream",
                    Data = base64Data
                });
            }

            // İşleme ve cache'e ekleme
            byte[] fileBytes = DownloadFileFromFtp(folderName, fileName);
            fileBytes = ResizeAndCompressImage(fileBytes, 50);
            base64Data = Convert.ToBase64String(fileBytes);

            // Veriyi cache'e ekle
            _cache.Set(cacheKey, base64Data, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                Priority = CacheItemPriority.High
            });

            return Ok(new
            {
                FileName = fileName,
                ContentType = "application/octet-stream",
                Data = base64Data
            });
        }



        private byte[] ResizeAndCompressImage(byte[] imageBytes, int resizePercentage)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(imageBytes))
            {
                // Yüzde olarak verilen değere göre yeni boyutları hesapla
                int newWidth = image.Width * resizePercentage / 100;
                int newHeight = image.Height * resizePercentage / 100;

                // Resmi yeniden boyutlandır
                image.Mutate(x => x.Resize(newWidth, newHeight));

                // Sıkıştırma ayarları
                var encoder = new JpegEncoder()
                {
                    Quality = 95 // Kaliteyi optimize ederek dosya boyutunu küçült
                };

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, encoder);
                    return CompressWithGZip(ms.ToArray()); // GZip ile sıkıştır ve döndür
                }
            }
        }

        private byte[] CompressWithGZip(byte[] data)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gzipStream.Write(data, 0, data.Length);
                }
                return outputStream.ToArray();
            }
        }

        private byte[] DownloadFileFromFtp(string folderName, string fileName)
        {
            using (var client = new FtpClient(ftpHost, ftpUser, ftpPassword))
            {
                // Senkron bağlantı
                client.Connect();

                string remotePath = $"/{folderName}/{fileName}";
                byte[] fileBytes;

                // Senkron dosya indirme
                client.DownloadBytes(out fileBytes, remotePath);

                return fileBytes;
            }
        }

        private byte[] CompressFileLZ4(byte[] fileBytes)
        {
            using (var outputStream = new MemoryStream())
            using (var lz4Stream = LZ4Stream.Encode(outputStream))
            {
                lz4Stream.Write(fileBytes, 0, fileBytes.Length);
                lz4Stream.Flush();
                return outputStream.ToArray();
            }
        }


        [HttpPost("UploadBase64ToFtp")]
        public IActionResult UploadBase64ToFtp([FromForm] string folderName, [FromForm] string base64Data, [FromForm] string fileName)
        {
            if (string.IsNullOrEmpty(base64Data) || string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Geçersiz Base64 verisi veya dosya adı.");
            }

            try
            {
                // Base64 verisini byte dizisine dönüştür
                byte[] fileBytes = Convert.FromBase64String(base64Data);

                // FTP'ye yükleme yap
                UploadFileToFtp(folderName, fileName, fileBytes);

                return Ok(new
                {
                    Message = "Dosya başarıyla yüklendi.",
                    FileName = fileName,
                    FolderName = folderName
                });
            }
            catch (FormatException)
            {
                return BadRequest("Geçersiz Base64 formatı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya yüklenirken bir hata oluştu: {ex.Message}");
            }
        }

        private void UploadFileToFtp(string folderName, string fileName, byte[] fileBytes)
        {
            using (var client = new FtpClient(ftpHost, ftpUser, ftpPassword))
            {
                // Senkron bağlantı
                client.Connect();

                string remotePath = $"/{folderName}/{fileName}";

                // Klasör yoksa oluştur
                if (!client.DirectoryExists($"/{folderName}"))
                {
                    client.CreateDirectory($"/{folderName}");
                }

                // Dosyayı yükle
                using (var ms = new MemoryStream(fileBytes))
                {
                    client.UploadStream(ms, remotePath);
                }
            }
        }


    }
}
