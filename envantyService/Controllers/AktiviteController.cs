using envantyefe.Entityframeworks;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AktiviteController : Controller
    {
        private readonly AppDbContext _context;

        public AktiviteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Activite> GetActivites()
        {
            return _context.Activites.ToList();
        }

    }
}
