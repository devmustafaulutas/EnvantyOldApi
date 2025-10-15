using Microsoft.AspNetCore.Mvc;
using global::envantyService.Models;
using System.Collections.Generic;
using System.Linq;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationManagementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationManagementController(AppDbContext context)
        {
            _context = context;
        }

        // Belirli bir konuya göre bildirimleri getirir
        [HttpGet("topic")]
        public ActionResult<IEnumerable<NotificationManagement>> GetNotifications(string topic)
        {
            var notifications = _context.NotificationManagement
                .Where(n => n.Topic == topic)
                .OrderByDescending(n => n.CreateDate)
                .ToList();

            return Ok(notifications);
        }

        // Belirli bir token'a göre bildirimleri getirir
        [HttpGet("token")]
        public ActionResult<IEnumerable<NotificationManagement>> GetNotificationsByToken(string token)
        {
            var notifications = _context.NotificationManagement
                .Where(n => n.Token == token)
                .OrderByDescending(n => n.CreateDate)
                .ToList();

            return Ok(notifications);
        }
    }
}
