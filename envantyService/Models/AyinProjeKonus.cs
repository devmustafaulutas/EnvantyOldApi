namespace envantyService.Models
{
    public class AyinProjeKonus
    {
        public int Id { get; set; }
        public string Konu { get; set; }
        public bool Status { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
