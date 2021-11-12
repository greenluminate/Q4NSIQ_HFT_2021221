using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class MovieHallRepository : Repository<MovieHall>, IMovieHallRepository
    {
        public MovieHallRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<MovieHall> ReadByCategory(string category)
        {
            return dbSet.Where(hall => hall.HallCategory.Equals(category)).AsQueryable();
        }
    }
}
