using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class MovieRepository : IMovieRepository
    {
        CinemaDbContext db;

        public MovieRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public Movie Read(int id)
        {
            return db.Movies.FirstOrDefault(t => t.MovieId == id);
        }

        public IQueryable<Movie> ReadAll()
        {
            return db.Movies;
        }

        public void Update(Movie movie)
        {
            var oldMovie = Read(movie.MovieId);
            oldMovie.Languages = movie.Languages;
            oldMovie.MovieTitle = movie.MovieTitle;
            oldMovie.Rating = movie.Rating;
            oldMovie.Showtimes = movie.Showtimes;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
