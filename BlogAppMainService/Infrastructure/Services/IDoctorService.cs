using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IDoctorService
    {
        void Add(Doctor doctor);
        void Delete(Doctor doctor);
        void Edit(Doctor doctor);
        Doctor GetById(Guid id);
        IList<Doctor> GetDoctors();
        Doctor Login(string email, string password);
    }
}