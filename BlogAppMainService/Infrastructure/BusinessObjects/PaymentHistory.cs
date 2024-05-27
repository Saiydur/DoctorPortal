using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class PaymentHistory
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
