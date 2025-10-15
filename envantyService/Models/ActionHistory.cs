namespace envantyService.Models
{
    public class ActionHistory
    {
        public int Id { get; set; }
        public DateTime? YapılmaTarihi { get; set; }
        public string? AksiyonYapanKişi { get; set; }
        public string? AksiyonAdı { get; set; }
        public string? AksiyonAçıklaması { get; set; }
        public int? TakipFormId { get; set; }
    }
}
