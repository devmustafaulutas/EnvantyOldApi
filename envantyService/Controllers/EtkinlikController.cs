using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Tüm metotlar için kimlik doğrulama gerekti
    public class EtkinlikController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EtkinlikController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm etkinlik kayıtlarını getirir.
        /// </summary>
        /// <returns>Tüm etkinliklerin listesi.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllEtkinliks()
        {
            var etkinliks = _context.Etkinliks.Where(d => d.Status == true).ToList();

            if (!etkinliks.Any())
            {
                return NoContent(); // Veri yoksa 204 No Content döner
            }

            return Ok(etkinliks); // Veri varsa 200 OK ile birlikte liste döner
        }
    }

}
