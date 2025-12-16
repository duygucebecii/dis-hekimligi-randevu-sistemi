using System.ComponentModel.DataAnnotations;

namespace randevu_sistemi.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
