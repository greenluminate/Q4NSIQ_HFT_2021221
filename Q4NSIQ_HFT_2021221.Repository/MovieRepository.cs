using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<Movie> ReadByTitle(string title)
        {
            return dbSet.Where(movie => movie.MovieTitle.Equals(title)).AsQueryable();
        }
    }
}
