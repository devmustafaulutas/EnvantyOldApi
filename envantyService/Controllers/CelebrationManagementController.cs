using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CelebrationManagementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CelebrationManagementController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm kutlama kayıtlarını getirir.
        /// </summary>
        /// <returns>Tüm kutlamaların listesi.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCelebrations()
        {
            var celebrations = _context.CelebrationManagements.Where(d => d.Status == true).ToList();

            if (!celebrations.Any())
            {
                return NoContent(); // Veri yoksa 204 No Content döner
            }

            return Ok(celebrations); // Veri varsa 200 OK ile birlikte liste döner
        }
    }

}
