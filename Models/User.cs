using System.ComponentModel.DataAnnotations;

namespace randevu_sistemi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Projede hash yapabilirsin, şimdilik düz dursun

        public string? Role { get; set; } // "Admin", "Sekreter", "Doktor" vb.
    }
}
