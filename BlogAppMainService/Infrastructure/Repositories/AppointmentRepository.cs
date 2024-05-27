using Infrastructure.DbContexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : Repository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public (IList<Appointment> data,int total,int totalDisplay) GetByDoctorId(Guid id,int pageIndex,int pageSize)
        {
            (IList<Appointment> data, int total, int totalDisplay) results = GetDynamic(x => x.DoctorId.Equals(id),null,"Doctors,Users",pageIndex,pageSize,true);
            return results;
        }
    }
}
