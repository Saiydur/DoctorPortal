using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;
using Newtonsoft.Json;
using System.Text;

namespace MainServiceAPI.Models
{
    public class UserModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;

        public UserModel()
        {

        }

        public UserModel(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<User>? GetUsers()
        {
            return _userService?.GetUsers();
        }

        internal void Edit(Guid id)
        {
            var userBO = _mapper.Map<User>(this);
            _userService.EditUser(userBO, id);
        }

        internal void Add()
        {
            var userBO = _mapper.Map<User>(this);
            _userService.AddUser(userBO);
        }
    }
}
