using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public string Advice { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
