using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Doctor : IEntity<Guid>
    {
        [Key]
        public Guid Id { get ; set ; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string WorkingDetails { get; set; }

        [Required]
        public double TotalExperience { get; set; }

        [Required]
        public double PerConsultCharge { get; set; }

        [Required]
        public bool IsActiveNow { get; set; } = false;

        [Required]
        public string Role { get; set; } = "Doctor";

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get ; set ; }

        public ICollection<PaymentHistory> PaymentHistories { get; set; }

        public ICollection<Rating> PatientRating { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public Guid DoctorSpecialityId { get; set; }

        public Speciality Speciality { get; set; }

        public Guid ConsultationId { get; set; }

        public Consultation Consultation { get; set; }
    }
}
