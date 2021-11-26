using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class MovieHallLogic : Logic<MovieHall>, IMovieHallLogic
    {
        IMovieHallRepository movieHallRepo;
        public MovieHallLogic(IMovieHallRepository movieHallRepo)
        : base(movieHallRepo)
        {
            this.movieHallRepo = movieHallRepo;
        }

        public IEnumerable<MovieHall> ReadByCategory(string category)
        {
            return movieHallRepo.ReadByCategory(category);
        }
    }
}
