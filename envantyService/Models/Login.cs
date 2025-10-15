// Models/Login.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace envantyService.Models
{
    [Table("Logins")]
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string UserId { get; set; }

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; }

        // EF Core’a bu özelliğin aslında “Password” kolonuna karşılık geldiğini söyle
        [Required]
        [Column("Password")]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Surname { get; set; }

        public DateTime Birthdate { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime EmploymentStart { get; set; }
        public DateTime EmploymentEnd { get; set; }

        public int DepartmanId { get; set; }
        public int DepartmanCategoryId { get; set; }
        public int ÜnvanlarId { get; set; }
        public int SirketlerId { get; set; }

        public DateTime CreateDate { get; set; }
        public int RoleId { get; set; }

        public bool Status { get; set; }
        public bool FirstLogin { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [ForeignKey(nameof(SirketlerId))]
        public Sirketler Sirketler { get; set; }
    }
}
