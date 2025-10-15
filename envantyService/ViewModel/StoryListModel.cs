namespace envantyService.ViewModel
{
    public class StoryListModel
    {
        public int StoryId { get; set; }
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public bool SeeStatus { get; set; }
        public DateTime ShareDate { get; set; }
    }
}
