using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IMovieRepository
    {
        IQueryable<Movie> ReadByTitle(string title);
    }
}