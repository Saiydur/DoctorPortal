using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        public ICategoryRepository Categories { get; }

        public IConsultationRepository Consultations { get; }

        public ISpecialityRepository Specialities { get; }

        public IDoctorRepository Doctors { get; }

        public IRatingRepository Ratings { get; }

        public IAppointmentRepository Appointments { get; }

        public IPrescriptionRepository Prescriptions { get; }

        public IPaymentHistoryRepository PaymentHistory { get; }
    }
}
