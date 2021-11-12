using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IMovieHallRepository : IRepository<MovieHall>
    {
        IQueryable<MovieHall> ReadByCategory(string category);
    }
}