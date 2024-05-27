using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        public void AddUser(User user);
        IList<User>? GetUsers();
        User Login(string email, string password);
        public User GetUserById(Guid id);
        public void EditUser(User user, Guid id);
        public void DeleteUser(Guid id);
    }
}