namespace envantyService.Models
{
    public class VisitorLog
    {
        public int Id { get; set; }
        public string VisitorId { get; set; } // Artık IP yerine bunu kullanacağız
        public string UserAgent { get; set; }
        public string DeviceType { get; set; }
        public DateTime VisitTime { get; set; } = DateTime.UtcNow;
        public DateTime? LastActivity { get; set; }
    }
}
