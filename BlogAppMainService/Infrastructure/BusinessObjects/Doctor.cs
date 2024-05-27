using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BusinessObjects
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string WorkingDetails { get; set; }

        public double TotalExperience { get; set; }

        public double PerConsultCharge { get; set; }

        public bool IsActiveNow { get; set; } = false;

        public string Role { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid ConsultationId { get; set; }

        public Guid DoctorSpecialityId { get; set; }
    }
}
