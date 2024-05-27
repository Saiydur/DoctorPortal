using Infrastructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISpecialityService
    {
        public void AddSpeciality(Speciality speciality);

        public IList<Speciality> GetSpecialityLists();

        public Speciality GetSpecialityById(Guid id);

        public void UpdateSpeciality(Speciality speciality);

        public void DeleteSpeciality(Speciality speciality);
    }
}
