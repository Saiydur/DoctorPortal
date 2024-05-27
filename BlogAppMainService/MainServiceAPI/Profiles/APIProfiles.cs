using AutoMapper;
using Infrastructure.BusinessObjects;
using MainServiceAPI.Models;

namespace MainServiceAPI.Profiles
{
    public class APIProfiles : Profile
    {
        public APIProfiles()
        {
            CreateMap<UserModel, User>()
                .ReverseMap();

            CreateMap<RegistrationModel, User>()
                .ReverseMap();

            CreateMap<CategoryCreateModel, Category>()
                .ReverseMap();

            CreateMap<CategoryUpdateModel, Category>()
                .ReverseMap();

            CreateMap<ConsultationModel, Consultation>()
                .ReverseMap();

            CreateMap<SpecialityModel, Speciality>()
                .ReverseMap();

            CreateMap<DoctorModel, Doctor>()
                .ReverseMap();

            CreateMap<RatingModel, Rating>()
                .ReverseMap();

            CreateMap<AppointmentModel, Appointment>()
                .ReverseMap();

            CreateMap<PaymentModel, PaymentHistory>()
                .ReverseMap();

            CreateMap<PrescriptionModel, Prescription>()
                .ReverseMap();
        }
    }
}
