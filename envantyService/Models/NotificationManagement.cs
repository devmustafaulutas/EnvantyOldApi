namespace envantyService.Models
{
    public class NotificationManagement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int TakipFormId { get; set; }
        public string ?Topic { get; set; }

        public string? Token { get; set; }
    }
}
