using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class MovieHallLogic
    {
        IMovieHallRepository movieHallRepo;

        public MovieHallLogic(IMovieHallRepository movieHallRepo)
        {
            this.movieHallRepo = movieHallRepo;
        }

        public void Create(MovieHall movieHall)
        {
            movieHallRepo.Create(movieHall);
        }

        public MovieHall Read(int id)
        {
            return movieHallRepo.Read(id);
        }

        public IEnumerable<MovieHall> ReadAll()
        {
            return movieHallRepo.ReadAll();
        }

        public void Update(MovieHall MovieHall)
        {
            movieHallRepo.Update(MovieHall);
        }

        public void Delete(int id)
        {
            movieHallRepo.Delete(id);
        }
    }
}
