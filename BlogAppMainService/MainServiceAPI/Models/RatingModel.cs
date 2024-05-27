using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class RatingModel
    {
        private IRatingService _ratingService;
        private IMapper _mapper;

        public Guid Id { get; set; }

        public int Rate { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public Guid DoctorId { get; set; }

        public Guid UserId { get; set; }

        public RatingModel()
        {
            
        }

        public RatingModel(IRatingService ratingService, IMapper mapper)
        {
            _ratingService = ratingService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _ratingService = scope.Resolve<IRatingService>();
            _mapper = scope.Resolve<IMapper>();
        }

        public void Create()
        {
            var ratingBO = _mapper.Map<Rating>(this);
            _ratingService.Add(ratingBO);
        }

        public void Update()
        {
            var ratingBO = _mapper.Map<Rating>(this);
            _ratingService.Edit(ratingBO);
        }

        public void Delete()
        {
            var ratingBO = _mapper.Map<Rating>(this);
            _ratingService.Delete(ratingBO);
        }

        public IList<RatingModel> GetRatings()
        {
            var ratingBOs = _ratingService.GetRatings();
            var ratings = new List<RatingModel>();
            foreach (var ratingBO in ratingBOs)
            {
                ratings.Add(_mapper.Map<RatingModel>(ratingBO));
            }
            return ratings;
        }

        public RatingModel GetRating(Guid id)
        {
            var ratingBO = _ratingService.GetById(id);
            var rating = _mapper.Map<RatingModel>(ratingBO);
            return rating;
        }
    }
}
