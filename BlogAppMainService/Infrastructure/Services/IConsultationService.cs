using Infrastructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IConsultationService
    {
        public void Create(Consultation consultation);

        public IList<Consultation> GetConsultations();

        public Consultation GetConsultationById(Guid id);

        public void UpdateConsultation(Consultation consultation);

        public void DeleteConsultation(Consultation consultation);
    }
}
