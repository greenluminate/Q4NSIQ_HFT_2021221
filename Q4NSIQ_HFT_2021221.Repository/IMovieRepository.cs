using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IQueryable<Movie> ReadByTitle(string title);
    }
}