using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBO = Infrastructure.BusinessObjects.User;
using UserEO = Infrastructure.Entities.User;
using CategoryBO = Infrastructure.BusinessObjects.Category;
using CategoryEO = Infrastructure.Entities.Category;
using ConsultationEO = Infrastructure.Entities.Consultation;
using ConsultationBO = Infrastructure.BusinessObjects.Consultation;
using SpecialityEO = Infrastructure.Entities.Speciality;
using SpecialityBO = Infrastructure.BusinessObjects.Speciality;
using DoctorEO = Infrastructure.Entities.Doctor;
using DoctorBO = Infrastructure.BusinessObjects.Doctor;
using RatingBO = Infrastructure.BusinessObjects.Rating;
using RatingEO = Infrastructure.Entities.Rating;
using AppointmentBO = Infrastructure.BusinessObjects.Appointment;
using AppointmentEO = Infrastructure.Entities.Appointment;
using PrescriptionBO = Infrastructure.BusinessObjects.Prescription;
using PrescriptionEO = Infrastructure.Entities.Prescription;
using PaymentHistoryBO = Infrastructure.BusinessObjects.PaymentHistory;
using PaymentHistoryEO = Infrastructure.Entities.PaymentHistory;

namespace Infrastructure.Profiles
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<UserBO, UserEO>()
                .ReverseMap();

            CreateMap<CategoryBO, CategoryEO>()
                .ReverseMap();

            CreateMap<ConsultationBO, ConsultationEO>()
                .ReverseMap();

            CreateMap<SpecialityBO, SpecialityEO>()
                .ReverseMap();

            CreateMap<DoctorBO, DoctorEO>()
                .ReverseMap();

            CreateMap<RatingBO, RatingEO>()
                .ReverseMap();

            CreateMap<AppointmentBO, AppointmentEO>()
                .ReverseMap();

            CreateMap<PrescriptionBO, PrescriptionEO>()
                .ReverseMap();

            CreateMap<PaymentHistoryBO, PaymentHistoryEO>()
                .ReverseMap();
        }
    }
}
