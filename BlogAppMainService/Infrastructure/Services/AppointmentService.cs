using AutoMapper;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBO = Infrastructure.BusinessObjects.Appointment;
using AppointmentEO = Infrastructure.Entities.Appointment;

namespace Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Add(AppointmentBO appointment)
        {
            var appointmentEO = _mapper.Map<AppointmentEO>(appointment);
            _applicationUnitOfWork.Appointments.Add(appointmentEO);
            _applicationUnitOfWork.Save();
        }

        public void Edit(AppointmentBO appointment)
        {
            var appointmentEO = _applicationUnitOfWork.Appointments.GetById(appointment.Id);
            if (appointmentEO == null)
                throw new InvalidOperationException("Appointment Not Found");
            appointmentEO = _mapper.Map(appointment, appointmentEO);
            _applicationUnitOfWork.Save();
        }

        public void Delete(AppointmentBO appointment)
        {
            _applicationUnitOfWork.Appointments.Remove(appointment.Id);
            _applicationUnitOfWork.Save();
        }

        public AppointmentBO GetById(Guid id)
        {
            var appointmentEO = _applicationUnitOfWork.Appointments.GetById(id);
            if (appointmentEO == null)
                throw new InvalidOperationException("Appointment Not Found");
            var appointmentBO = _mapper.Map<AppointmentBO>(appointmentEO);
            return appointmentBO;
        }

        public IList<AppointmentBO> GetAppointments()
        {
            var appointmentEOs = _applicationUnitOfWork.Appointments.GetAll();
            var appointmentBOs = _mapper.Map<IList<AppointmentBO>>(appointmentEOs);
            return appointmentBOs;
        }

        public (IList<AppointmentBO> data, int total, int totalDisplay) GetAppointmentByDoctorId(Guid doctorId,int pageIndex,int pageSize)
        {
            (IList<AppointmentEO> data, int total, int totalDisplay) appointmentEOs = _applicationUnitOfWork.Appointments.GetByDoctorId(doctorId,pageIndex,pageSize);

            IList<AppointmentBO> appointments = new List<AppointmentBO>();
            foreach(var  appointmentEO in appointmentEOs.data) 
            { 
                appointments.Add(_mapper.Map<AppointmentBO>(appointmentEO));
            }
            return (appointments,appointmentEOs.total,appointmentEOs.totalDisplay);
        }
    }
}
