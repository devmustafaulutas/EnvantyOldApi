namespace envantyService.Models
{
    public class Duyuru
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime ShareDate { get; set; }
        public string? ImagePath { get; set; }
        public bool Status { get; set; }
    }

}
