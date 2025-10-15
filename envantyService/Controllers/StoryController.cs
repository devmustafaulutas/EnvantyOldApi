using envantyService.Entityframeworks;
using envantyService.ViewModel;
using FluentFTP;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        string ftpServer = "37.148.210.25";
        string ftpUsername = "administrator";
        string ftpPassword = "Ti4!Gq2#Wd0#Ld0#";
        private readonly AppDbContext _appDbContext;

        public StoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("UploadStory")]
        public IActionResult UploadStory(IFormFile file, [FromQuery] string fileName, [FromQuery] string folderName, [FromQuery] string UserId, [FromQuery] string Description, [FromQuery] int CompanyId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name cannot be empty.");

            if (string.IsNullOrEmpty(folderName))
                return BadRequest("Folder name cannot be empty.");

            var date = DateTime.Now;
            // Benzersiz dosya adı oluştur
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{fileName}";
            var remotePath = $"/{folderName}/{uniqueFileName}";

            FtpClient ftpClient = null;

            try
            {
                ftpClient = new FtpClient(ftpServer, ftpUsername, ftpPassword);
                ftpClient.Connect();

                if (!ftpClient.DirectoryExists($"/{folderName}"))
                {
                    ftpClient.CreateDirectory($"/{folderName}");
                }

                using (var stream = file.OpenReadStream())
                {
                    ftpClient.UploadStream(stream, remotePath);
                }

                var story = new Story()
                {
                    Description = Description,
                    ImagePath = uniqueFileName,
                    ShareDate = DateTime.Now,
                    Status = true,
                    UserId = UserId,
                    CompanyId = CompanyId
                };

                _appDbContext.Stories.Add(story);
                _appDbContext.SaveChanges();

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("GetStory")]
        public IActionResult GetStory([FromQuery] int StoryId, [FromQuery] string? UserId, [FromQuery] string? seeUserId)
        {
            if (StoryId <= 0 || string.IsNullOrEmpty(seeUserId))
                return BadRequest("Invalid parameters.");

            try
            {
                var story = _appDbContext.Stories.FirstOrDefault(x => x.Id == StoryId && x.UserId == UserId);

                if (story == null)
                    return NotFound("Story not found for this user.");

                var fileName = story.ImagePath;

                // CDN'deki dosyanın temel URL'si (senin ortamına göre değiştir)
                var cdnBaseUrl = "https://getinge.envanty.net/images/StoryImages/";

                var imageUrl = cdnBaseUrl + fileName;

                // Görüldü bilgisini kaydet
                var alreadySeen = _appDbContext.StorySeeUsers
                    .Any(x => x.StoryId == story.Id && x.UserId == seeUserId);

                if (!alreadySeen)
                {
                    var seestory = new StorySeeUser()
                    {
                        UserId = seeUserId,
                        SeeDate = DateTime.Now,
                        Status = true,
                        StoryId = story.Id
                    };
                    _appDbContext.StorySeeUsers.Add(seestory);
                    _appDbContext.SaveChanges();
                }

                string timeAgo = "";
                var diff = DateTime.Now - story.ShareDate;
                if (diff.TotalMinutes < 60)
                {
                    var minutes = (int)diff.TotalMinutes;
                    if (minutes < 1) minutes = 1;
                    timeAgo = $"{minutes} Dakika";
                }
                else
                {
                    var hours = (int)diff.TotalHours;
                    if (hours < 1) hours = 1;
                    timeAgo = $"{hours} Saat";
                }
                return Ok(new
                {
                    FileName = fileName,
                    Url = imageUrl,
                    SentAgo = timeAgo,
                    Description = story.Description,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // MIME tipi belirleme yardımcı metodu
        private string GetMimeType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return ext switch
            {
                ".mp4" => "video/mp4",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                ".wmv" => "video/x-ms-wmv",
                ".webm" => "video/webm",
                ".3gp" => "video/3gpp",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }

        [HttpGet("ListStory")]
        public IActionResult ListStory([FromQuery] int CompanyId, [FromQuery] string? UserId)
        {
            if (CompanyId <= 0)
                return BadRequest("Şirket ID geçersiz.");

            var list = (from s in _appDbContext.Stories
                        where s.CompanyId == CompanyId && s.Status == true
                        select new StoryListModel
                        {
                            StoryId = s.Id,
                            ShareDate = s.ShareDate,
                            UserId = s.UserId,
                            UserName = _appDbContext.Logins
                                .Where(l => l.UserId == s.UserId)
                                .Select(l => l.Name + " " + l.Surname)
                                .FirstOrDefault(),

                            SeeStatus = _appDbContext.StorySeeUsers
                                .Any(ss => ss.StoryId == s.Id && ss.UserId == UserId)
                        }).ToList();

            return Ok(list);
        }

        [HttpGet("StorySeeList")]
        public IActionResult StorySeeList([FromQuery] int StoryId)
        {
            if (StoryId <= 0) return BadRequest("Geçersiz Story ID.");

            var seeList = (from ss in _appDbContext.StorySeeUsers
                           where ss.StoryId == StoryId && ss.Status == true
                           select new StorySeeListModel
                           {
                               UserId = ss.UserId,
                               NameSurname = _appDbContext.Logins
                                   .Where(l => l.UserId == ss.UserId)
                                   .Select(l => l.Name + " " + l.Surname)
                                   .FirstOrDefault()
                           }).ToList();

            return Ok(seeList);
        }
    }
}
