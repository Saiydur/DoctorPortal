using Autofac;
using MainServiceAPI.Models;

namespace MainServiceAPI
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            
            builder.RegisterType<LoginModel>().AsSelf();

            builder.RegisterType<RegistrationModel>().AsSelf();

            builder.RegisterType<CategoryCreateModel>().AsSelf();

            builder.RegisterType<CategoryUpdateModel>().AsSelf();

            builder.RegisterType<ConsultationModel>().AsSelf();

            builder.RegisterType<SpecialityModel>().AsSelf();

            builder.RegisterType<DoctorModel>().AsSelf();

            builder.RegisterType<RatingModel>().AsSelf();

            builder.RegisterType<AppointmentModel>().AsSelf();

            builder.RegisterType<PaymentModel>().AsSelf();

            base.Load(builder);
        }
    }
}
