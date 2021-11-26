using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class MovieLogic : Logic<Movie>, IMovieLogic
    {
        IMovieRepository movieRepo;
        public MovieLogic(IMovieRepository movieRepo)
        : base(movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public IEnumerable<Movie> ReadByTitle(string title)
        {
            return movieRepo.ReadByTitle(title);
        }
    }
}
