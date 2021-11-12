using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        CinemaDbContext db;

        public ShowtimeRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(Showtime showtime)
        {
            db.Showtimes.Add(showtime);
            db.SaveChanges();
        }

        public Showtime Read(int id)
        {
            return db.Showtimes.FirstOrDefault(t => t.ShowtimeId == id);
        }

        public IQueryable<Showtime> ReadAll()
        {
            return db.Showtimes;
        }

        public void Update(Showtime showtime)
        {
            var oldShowtime = Read(showtime.ShowtimeId);
            oldShowtime.Date = showtime.Date;
            oldShowtime.MovieId = showtime.MovieId;
            oldShowtime.MovieHallId = showtime.MovieHallId;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
