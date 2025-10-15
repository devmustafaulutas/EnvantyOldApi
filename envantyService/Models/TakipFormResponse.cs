namespace envantyService.Models
{
    public class TakipFormResponse
    {
        public int Id { get; set; }
        public int TakipNumarısı { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }

        public string Icerik { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string SorumluMail { get; set; }
        public int DepartmanCategoryId { get; set; }
        public string CevapIcerigi { get; set; }
        public DateTime? CevapTarihi { get; set; }
        public string? AyınProjeKonu { get; set; }
        public bool ShowMail { get; set; }
        public int? DepartmanId { get; set; }
    }

}
