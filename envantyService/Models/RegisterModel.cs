// src/Models/RegisterModel.cs
using System.ComponentModel.DataAnnotations;

namespace envantyService.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string PasswordHash { get; set; }
    }
}
