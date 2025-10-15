using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using envantyService.Models;
using envantyService;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllEventParticipantsbyEtkinlikId/{etkinlikId}")]
        public IActionResult GetAllEventParticipantsbyEtkinlikId(int etkinlikId)
        {
            List<EventParticipant> eventParticipants = _context.EventParticipants
                .Where(ep => ep.etkinlikId == etkinlikId)
                .ToList();

            if (!eventParticipants.Any())
            {
                return NotFound("Katılımcı bulunamadı.");
            }

            return Ok(eventParticipants);
        }

        [HttpGet("GetAllEventParticipants")]
        public IActionResult GetAllEventParticipants()
        {
            List<EventParticipant> eventParticipants = _context.EventParticipants.ToList();
            return Ok(eventParticipants);
        }
        [HttpPost("addParticipant")]
        public async Task<IActionResult> AddParticipant(string userMail, int eventId)
        {
            var participant = new EventParticipant
            {
                UserMail = userMail,
                CreationDate = DateTime.Now,
                etkinlikId = eventId
            };

            _context.EventParticipants.Add(participant);

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Participant added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

    }
}
