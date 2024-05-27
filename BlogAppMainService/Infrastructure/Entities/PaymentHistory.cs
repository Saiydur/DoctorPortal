using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class PaymentHistory : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = string.Empty;

        public Guid AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; }
    }
}
