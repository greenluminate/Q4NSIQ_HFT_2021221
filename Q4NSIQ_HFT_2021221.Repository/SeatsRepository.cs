using System.Linq;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class SeatsRepository : Repository<Seats>, ISeatsRepository
    {
        public SeatsRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<Seats> ReadByMovieHallId(int id)
        {
            return dbSet.Where(seat => seat.MovieHallId.Equals(id)).AsQueryable();
        }
    }
}
