using System;
using System.Linq;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class ShowtimeRepository : Repository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<Showtime> ReadByDate(DateTime? date)
        {
            date = date is null ? DateTime.Today : date;

            return dbSet.Where(showtime => showtime.Date.Date.Equals(date.Value.Date)).OrderBy(showtime => showtime.Date).AsQueryable();
        }
    }
}
