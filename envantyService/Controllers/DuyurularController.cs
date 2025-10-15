using Microsoft.AspNetCore.Mvc;
using envantyService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuyurularController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DuyurularController(AppDbContext context)
        {
            _context = context;
        }

        //// GET: api/duyurular
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Duyuru>>> GetAllDuyurular()
        //{
        //    var duyurular = await _context.Duyuruyönetimis.ToListAsync();
        //    return Ok(duyurular);
        //}

        // GET: api/Duyurular/aktif
        [HttpGet("aktif")]
        public async Task<ActionResult<IEnumerable<Duyuru>>> GetAktifDuyurular()
        {
            var duyurular = await _context.Duyuruyönetimis.Where(d => d.Status == true).ToListAsync();
            return Ok(duyurular);
        }
    }
}
