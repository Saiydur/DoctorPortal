using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Appointment : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ConsultType { get; set; }

        [Required]
        public string ConsultDetails { get; set; }

        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<PaymentHistory> PaymentHistories { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
