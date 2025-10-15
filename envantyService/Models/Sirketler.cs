namespace envantyService.Models
{
    public class Sirketler
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string SGKSicilNo { get; set; }
        public string TehlikeSınıfı { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
