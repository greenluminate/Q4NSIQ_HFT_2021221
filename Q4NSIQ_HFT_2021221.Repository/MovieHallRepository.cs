using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    class MovieHallRepository
    {
        CinemaDbContext db;

        public MovieHallRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(MovieHall movieHall)
        {
            db.MovieHalls.Add(movieHall);
            db.SaveChanges();
        }

        public MovieHall Read(int id)
        {
            return db.MovieHalls.FirstOrDefault(t => t.MovieHallId == id);
        }

        public IQueryable<MovieHall> ReadAll()
        {
            return db.MovieHalls;
        }

        public void Update(MovieHall movieHall)
        {
            var oldMovieHall = Read(movieHall.MovieHallId);
            oldMovieHall.HallCategory = movieHall.HallCategory;
            oldMovieHall.NumberOfSeats = movieHall.NumberOfSeats;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
