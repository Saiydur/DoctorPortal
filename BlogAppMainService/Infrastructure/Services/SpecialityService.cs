using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecialityEO = Infrastructure.Entities.Speciality;
using SpecialityBO = Infrastructure.BusinessObjects.Speciality;
using Infrastructure.Exceptions;

namespace Infrastructure.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public SpecialityService(IApplicationUnitOfWork applicationUnitOfWork,IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void AddSpeciality(SpecialityBO speciality)
        {
            var specialityCount = _applicationUnitOfWork.Specialities.GetCount(x=>x.Name==speciality.Name);
            if (specialityCount > 0)
                throw new DuplicateException("Speciality Already Taken");
            var specialityEO = _mapper.Map<SpecialityEO>(speciality);
            _applicationUnitOfWork.Specialities.Add(specialityEO);
            _applicationUnitOfWork.Save();
        }

        public void DeleteSpeciality(SpecialityBO speciality)
        {
            var specialityExist = _applicationUnitOfWork.Specialities.GetById(speciality.Id);
            if (specialityExist == null)
                throw new InvalidOperationException("Speciality Not Found");
            _applicationUnitOfWork.Specialities.Remove(speciality.Id);
            _applicationUnitOfWork.Save();
        }

        public SpecialityBO GetSpecialityById(Guid id)
        {
            var specialityEO = _applicationUnitOfWork.Specialities.GetById(id);
            if (specialityEO == null)
                throw new InvalidOperationException("Speciality Not Found");
            var speciality = _mapper.Map<Speciality>(specialityEO);
            return speciality;
        }

        public IList<SpecialityBO> GetSpecialityLists()
        {
            var specialityEOs = _applicationUnitOfWork.Specialities.GetAll();
            var specialities = new List<SpecialityBO>();
            foreach(var speciality in specialityEOs)
            {
                specialities.Add(_mapper.Map<SpecialityBO>(speciality));
            }
            return specialities;
        }

        public void UpdateSpeciality(SpecialityBO speciality)
        {
            var specialityEO = _applicationUnitOfWork.Specialities.GetById(speciality.Id);
            if (specialityEO == null)
                throw new InvalidOperationException("Speciality Not Found");
            specialityEO = _mapper.Map(speciality, specialityEO);
            _applicationUnitOfWork.Save();
        }
    }
}
