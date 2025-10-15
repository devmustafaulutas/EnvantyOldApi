using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace EnvantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly string _filesRoot;
        private readonly ILogger<DocumentController> _logger;
        private readonly IMemoryCache _cache;

        public DocumentController(
            IWebHostEnvironment env,
            ILogger<DocumentController> logger,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;

            // 1) Önce WebRootPath'e bakalım
            var webRoot = env.WebRootPath;

            // 2) Eğer null veya boşsa, ContentRootPath + "wwwroot" kullan
            if (string.IsNullOrEmpty(webRoot))
            {
                webRoot = Path.Combine(env.ContentRootPath, "wwwroot");
                _logger.LogWarning("env.WebRootPath null geldi, fallback olarak ContentRootPath/wwwroot kullanıldı: {WebRoot}", webRoot);
            }

            // 3) Son olarak files klasörünü ekle
            _filesRoot = Path.Combine(webRoot, "files");
            _logger.LogInformation("Files root set to: {FilesRoot}", _filesRoot);
        }

        [HttpGet("ListFolders")]
        public IActionResult ListFolders()
        {
            try
            {
                if (!Directory.Exists(_filesRoot))
                    return NotFound($"Files klasörü bulunamadı: {_filesRoot}");

                var folders = Directory
                    .GetDirectories(_filesRoot)
                    .Select(Path.GetFileName)
                    .ToList();

                return Ok(folders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListFolders hatası");
                return StatusCode(500, $"Klasör listelenirken hata: {ex.Message}");
            }
        }

        [HttpGet("ListFiles/{folderName}")]
        public IActionResult ListFiles(string folderName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(folderName))
                    return BadRequest("Geçerli bir klasör adı giriniz.");

                var folderPath = Path.Combine(_filesRoot, folderName);
                if (!Directory.Exists(folderPath))
                    return NotFound($"Klasör bulunamadı: {folderPath}");

                var files = Directory
                    .GetFiles(folderPath)
                    .Select(Path.GetFileName)
                    .ToList();

                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListFiles hatası");
                return StatusCode(500, $"Dosya listelenirken hata: {ex.Message}");
            }
        }

        [HttpGet("GetDocumentAsBinary/{folderName}/{fileName}")]
        public IActionResult GetDocumentAsBinary(string folderName, string fileName)
        {
            var cacheKey = $"{folderName}/{fileName}";
            if (_cache.TryGetValue(cacheKey, out string base64Data))
            {
                return Ok(new { FileName = fileName, ContentType = "application/octet-stream", Data = base64Data });
            }

            try
            {
                var filePath = Path.Combine(_filesRoot, folderName, fileName);
                if (!System.IO.File.Exists(filePath))
                    return NotFound($"Dosya bulunamadı: {filePath}");

                var bytes = System.IO.File.ReadAllBytes(filePath);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                    contentType = "application/octet-stream";

                base64Data = Convert.ToBase64String(bytes);
                _cache.Set(cacheKey, base64Data, TimeSpan.FromMinutes(30));

                return Ok(new { FileName = fileName, ContentType = contentType, Data = base64Data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetDocumentAsBinary hatası");
                return StatusCode(500, $"Doküman indirilirken hata: {ex.Message}");
            }
        }

        [HttpPost("UploadBase64ToLocal")]
        public IActionResult UploadBase64ToLocal(
            [FromForm] string folderName,
            [FromForm] string fileName,
            [FromForm] string base64Data)
        {
            if (string.IsNullOrEmpty(folderName) ||
                string.IsNullOrEmpty(fileName) ||
                string.IsNullOrEmpty(base64Data))
            {
                return BadRequest("folderName, fileName veya base64Data eksik.");
            }

            try
            {
                var folderPath = Path.Combine(_filesRoot, folderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);
                var bytes = Convert.FromBase64String(base64Data);
                System.IO.File.WriteAllBytes(filePath, bytes);

                return Ok(new { Message = "Dosya yüklendi.", FileName = fileName, FolderName = folderName });
            }
            catch (FormatException)
            {
                return BadRequest("Geçersiz Base64 formatı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadBase64ToLocal hatası");
                return StatusCode(500, $"Dosya yüklenirken hata: {ex.Message}");
            }
        }
    }
}
