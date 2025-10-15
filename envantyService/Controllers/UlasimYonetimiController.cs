using Microsoft.AspNetCore.Mvc;

using envantyService.Models;
using System.Collections.Generic;
using System.Linq;
namespace envantyService.Controllers
{

    public class UlasimYonetimiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UlasimYonetimiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/GetAllUlasimList")]
        public IActionResult GetAllUlasimList()
        {
            List<UlasimYonetimi> ulasimYonetimleri = _context.UlasimYönetimis.ToList();
            return Ok(ulasimYonetimleri);
        }
       
    }

}
