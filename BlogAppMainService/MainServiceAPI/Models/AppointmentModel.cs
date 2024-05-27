using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class AppointmentModel
    {
        public Guid Id { get; set; }
        public string ConsultType { get; set; }
        public string ConsultDetails { get; set; }
        public string DoctorId { get; set; }
        public string UserId { get; set; }

        private IAppointmentService _appointmentService { get; set; }
        private IMapper _mapper { get; set; }

        public AppointmentModel()
        {
        }

        public AppointmentModel(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _appointmentService = scope.Resolve<IAppointmentService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void Add()
        {
            var appointmentBO = _mapper.Map<Appointment>(this);
            _appointmentService.Add(appointmentBO);
        }

        public void Remove()
        {
            var appointmentBO = _mapper.Map<Appointment>(this);
            _appointmentService.Delete(appointmentBO);
        }

        public void Update()
        {
            var appointmentBO = _mapper.Map<Appointment>(this);
            _appointmentService.Edit(appointmentBO);
        }

        public AppointmentModel GetAppointmentById(Guid id)
        {
            var appointmentBO = _appointmentService.GetById(id);
            var appointment = _mapper.Map<AppointmentModel>(appointmentBO);
            return appointment;
        }
        
        public IList<AppointmentModel> GetAppointments()
        {
            var appointmentBOs = _appointmentService.GetAppointments();
            var appointments = new List<AppointmentModel>();
            foreach(var  appointmentBO in appointmentBOs)
            {
                appointments.Add(_mapper.Map<AppointmentModel>(appointmentBO));
            }
            return appointments;
        }

        //public IList<AppointmentModel> GetAppointmentByDoctorId(Guid userId)
        //{
        //    var appointmentBOs = _appointmentService.GetAppointmentByDoctorId(userId);
        //    var appointments = new List<AppointmentModel>();
        //    foreach(var  appointmentBO in appointmentBOs)
        //    {
        //        appointments.Add(_mapper.Map<AppointmentModel>(appointmentBO));
        //    }
        //    return appointments;
        //}
    }
}
