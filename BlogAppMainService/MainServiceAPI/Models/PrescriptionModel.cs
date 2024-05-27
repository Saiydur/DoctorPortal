using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class PrescriptionModel
    {
        public Guid Id { get; set; }
        public string Advice { get; set; }
        public Guid AppointmentId { get; set; }

        private IPrescriptionService _prescriptionService { get; set; }
        private IMapper _mapper { get; set; }

        public PrescriptionModel()
        {
            
        }

        public PrescriptionModel(IPrescriptionService prescriptionService, IMapper mapper)
        {
            _prescriptionService = prescriptionService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _prescriptionService = scope.Resolve<IPrescriptionService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void Add()
        {
            var prescriptionBO = _mapper.Map<Prescription>(this);
            _prescriptionService.Add(prescriptionBO);
        }

        public void Remove()
        {
            var prescriptionBO = _mapper.Map<Prescription>(this);
            _prescriptionService.Delete(prescriptionBO);
        }

        public void Update()
        {
            var prescriptionBO = _mapper.Map<Prescription>(this);
            _prescriptionService.Edit(prescriptionBO);
        }

        public PrescriptionModel GetPrescriptionById(Guid id)
        {
            var prescriptionBO = _prescriptionService.GetById(id);
            var prescription = _mapper.Map<PrescriptionModel>(prescriptionBO);
            return prescription;
        }

        public IList<PrescriptionModel> GetPrescriptions()
        {
            var prescriptionsBO = _prescriptionService.GetPrescriptions();
            var prescriptions = _mapper.Map<IList<PrescriptionModel>>(prescriptionsBO);
            return prescriptions;
        }
    }
}
