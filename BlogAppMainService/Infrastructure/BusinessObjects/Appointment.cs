using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string ConsultType { get; set; }
        public string ConsultDetails { get; set; }
        public Guid DoctorId { get; set; }
        public Guid UserId { get; set; }
    }
}
