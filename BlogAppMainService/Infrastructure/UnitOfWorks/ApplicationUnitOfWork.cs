using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        //Repos
        public IUserRepository Users { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IConsultationRepository Consultations { get; private set; }
        public ISpecialityRepository Specialities { get; private set; }

        public IRatingRepository Ratings { get; private set; }

        public IDoctorRepository Doctors { get; private set; }

        public IAppointmentRepository Appointments { get; private set; }

        public IPrescriptionRepository Prescriptions { get; private set; }

        public IPaymentHistoryRepository PaymentHistory { get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            IUserRepository userRepository, ICategoryRepository categoryRepository,
            IConsultationRepository consultationRepository,ISpecialityRepository specialityRepository,
            IDoctorRepository doctorRepository, IRatingRepository ratingRepository,
            IAppointmentRepository appointmentRepository, IPrescriptionRepository prescriptionRepository,
            IPaymentHistoryRepository paymentHistoryRepository
            )
            : base((DbContext)dbContext)
        { 
            Users= userRepository;
            Categories = categoryRepository;
            Consultations = consultationRepository;
            Specialities = specialityRepository;
            Doctors = doctorRepository;
            Ratings = ratingRepository;
            Appointments = appointmentRepository;
            Prescriptions = prescriptionRepository;
            PaymentHistory = paymentHistoryRepository;
        }
    }
}
