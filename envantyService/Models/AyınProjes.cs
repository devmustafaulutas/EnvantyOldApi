namespace envantyService.Models
{
    public class AyınProjes
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string DocumentPath { get; set; }
        public string Konu { get; set; }
        public int TakipNumarisi { get; set; }
        public string? Icerik { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    
    }
}
