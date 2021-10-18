using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    class SeatsRepository : ISeatsRepository
    {
        CinemaDbContext db;

        public SeatsRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(Seats seats)
        {
            db.Seats.Add(seats);
            db.SaveChanges();
        }

        public Seats Read(int id)
        {
            return db.Seats.FirstOrDefault(t => t.SeatId == id);
        }

        public IQueryable<Seats> ReadAll()
        {
            return db.Seats;
        }

        public void Update(Seats seats)
        {
            var oldSeats = Read(seats.SeatId);
            oldSeats.SeatCategory = seats.SeatCategory;
            oldSeats.MovieHallId = seats.MovieHallId;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
