namespace envantyService.Models
{
    public class Kampanya
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int IndirimOrani { get; set; }
        public int SirketlerId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime IsDeletedDate { get; set; }
        public bool Status { get; set; }
        public string? ImagePath { get; set; }
    }

}
