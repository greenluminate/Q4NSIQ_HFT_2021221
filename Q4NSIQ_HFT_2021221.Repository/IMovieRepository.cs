using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    interface IMovieRepository
    {
        void Create(Movie movie);
        void Delete(int id);
        Movie Read(int id);
        IQueryable<Movie> ReadAll();
        void Update(Movie movie);
    }
}