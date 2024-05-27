using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Prescription : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Advice { get; set; }

        public Guid AppointmentId { get; set; }

        public Appointment Appointment { get; set; }
    }
}
