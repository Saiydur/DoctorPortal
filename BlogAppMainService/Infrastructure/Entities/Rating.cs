using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Rating : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        
        public int Rate { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public Guid DoctorId { get; set; }
        
        public Doctor Doctor { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
