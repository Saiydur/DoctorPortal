using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class User : IEntity<Guid>
    {
        [Key]
        public Guid Id { get ; set ; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get ; set ; }

        [Required]
        [MaxLength(50)]
        public string LastName { get ; set ; }

        [Required]
        public string Email { get ; set ; }

        [Required]
        public string Password { get ; set ; }

        [Required]
        public string Role { get ; set ; }

        [Required]
        public bool IsActive { get ; set ; } = true;

        public ICollection<PaymentHistory> PaymentHistories { get; set; }

        public ICollection<Rating> PatientRating { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
