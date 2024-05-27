using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IAppointmentService
    {
        void Add(Appointment appointment);
        void Delete(Appointment appointment);
        void Edit(Appointment appointment);
        IList<Appointment> GetAppointments();
        Appointment GetById(Guid id);
        (IList<Appointment> data, int total, int totalDisplay) GetAppointmentByDoctorId(Guid doctorId, int pageIndex, int pageSize);
    }
}