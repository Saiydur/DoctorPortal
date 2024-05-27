using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class Rating
    {
        public Guid Id { get; set; }

        public int Rate { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public Guid DoctorId { get; set; }

        public Guid UserId { get; set; }
    }
}
