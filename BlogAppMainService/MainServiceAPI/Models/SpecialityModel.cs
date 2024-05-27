using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class SpecialityModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        private ISpecialityService _specialityService;
        private IMapper _mapper;

        public SpecialityModel()
        {
            
        }

        public SpecialityModel(ISpecialityService specialityService, IMapper mapper)
        {
            _specialityService = specialityService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _specialityService = scope.Resolve<ISpecialityService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void Add()
        {
            var specialityBO = _mapper.Map<Speciality>(this);
            _specialityService.AddSpeciality(specialityBO);
        }

        public void Remove(SpecialityModel model)
        {
            var specialityBO = _mapper.Map<Speciality>(model);
            _specialityService.DeleteSpeciality(specialityBO);
        }

        public void Update(SpecialityModel model)
        {
            var specialityBO = _mapper.Map<Speciality>(model);
            _specialityService.UpdateSpeciality(specialityBO);
        }

        public IList<SpecialityModel> GetSpecialities()
        {
            var specialityBOs = _specialityService.GetSpecialityLists();
            var specialities = new List<SpecialityModel>();
            foreach (var speciality in specialityBOs)
            {
                specialities.Add(_mapper.Map<SpecialityModel>(speciality));
            }
            return specialities;
        }

        public SpecialityModel GetSpeciality(Guid id)
        {
            var specialityBO = _specialityService.GetSpecialityById(id);
            var speciality = _mapper.Map<SpecialityModel>(specialityBO);
            return speciality;
        }
    }
}
