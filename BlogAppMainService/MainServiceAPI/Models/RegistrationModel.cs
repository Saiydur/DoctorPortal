using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Exceptions;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class RegistrationModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;

        public RegistrationModel()
        {
            
        }

        public RegistrationModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void AddUser()
        {
            var user=_mapper.Map<User>(this);
           
            _userService?.AddUser(user);
        }
    }
}
