using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class DoctorModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string WorkingDetails { get; set; }

        public double TotalExperience { get; set; }

        public double PerConsultCharge { get; set; }

        public bool IsActiveNow { get; set; } = false;

        public string Role { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid ConsultationId { get; set; }

        public Guid DoctorSpecialityId { get; set; }

        private IDoctorService _doctorService { get; set; }
        private IMapper _mapper { get; set; }

        public DoctorModel()
        {
            
        }

        public DoctorModel(IMapper mapper, IDoctorService doctorService)
        {
            _mapper = mapper;
            _doctorService = doctorService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _mapper = scope.Resolve<IMapper>();
            _doctorService = scope.Resolve<IDoctorService>();
        }

        public void Create()
        {
            var doctorBO = _mapper.Map<Doctor>(this);
            _doctorService.Add(doctorBO);
        }

        public void Update()
        {
            var doctorBO = _mapper.Map<Doctor>(this);
            _doctorService.Edit(doctorBO);
        }

        public void Delete()
        {
            var doctorBO = _mapper.Map<Doctor>(this);
            _doctorService.Delete(doctorBO);
        }

        public DoctorModel GetDoctor(Guid id)
        {
            var doctorBO = _doctorService.GetById(id);
            var doctor = _mapper.Map<DoctorModel>(doctorBO);
            return doctor;
        }

        public IList<DoctorModel> GetDoctors()
        {
            var doctorBOs = _doctorService.GetDoctors();
            var doctors = new List<DoctorModel>();
            foreach (var doctor in doctorBOs)
            {
                doctors.Add(_mapper.Map<DoctorModel>(doctor));
            }
            return doctors;
        }

        public DoctorModel Login()
        {
            var doctorBO = _doctorService.Login(Email, Password);
            var doctor = _mapper.Map<DoctorModel>(doctorBO);
            return doctor;
        }
    }
}
