using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly AppDbContext _context;
        public DashboardController(ILogger<DashboardController> logger, AppDbContext envantyContext)
        {
            _logger = logger;
            _context = envantyContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var today = DateTime.UtcNow.Date;
            var oneYearAgo = today.AddYears(-1);

            var totalVisitors = await _context.VisitorLogs.CountAsync();
            var todayVisitors = await _context.VisitorLogs.CountAsync(v => v.VisitTime >= today);
            var yearlyVisitors = await _context.VisitorLogs
                         .Where(v => v.VisitTime >= oneYearAgo)
                         .GroupBy(v => v.VisitorId) // Aynı kullanıcı tekrar sayılmasın
                         .CountAsync();
            var onlineUsers = await GetOnlineUsers(_context);
            var deviceStats = await _context.VisitorLogs
                .GroupBy(v => v.DeviceType)
                .Select(g => new { DeviceType = g.Key, Count = g.Count() })
                .ToListAsync();

            var totalTakipForm = await _context.TakipForms.CountAsync();
            var totalOMUS = await _context.Omus.CountAsync();
            var totalForm = totalTakipForm + totalOMUS;
            var thisYear = today.Year;
            var culture = new System.Globalization.CultureInfo("tr-TR");
            var monthsOfThisYear = Enumerable.Range(1, 12)
                .Select(i => new
                {
                    Year = thisYear,
                    MonthName = culture.DateTimeFormat.GetMonthName(i)
                }).ToList();

            // TakipForms için bu yılın aylık istatistikleri
            var takipFormCipStats = await _context.TakipForms
                .Where(t => t.CreateDate.Year == thisYear && t.KayitTipi == "Çalışan İletişim Platformu")
                .GroupBy(t => t.CreateDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();
            var takipFormAyinStats = await _context.TakipForms
                .Where(t => t.CreateDate.Year == thisYear && t.KayitTipi == "Ayın Projesi")
                .GroupBy(t => t.CreateDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();
            // OMUS için bu yılın aylık istatistikleri
            var omusStats = await _context.Omus
                .Where(o => o.CreateDate.Year == thisYear)
                .GroupBy(o => o.CreateDate.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();

            // Her ay için takip form ve OMUS verilerini eşleştirerek eksik ayları 0 ile doldur
            var monthlyStats = monthsOfThisYear.Select(m => new
            {
                Year = m.Year,
                MonthName = m.MonthName, // Ayın adı burada kullanılıyor
                TakipFormCipCount = takipFormCipStats.FirstOrDefault(t => culture.DateTimeFormat.GetMonthName(t.Month) == m.MonthName)?.Count ?? 0,
                TakipFormAyınCount = takipFormAyinStats.FirstOrDefault(t => culture.DateTimeFormat.GetMonthName(t.Month) == m.MonthName)?.Count ?? 0,
                OmusCount = omusStats.FirstOrDefault(o => culture.DateTimeFormat.GetMonthName(o.Month) == m.MonthName)?.Count ?? 0
            }).ToList();
            var monthlytotalStats = Enumerable.Range(1, 12)
            .Select(month => new
            {
                Year = thisYear,
                MonthName = culture.DateTimeFormat.GetMonthName(month), // Ayın adını al
                TotalCount =
                    (takipFormCipStats.FirstOrDefault(t => t.Month == month)?.Count ?? 0) +
                    (takipFormAyinStats.FirstOrDefault(t => t.Month == month)?.Count ?? 0) +
                    (omusStats.FirstOrDefault(o => o.Month == month)?.Count ?? 0)
            })
            .ToList();
            var Surveycount = _context.AnketYonetimi.Where(t=>t.Status == true).Count();
            var Corporatecount = _context.KampanyaYönetimis.Where(t => t.Status == true).Count();
            var Companynewscount = _context.SirkettenHaberlerYönetimis.Where(t => t.Status == true).Count();
            var Humanresourcescount = _context.Ceokösesis.Where(t => t.Status == true).Count();
            var Competitioncount = _context.CompetitionAdministrations.Where(t => t.Status == true).Count();
            var Eventcount = _context.Etkinliks.Where(t => t.Status == true).Count();
            var Celebrationcount = _context.CelebrationManagements.Where(t => t.Status == true).Count();
            var Announcementcount = _context.Duyuruyönetimis.Where(t => t.Status == true).Count();
            var Transportationcount = _context.UlasimYönetimis.Where(t => t.Status == true).Count();
            var Projectofthemonthcount = _context.AyınProjeKonus.Where(t => t.Status == true).Count();
            var Dailynewcount = _context.DailyNews.Where(t => t.Status == true).Count();

            return Ok(new
            {
                TotalVisitors = totalVisitors,
                TodayVisitors = todayVisitors,
                YearlyVisitors = yearlyVisitors,
                OnlineUsers = onlineUsers,
                DeviceStats = deviceStats,
                TotalTakipForm = totalTakipForm,
                TotalOMUS = totalOMUS,
                TotalForm = totalForm,
                MonthlyStats = monthlyStats,
                Surveycounts = Surveycount,
                Corporatecounts = Corporatecount,
                Companynewscounts = Companynewscount,
                Humanresourcescounts = Humanresourcescount,
                Competitioncounts = Competitioncount,
                Eventcounts = Eventcount,
                Celebrationcounts = Celebrationcount,
                Announcementcounts = Announcementcount,
                Transportationcounts = Transportationcount,
                Projectofthemonthcounts = Projectofthemonthcount,
                Dailynewcounts = Dailynewcount,
                MonthlytotalStats = monthlytotalStats
            });
        }

        private async Task<int> GetOnlineUsers(AppDbContext db)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-5); // Son 10 dakikada aktif olanları al
            return await db.VisitorLogs.CountAsync(v => v.LastActivity >= threshold);
        }

        [HttpGet("GetSurveyStats")]
        public async Task<IActionResult> GetSurveyStats()
        {
            var surveystats = await _context.AnketYonetimi.CountAsync();
            var surveyactivestats = await _context.AnketYonetimi.CountAsync(x => x.Status == true);
            var surveycompletestats = await _context.AnketYonetimi.CountAsync(x => x.Status == false);

            var userIds = await _context.SurveyAnswers
                .Select(sa => sa.UserId)
                .Distinct()
                .ToListAsync();

            var genders = await _context.Logins
                .Where(login => userIds.Contains(login.Email))
                .Select(login => login.Gender)
                .ToListAsync();

            int female = genders.Count(g => g == "Kadın");
            int male = genders.Count(g => g != "Kadın");
            int gendertotal = female + male;
            return Ok(new
            {
                surveyStats = surveystats,
                surveyActiveStats = surveyactivestats,
                surveyCompleteStats = surveycompletestats,
                Female = female,
                Male = male,
                genderTotal = gendertotal
            });
        }
    }
}
