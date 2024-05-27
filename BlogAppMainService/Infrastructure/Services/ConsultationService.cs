using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultationBO = Infrastructure.BusinessObjects.Consultation;
using ConsultationEO = Infrastructure.Entities.Consultation;

namespace Infrastructure.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public ConsultationService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Create(ConsultationBO consultation)
        {
            var consultationCount = _applicationUnitOfWork.Consultations.GetCount(x=>x.Name==consultation.Name);
            if (consultationCount > 0)
                throw new DuplicateException("Consultation Name Already Taken");
            var consultationEO = _mapper.Map<ConsultationEO>(consultation);
            _applicationUnitOfWork.Consultations.Add(consultationEO);
            _applicationUnitOfWork.Save();
        }

        public IList<ConsultationBO> GetConsultations()
        {
            var consultationEOs = _applicationUnitOfWork.Consultations.GetAll();
            var consultationBOs = new List<ConsultationBO>();
            foreach(var consultation in  consultationEOs)
            {
                consultationBOs.Add(_mapper.Map<ConsultationBO>(consultation));
            }
            return consultationBOs;
        }

        public ConsultationBO GetConsultationById(Guid consultationId)
        {
            var consultationEO = _applicationUnitOfWork.Consultations.GetById(consultationId);
            if (consultationEO == null)
                throw new InvalidOperationException("Consultation not found");
            var consultationBO = _mapper.Map<ConsultationBO>(consultationEO);
            return consultationBO;
        }

        public void UpdateConsultation(ConsultationBO consultationBO)
        {
            var consultationEO = _applicationUnitOfWork.Consultations.GetById(consultationBO.Id);
            if(consultationEO == null)
                throw new InvalidOperationException("Consultation not found");
            consultationEO = _mapper.Map(consultationBO, consultationEO);
            _applicationUnitOfWork.Save();
        }

        public void DeleteConsultation(ConsultationBO consultationBO)
        {
            _applicationUnitOfWork.Consultations.Remove(consultationBO.Id);
            _applicationUnitOfWork.Save();
        }
    }
}
