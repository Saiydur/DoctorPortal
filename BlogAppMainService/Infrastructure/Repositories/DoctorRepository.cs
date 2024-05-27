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
    public class DoctorRepository : Repository<Doctor, Guid>, IDoctorRepository
    {
        public DoctorRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
