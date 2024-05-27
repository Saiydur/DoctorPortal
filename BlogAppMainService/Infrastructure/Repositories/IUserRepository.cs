using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User,Guid>
    {
        
    }
}