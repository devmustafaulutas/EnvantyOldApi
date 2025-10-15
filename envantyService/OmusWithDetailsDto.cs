namespace envantyService
{
    public class OmusWithDetailsDto
    {
        public int Id { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? DepartmanName { get; set; }
        public string? EtkiAlani { get; set; } // Veritabanındaki 'EtkiAlanı' alanı
        public string? DocumentPath { get; set; } // Veritabanındaki 'DocumentPath' alanı
        public int? TakipNumarasi { get; set; } // Veritabanındaki 'TakipNumarisi' alanı
        public int? DepartmanId { get; set; } // Veritabanındaki 'DepartmanId' alanı
        public string? Planla_Acıklaması { get; set; }
        public string? Uygula_Acıklaması { get; set; }
        public string? Kontrol_Et_Acıklaması { get; set; }
        public string? Onlem_Al_Acıklaması { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; } // Veritabanındaki 'CreateDate' alanı
        public string? SorumluKisi { get; set; } // Veritabanındaki 'SorumluKisi' alanı
        public string? KapanKisi { get; set; }
        // Eksik Alanlar
        public int? omu_DepartmanId { get; set; } // Veritabanındaki 'omu_DepartmanId' alanı
        public int? IyilestirmetipiId { get; set; } // Veritabanındaki 'IyileştirmetipiId' alanı
        public string? Status { get; set; } // Veritabanındaki 'Status' alanı
        public int? DepartmanCategoryId { get; set; } // Veritabanındaki 'DepartmanCategoryId' alanı, nullable olabilir
        public string? Iyilestirme_Degerlendirmesi { get; set; } // Veritabanındaki 'Iyilestirme_Degerlendirmesi' alanı
        public DateTime? CloseDate { get; set; }
    }





}
