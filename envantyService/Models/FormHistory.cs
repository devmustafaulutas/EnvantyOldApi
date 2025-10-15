namespace envantyService.Models
{
    public class FormHistory
    {
        public int Id { get; set; }
        public DateTime OlusturulmaZamanı { get; set; }
        public string ?AlanAdı { get; set; }
        public string ?AksiyonYapanKisi { get; set; }
        public string ?AksiyonYapanPozisyon { get; set; }
        public string ?EskiDeger { get; set; }
        public string ?YeniDeger { get; set; }
        public int TakipFormId { get; set; }
    }

}
