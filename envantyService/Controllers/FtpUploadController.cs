using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FtpUploadController : Controller
    {
        private readonly string ftpServer = "37.148.210.25";
        private readonly string ftpUsername = "administrator";
        private readonly string ftpPassword = "Ti4!Gq2#Wd0#Ld0#";

        [HttpPost("UploadFile")]
        public IActionResult UploadFile(IFormFile file, [FromQuery] string fileName, [FromQuery] string folderName)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name cannot be empty.");

            if (string.IsNullOrEmpty(folderName))
                return BadRequest("Folder name cannot be empty.");

            try
            {
                using (var ftpClient = new FtpClient(ftpServer, ftpUsername, ftpPassword))
                {
                    ftpClient.Connect();

                    // Hedef yolu oluştur

                    string remotePath = $"/{folderName}/{fileName}";

                    // Klasör yoksa oluştur
                    if (!ftpClient.DirectoryExists($"/{folderName}"))
                    {
                        ftpClient.CreateDirectory($"/{folderName}");
                    }

                    // Akış ile dosya yükleme
                    using (var stream = file.OpenReadStream())
                    {
                        ftpClient.UploadStream(stream, remotePath);
                    }

                    ftpClient.Disconnect();
                }

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
