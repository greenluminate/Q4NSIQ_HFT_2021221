using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
