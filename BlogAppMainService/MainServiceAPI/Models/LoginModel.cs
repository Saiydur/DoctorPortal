using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class LoginModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel()
        {

        }

        public LoginModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public User? Login(string email,string password)
        {
            return _userService?.Login(email, password);
        }
    }
}
