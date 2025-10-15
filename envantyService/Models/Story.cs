namespace envantyService.Entityframeworks
{
    public class Story
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public DateTime ShareDate { get; set; }
    }
}
