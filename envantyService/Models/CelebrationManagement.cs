namespace envantyService.Models
{
    public class CelebrationManagement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SirketlerId { get; set; }
        public DateTime KutlamaTarihi { get; set; }
        public DateTime KutlamaSaati { get; set; }
        public bool MustJoinEvent { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; }
    }

}
