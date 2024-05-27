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
    public class ConsultationRepository : Repository<Consultation, Guid>, IConsultationRepository
    {
        public ConsultationRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
