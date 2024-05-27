using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserBO = Infrastructure.BusinessObjects.User;
using UserEO = Infrastructure.Entities.User;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public UserService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void AddUser(UserBO user)
        {
            try
            {
                var emailCount=_applicationUnitOfWork.Users.GetCount(x => x.Email == user.Email);
                if (emailCount>0)
                    throw new DuplicateException("Email Already Taken");
                UserEO userEO = _mapper.Map<UserEO>(user);
                _applicationUnitOfWork.Users.Add(userEO);
                _applicationUnitOfWork.Save();
            }
            catch (Exception e)
            { 
                throw new Exception(e.Message);
            }
        }

        public IList<UserBO> GetUsers()
        {
            var UserEO = _applicationUnitOfWork.Users.GetAll();
            IList<UserBO> users= new List<UserBO>();
            foreach (var user in UserEO)
            {
                users.Add(_mapper.Map<UserBO>(user));
            }
            return users;
        }

        public UserBO Login(string email, string password)
        {
            var user = _applicationUnitOfWork.Users.Get(x => x.Email.Equals(email),"").FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.Password != password)
            {
                throw new Exception("Password is incorrect");
            }
            return _mapper.Map<UserBO>(user);
        }

        public UserBO GetUserById(Guid id)
        {
            var user = _applicationUnitOfWork.Users.GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserBO>(user);
        }

        public void EditUser(UserBO user,Guid id)
        {
            var userEO = _applicationUnitOfWork.Users.GetById(id);
            if (userEO == null)
                throw new InvalidOperationException("User Not Found");
            userEO = _mapper.Map(user, userEO);
            _applicationUnitOfWork.Save();
        }

        public void DeleteUser(Guid id)
        {
            _applicationUnitOfWork.Users.Remove(id);
            _applicationUnitOfWork.Save();
        }
    }
}
