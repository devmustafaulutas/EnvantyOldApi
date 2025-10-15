using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CeokösesisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CeokösesisController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm CEO Köşesi kayıtlarını getirir.
        /// </summary>
        /// <returns>Tüm CEO köşesi kayıtlarının listesi.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllCeokösesis()
        {
            
            var records = _context.Ceokösesis
                      .Where(d => d.Status == true)
                      .ToList();

            if (records == null || !records.Any())
            {
                return NoContent(); // Veri yoksa 204 No Content döner
            }

            return Ok(records); // Veri varsa 200 OK ile liste döner

        }
    }

}
