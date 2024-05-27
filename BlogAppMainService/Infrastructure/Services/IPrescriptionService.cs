using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IPrescriptionService
    {
        void Add(Prescription prescription);
        void Delete(Prescription prescription);
        void Edit(Prescription prescription);
        Prescription GetById(Guid id);
        IList<Prescription> GetPrescriptions();
    }
}