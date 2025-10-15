using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActionHistoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ActionHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionHistory>>> GetActionHistories()
        {
            return await _context.ActionHistory.ToListAsync();
        }

        // GET: api/ActionHistory/5
        [HttpGet("{TakipFormId}")]
        public async Task<ActionResult<IEnumerable<ActionHistory>>> GetActionHistory(int TakipFormId)
        {
            var actionHistories = await _context.ActionHistory
                                                .Where(x => x.TakipFormId == TakipFormId)
                                                .ToListAsync();

            if (actionHistories == null || !actionHistories.Any())
            {
                return NotFound();
            }

            return actionHistories;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActionHistory([FromBody] ActionHistory actionHistory)
        {
            if (actionHistory == null)
            {
                return BadRequest("Geçersiz istek.");
            }
            var Tf=_context.TakipForms.FirstOrDefault(x=>x.TakipNumarısı==actionHistory.TakipFormId);
            Tf.Status = "Açık";
            actionHistory.YapılmaTarihi = DateTime.Now; // Yapılma tarihini otomatik ayarlıyoruz

            try
            {
                
                _context.ActionHistory.Add(actionHistory);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateActionHistory), new { id = actionHistory.Id }, actionHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kayıt sırasında bir hata oluştu: {ex.Message}");
            }
        }


        private bool ActionHistoryExists(int TakipFormId)
        {
            return _context.ActionHistory.Any(e => e.TakipFormId == TakipFormId);
        }
    }
}
