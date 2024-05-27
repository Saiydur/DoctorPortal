using AutoMapper;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorEO = Infrastructure.Entities.Doctor;
using DoctorBO = Infrastructure.BusinessObjects.Doctor;
using Infrastructure.Exceptions;

namespace Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Add(DoctorBO doctor)
        {
            var doctorCount = _applicationUnitOfWork.Doctors.GetCount(x => x.Email == doctor.Email);
            if (doctorCount > 0)
                throw new DuplicateException("Email Already Taken");
            var doctorEO = _mapper.Map<DoctorEO>(doctor);
            _applicationUnitOfWork.Doctors.Add(doctorEO);
            _applicationUnitOfWork.Save();
        }

        public void Edit(DoctorBO doctor)
        {
            var doctorEO = _applicationUnitOfWork.Doctors.GetById(doctor.Id);
            if (doctorEO == null)
                throw new InvalidOperationException("Doctor Not Found");
            doctorEO = _mapper.Map(doctor, doctorEO);
            _applicationUnitOfWork.Save();
        }

        public void Delete(DoctorBO doctor)
        {
            _applicationUnitOfWork.Doctors.Remove(doctor.Id);
            _applicationUnitOfWork.Save();
        }

        public DoctorBO GetById(Guid id)
        {
            var doctorEO = _applicationUnitOfWork.Doctors.GetById(id);
            if (doctorEO == null)
                throw new InvalidOperationException("Doctor Not Found");
            var doctorBO = _mapper.Map<DoctorBO>(doctorEO);
            return doctorBO;
        }

        public IList<DoctorBO> GetDoctors()
        {
            var doctorEOs = _applicationUnitOfWork.Doctors.GetAll();
            var doctorBOs = new List<DoctorBO>();
            foreach (var doctorEO in doctorEOs)
            {
                doctorBOs.Add(_mapper.Map<DoctorBO>(doctorEO));
            }
            return doctorBOs;
        }

        public DoctorBO Login(string email, string password)
        {
            var doctorEO = _applicationUnitOfWork.Doctors.Get(x => x.Email.Equals(email) && x.Password.Equals(password),"");
            if (doctorEO == null)
                throw new InvalidOperationException("Invalid Email or Password");
            var doctorBO = _mapper.Map<DoctorBO>(doctorEO);
            return doctorBO;
        }
    }
}
