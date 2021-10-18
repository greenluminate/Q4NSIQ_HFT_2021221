using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IMovieHallRepository
    {
        void Create(MovieHall movieHall);
        void Delete(int id);
        MovieHall Read(int id);
        IQueryable<MovieHall> ReadAll();
        void Update(MovieHall movieHall);
    }
}