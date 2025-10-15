using envantyService.Models;
using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/sirketler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sirketler>>> GetSirketlers()
        {
            return await _context.Sirketlers.ToListAsync();
        }
    }
}
