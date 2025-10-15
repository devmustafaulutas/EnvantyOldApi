

namespace envantyService.Entityframeworks
{
    public class StorySeeUser
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int StoryId { get; set; }
        public DateTime SeeDate { get; set; }
        public bool Status { get; set; }
    }
}
