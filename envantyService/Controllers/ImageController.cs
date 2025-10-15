using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("Download")]
        public async Task<IActionResult> DownloadImage(string folderName, string fileName)
        {
            string ftpHost = "ftp://37.148.210.25";
            string ftpUser = "administrator";
            string ftpPass = "Ti4!Gq2#Wd0#Ld0#";

            string ftpPath = $"{ftpHost}/{folderName}/{fileName}";

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpPath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.UseBinary = true;
                request.EnableSsl = false; // SSL kullanıyorsan true yap

                using (FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync())
                using (Stream ftpStream = response.GetResponseStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await ftpStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var mimeType = "application/octet-stream";
                    return File(memoryStream.ToArray(), mimeType, fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Dosya indirilemedi: {ex.Message}");
            }
        }
    }
}
