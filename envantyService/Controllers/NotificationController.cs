using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using envantyService.Models;
using Microsoft.EntityFrameworkCore;
using Google;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public NotificationController(AppDbContext context)
        {
            // Firebase App zaten var mı kontrol et
            if (FirebaseApp.DefaultInstance == null)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                // Firebase Admin SDK'yı başlat
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(currentDirectory + @"\notification.json"),
                });
            }
            _context = context;
        }

        [HttpPost("send-test-notification")]
        public async Task<IActionResult> SendTestNotification([FromBody] TestNotificationRequest request)
        {
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = request.Title, // Başlık parametresi
                    Body = request.Body,   // İçerik parametresi
                },
                Token = request.Token, // Alıcının FCM token'ı
            };
            NotificationManagement nf = new NotificationManagement();
            nf.Title = message.Notification.Title;
            nf.Content = message.Notification.Body;
            nf.TakipFormId = request.TakipNo;
            nf.Topic = message.Topic;
            nf.CreateDate = DateTime.Now; // Tarihi otomatik olarak ayarla
            nf.Status = true; // Varsayılan değer
            nf.Token = message.Token;
            CreateNotification(nf);
            string response;
            try
            {
                // Bildirimi gönder
                response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return Ok(new { MessageId = response });
            }
            catch (FirebaseMessagingException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPost("send-topic-notification")]
        public async Task<IActionResult> SendTopicNotification([FromBody] TopicNotificationRequest request)
        {
            var message = new Message()
            {
                Topic = request.Topic, // Konu adı
                Notification = new Notification()
                {
                    Title = request.Notification.Title,
                    Body = request.Notification.Body,
                },
                Data = request.Data, // Ek veri
            };
            NotificationManagement nf = new NotificationManagement();
            nf.Title = message.Notification.Title;
            nf.Content = message.Notification.Body;
            nf.TakipFormId = 0;
            nf.Topic= message.Topic;
            nf.CreateDate = DateTime.Now; // Tarihi otomatik olarak ayarla
            nf.Status = true; // Varsayılan değer
            CreateNotification(nf);
            string response;
            try
            {
                // Bildirimi gönder
                response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                return Ok(new { MessageId = response });
            }
            catch (FirebaseMessagingException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
       
        private void CreateNotification(NotificationManagement notification)
        {
           
            _context.NotificationManagement.Add(notification);
            _context.SaveChangesAsync();

        }


    }


    public class TestNotificationRequest
    {
        public string Token { get; set; } // Alıcının FCM token'ı
        public string Title { get; set; } // Bildirim başlığı
        public string Body { get; set; }  // Bildirim içeriği

        public int TakipNo { get; set; }  // Bildirim içeriği
    }
    public class TopicNotificationRequest
    {
        public string Topic { get; set; }
        public NotificationPayload Notification { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }

    public class NotificationPayload
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
