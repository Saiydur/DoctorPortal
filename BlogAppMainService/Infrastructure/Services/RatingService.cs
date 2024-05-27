using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatingBO = Infrastructure.BusinessObjects.Rating;
using RatingEO = Infrastructure.Entities.Rating;

namespace Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public void Add(RatingBO rating)
        {
            var ratingEO = _mapper.Map<RatingEO>(rating);
            _applicationUnitOfWork.Ratings.Add(ratingEO);
            _applicationUnitOfWork.Save();
        }

        public void Edit(RatingBO rating)
        {
            var ratingEO = _applicationUnitOfWork.Ratings.GetById(rating.Id);
            if (ratingEO == null)
                throw new InvalidOperationException("Rating Not Found");
            ratingEO = _mapper.Map(rating, ratingEO);
            _applicationUnitOfWork.Save();
        }

        public void Delete(RatingBO rating)
        {
            _applicationUnitOfWork.Ratings.Remove(rating.Id);
            _applicationUnitOfWork.Save();
        }

        public RatingBO GetById(Guid id)
        {
            var ratingEO = _applicationUnitOfWork.Ratings.GetById(id);
            if (ratingEO == null)
                throw new InvalidOperationException("Rating Not Found");
            var ratingBO = _mapper.Map<RatingBO>(ratingEO);
            return ratingBO;
        }

        public IList<RatingBO> GetRatings()
        {
            var ratingEOs = _applicationUnitOfWork.Ratings.GetAll();
            var ratingBOs = new List<RatingBO>();
            foreach (var ratingEO in ratingEOs)
            {
                ratingBOs.Add(_mapper.Map<RatingBO>(ratingEO));
            }
            return ratingBOs;
        }
    }
}
