using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SirkettenHaberlerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SirkettenHaberlerController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm duyuruları getirir.
        /// </summary>
        /// <returns>Tüm şirket duyuruları listesi.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllAnnouncements()
        {
            var announcements = _context.SirkettenHaberlerYönetimis.Where(d => d.Status == true).ToList();

            if (!announcements.Any())
            {
                return NoContent(); // Veri yoksa 204 No Content döner
            }

            return Ok(announcements); // Veri varsa 200 ile birlikte liste döner
        }
    }

}
