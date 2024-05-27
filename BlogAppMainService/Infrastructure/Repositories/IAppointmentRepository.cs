using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment,Guid>
    {
        public (IList<Appointment> data, int total, int totalDisplay) GetByDoctorId(Guid id, int pageIndex, int pageSize);
    }
}
