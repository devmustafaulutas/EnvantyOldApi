using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FtpFilesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FtpFilesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile file , string foldername)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya bulunamadı.");

            // FTP bilgileri (gerçek bilgileri buraya yaz)
            string ftpHost = "ftp://37.148.210.25:2121";
            string ftpUsername = "administrator";
            string ftpPassword = "Ti4!Gq2#Wd0#Ld0#";

            // FTP yolunu oluştur
            string targetPath = "/"+foldername+"/" + file.FileName;
            string ftpFullPath = ftpHost + targetPath;

            try
            {
                // FTP isteği oluştur
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFullPath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = false;

                // Dosyayı FTP'ye yükle
                using (var fileStream = file.OpenReadStream())
                using (var requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                }

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    return Ok(new
                    {
                        message = "Dosya başarıyla yüklendi.",
                        status = response.StatusDescription
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Dosya yüklenirken hata oluştu.",
                    detail = ex.Message
                });
            }
        }

        [HttpGet("Download")]
        public IActionResult DownloadFile(string foldername, string fileName)
        {
            // FTP sunucusuna bağlanmak için adres ve dosya yolu
            string ftpServer = "ftp://37.148.210.25:2121";  // FTP sunucu adresi
            string ftpFolder = foldername;                 // FTP klasörü
            string ftpFile = fileName;                     // İndirmek istenilen dosya adı

            // FTP URL’sini oluştur
            string ftpUri = $"{ftpServer}/{ftpFolder}/{ftpFile}";

            // FTP bağlantısı için kullanıcı adı ve şifre
            string ftpUsername = "administrator";
            string ftpPassword = "Ti4!Gq2#Wd0#Ld0#";

            try
            {
                // FTP bağlantısı için istek oluştur
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUri);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // FTP yanıtını al
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Dosya adını al
                    string filesName = Path.GetFileName(ftpFile);
                    var provider = new FileExtensionContentTypeProvider();
                    string contentType = "application/octet-stream"; // Varsayılan MIME tipi

                    // MIME tipi belirleme
                    if (!provider.TryGetContentType(filesName, out contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    // Yanıtı byte dizisi olarak al ve dosyayı döndür
                    using (MemoryStream ms = new MemoryStream())
                    {
                        responseStream.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
                        return File(fileBytes, contentType, filesName); // Dosyayı indirme için gönder
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj dön
                return BadRequest($"Dosya indirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
