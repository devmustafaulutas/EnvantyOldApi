namespace envantyService.Models
{
    public class RegistrationHistory
    {
        public int Id { get; set; }
        public DateTime OlusturulmaZamanı { get; set; }
        public string ?AksiyonAdı { get; set; }
        public string ?AksiyonYapanKisi { get; set; }
        public string ?AksiyonYapanPozisyon { get; set; }
        public string ?AksiyonYapanGrup { get; set; }
        public string ?SistemAciklaması { get; set; }
        public string ?Aciklama { get; set; }
        public int TakipFormId { get; set; }
    }

}
