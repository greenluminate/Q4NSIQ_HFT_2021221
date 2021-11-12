using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface IMovieHallLogic
    {
        void Create(MovieHall movieHall);
        void Delete(int id);
        MovieHall Read(int id);
        IEnumerable<MovieHall> ReadAll();
        void Update(MovieHall MovieHall);
    }
}