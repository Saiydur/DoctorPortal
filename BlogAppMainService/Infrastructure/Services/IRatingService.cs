using Infrastructure.BusinessObjects;

namespace Infrastructure.Services
{
    public interface IRatingService
    {
        void Add(Rating rating);
        void Delete(Rating rating);
        void Edit(Rating rating);
        Rating GetById(Guid id);
        IList<Rating> GetRatings();
    }
}