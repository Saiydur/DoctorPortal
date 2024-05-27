using Autofac;
using Infrastructure.DbContexts;
using Infrastructure.RabbitMQ;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
              .WithParameter("connectionString", _connectionString)
              .WithParameter("migrationAssemblyName", _migrationAssemblyName)
              .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssemblyName", _migrationAssemblyName)
               .InstancePerLifetimeScope();

            //Services
            builder.RegisterType<UserService>().As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConsultationService>().As<IConsultationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SpecialityService>().As<ISpecialityService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DoctorService>().As<IDoctorService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RatingService>().As<IRatingService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentService>().As<IAppointmentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PrescriptionService>().As<IPrescriptionService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentHistoryService>().As<IPaymentHistoryService>()
                .InstancePerLifetimeScope();

            //Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConsultationRepository>().As<IConsultationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SpecialityRepository>().As<ISpecialityRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RatingRepository>().As<IRatingRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppointmentRepository>().As<IAppointmentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PrescriptionRepository>().As<IPrescriptionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentHistoryRepository>().As<IPaymentHistoryRepository>()
                .InstancePerLifetimeScope();

            //Unit of Works
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            //Utils
            builder.RegisterType<RabitMQProducer>().As<IRabitMQProducer>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
