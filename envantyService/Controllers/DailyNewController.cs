using envantyefe.Entityframeworks;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyNewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DailyNewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("daily-news")]
        public IActionResult GetDailyNews()
        {
            var today = DateTime.Today;
            var oneDayAgo = today.AddDays(-1);
            var oneWeekAgo = today.AddDays(-7);

            var dailyNews = _context.DailyNews
                .Where(news => news.Status == true &&
                              (((news.Type == "Doğum Günü" || news.Type == "Günün Haberi") && news.ShareDate >= oneDayAgo) ||
                               ((news.Type != "Doğum Günü" || news.Type != "Günün Haberi") && news.ShareDate >= oneWeekAgo)))
                .OrderByDescending(news => news.ShareDate)
                .Select(news => new DailyNews()
                {
                    Title = news.Title,
                    Content = news.Content,
                    Type = news.Type,
                    Status = news.Status,
                    ImagePath = news.ImagePath,
                    ShareDate = news.ShareDate
                })
                .ToList();

            return Ok(dailyNews);
        }

    }
}
