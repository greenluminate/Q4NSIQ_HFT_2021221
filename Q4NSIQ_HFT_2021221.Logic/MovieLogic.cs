using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class MovieLogic
    {
        IMovieRepository movieRepo;

        public MovieLogic(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public void Create(Movie movie)
        {
            movieRepo.Create(movie);
        }

        public Movie Read(int id)
        {
            return movieRepo.Read(id);
        }

        public IEnumerable<Movie> ReadAll()
        {
            return movieRepo.ReadAll();
        }

        public void Update(Movie movie)
        {
            movieRepo.Update(movie);
        }

        public void Delete(int id)
        {
            movieRepo.Delete(id);
        }
    }
}
