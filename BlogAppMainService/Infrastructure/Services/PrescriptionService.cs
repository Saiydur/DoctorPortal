using AutoMapper;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrescriptionBO = Infrastructure.BusinessObjects.Prescription;
using PrescriptionEO = Infrastructure.Entities.Prescription;

namespace Infrastructure.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public PrescriptionService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Add(PrescriptionBO prescription)
        {
            var prescriptionEO = _mapper.Map<PrescriptionEO>(prescription);
            _applicationUnitOfWork.Prescriptions.Add(prescriptionEO);
            _applicationUnitOfWork.Save();
        }

        public void Edit(PrescriptionBO prescription)
        {
            var prescriptionEO = _applicationUnitOfWork.Prescriptions.GetById(prescription.Id);
            if (prescriptionEO == null)
                throw new InvalidOperationException("Prescription Not Found");
            prescriptionEO = _mapper.Map(prescription, prescriptionEO);
            _applicationUnitOfWork.Save();
        }

        public void Delete(PrescriptionBO prescription)
        {
            _applicationUnitOfWork.Prescriptions.Remove(prescription.Id);
            _applicationUnitOfWork.Save();
        }

        public PrescriptionBO GetById(Guid id)
        {
            var prescriptionEO = _applicationUnitOfWork.Prescriptions.GetById(id);
            if (prescriptionEO == null)
                throw new InvalidOperationException("Prescription Not Found");
            var prescriptionBO = _mapper.Map<PrescriptionBO>(prescriptionEO);
            return prescriptionBO;
        }

        public IList<PrescriptionBO> GetPrescriptions()
        {
            var prescriptionEOs = _applicationUnitOfWork.Prescriptions.GetAll();
            var prescriptionBOs = _mapper.Map<IList<PrescriptionBO>>(prescriptionEOs);
            return prescriptionBOs;
        }
    }
}
