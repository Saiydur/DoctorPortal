using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class ConsultationModel
    {
        private IMapper _mapper;
        private IConsultationService _consultationService;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public ConsultationModel()
        {
            
        }

        public ConsultationModel(IConsultationService consultationService, IMapper mapper)
        {
            _consultationService = consultationService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _consultationService = scope.Resolve<IConsultationService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal void Add()
        {
            var consultationBO = _mapper.Map<Consultation>(this);
            _consultationService.Create(consultationBO);
        }

        internal IList<ConsultationModel> GetConsultations()
        {
            var consultationBOs = _consultationService.GetConsultations();
            var consultations = new List<ConsultationModel>();
            foreach(var cons in consultationBOs)
            {
                consultations.Add(_mapper.Map<ConsultationModel>(cons));
            }
            return consultations;
        }

        internal ConsultationModel GetConsultationById(Guid id)
        {
            var consultationBO = _consultationService.GetConsultationById(id);
            var consultation = _mapper.Map<ConsultationModel>(consultationBO);
            return consultation;
        }

        internal void UpdateConsultation()
        {
            var consultationBO = _mapper.Map<Consultation>(this);
            _consultationService.UpdateConsultation(consultationBO);
        }

        internal void DeleteConsultation()
        {
            var consultationBO = _mapper.Map<Consultation>(this);
            _consultationService.DeleteConsultation(consultationBO);
        }
    }
}
