using System.ComponentModel.DataAnnotations;

namespace randevu_sistemi.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int DentistId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [StringLength(250)]
        public string? Note { get; set; }

        // Navigation properties
        public Dentist? Dentist { get; set; }
        public Patient? Patient { get; set; }
    }
}
