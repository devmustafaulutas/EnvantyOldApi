using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionAdministrationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompetitionAdministrationController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm yarışma kayıtlarını getirir.
        /// </summary>
        /// <returns>Tüm yarışma kayıtları listesi.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCompetitions()
        {
            var competitions = _context.CompetitionAdministrations.Where(d => d.Status == true).ToList();

            if (!competitions.Any())
            {
                return NoContent(); // Veri yoksa 204 No Content döner
            }

            return Ok(competitions); // Veri varsa 200 OK ile birlikte döner
        }
    }

}
