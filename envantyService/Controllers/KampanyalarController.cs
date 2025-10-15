using Microsoft.AspNetCore.Mvc;
using envantyService.Models;
using Microsoft.EntityFrameworkCore;
namespace envantyService.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class KampanyalarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KampanyalarController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/kampanyalar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kampanya>>> GetAllKampanyalar()
        {
            try
            {
                var kampanyalar = await _context.KampanyaYönetimis.Where(d => d.Status == true).ToListAsync();
                return Ok(kampanyalar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Veri çekilirken hata oluştu: {ex.Message}");
            }
        }
    }

}
