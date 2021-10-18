using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Q4NSIQ_HFT_2021221.Data
{
    public class CinemaDbContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieHall> MovieHalls { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public CinemaDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CinemaDB.mdf;Integrated Security=True";
                builder.UseLazyLoadingProxies()
                       .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Showtime>(entity =>
            {
                entity
                .HasOne(showtime => showtime.Movie)
                .WithMany(movie => movie.Showtimes)
                .HasForeignKey(showtime => showtime.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasOne(showtime => showtime.MovieHall)
                .WithMany(movieHall => movieHall.Showtimes)
                .HasForeignKey(showtime => showtime.MovieHallId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity
                .HasOne(ticket => ticket.Showtime)
                .WithMany(showtime => showtime.Tickets)
                .HasForeignKey(ticket => ticket.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);
                entity
                .HasOne(ticket => ticket.Staff)
                .WithMany(staff => staff.Tickets)
                .HasForeignKey(ticket => ticket.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Seats>(entity =>
            {
                entity
                .HasOne(seats => seats.MovieHall)
                .WithMany(movieHall => movieHall.Seats)
                .HasForeignKey(seats => seats.MovieHallId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //Source: https://www.cinemacity.hu/cinemas/arena/1132?lang=en_GB#/buy-tickets-by-cinema?in-cinema=1132&at=2021-10-13&view-mode=list Downloaded: 2021.10.13.

            #region DbSeed
            //Order: Movie, MovieHall, Staff | Seats, Showtime, Ticket
            #region MoviePreData
            Movie movie1 = new Movie() { MovieId = 1, MovieTitle = "30 Days Left (30 Jours Max)", Category = "Action, Comedy, Crime", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 27, 0), Rating = 4 };
            Movie movie2 = new Movie() { MovieId = 2, MovieTitle = "A Feleségem Története", Category = "Drama, Romance", AgeRestriction = 16, Languages = "HU", Duration = new TimeSpan(2, 49, 0), Rating = 1 };
            Movie movie3 = new Movie() { MovieId = 3, MovieTitle = "After We Fell", Category = "Drama, Romance", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 38, 0), Rating = 3 };
            Movie movie4 = new Movie() { MovieId = 4, MovieTitle = "Candyman", Category = "Drama", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 31, 0), Rating = 4 };
            Movie movie5 = new Movie() { MovieId = 5, MovieTitle = "Eltörölni Frankot", Category = "Action, Comedy, Crime", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 27, 0), Rating = 5 };
            Movie movie6 = new Movie() { MovieId = 6, MovieTitle = "Escape Room: Tournament of Champions", Category = "Action, Adventure, Horror", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 28, 0), Rating = 3 };
            Movie movie7 = new Movie() { MovieId = 7, MovieTitle = "Free Guy", Category = "Action, Comedy, Fantasy", AgeRestriction = 12, Languages = "HU, EN", Duration = new TimeSpan(1, 55, 0), Rating = 4 };
            Movie movie8 = new Movie() { MovieId = 8, MovieTitle = "Jungle Cruise", Category = "Adventure, Family, Fantasy", AgeRestriction = 12, Languages = "HU, EN", Duration = new TimeSpan(2, 7, 0), Rating = 5 };
            Movie movie9 = new Movie() { MovieId = 9, MovieTitle = "Luca", Category = "Animation, Family", AgeRestriction = 6, Languages = "HU, EN", Duration = new TimeSpan(1, 35, 0), Rating = 3 };
            Movie movie10 = new Movie() { MovieId = 10, MovieTitle = "The Night House", Category = "Horror, Thriller", AgeRestriction = 16, Languages = "EN", Duration = new TimeSpan(1, 48, 0), Rating = 4 };
            #endregion
            modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3, movie4, movie5, movie6, movie7, movie8, movie9, movie10);

            #region MovieHallPreData
            MovieHall movieHall1 = new MovieHall() { MovieHallId = 1, HallCategory = "2D", NumberOfSeats = 50 };
            MovieHall movieHall2 = new MovieHall() { MovieHallId = 2, HallCategory = "2D", NumberOfSeats = 60 };
            MovieHall movieHall3 = new MovieHall() { MovieHallId = 3, HallCategory = "IMAX 3D", NumberOfSeats = 55 };
            MovieHall movieHall4 = new MovieHall() { MovieHallId = 4, HallCategory = "2D ScreenX", NumberOfSeats = 60 };
            MovieHall movieHall5 = new MovieHall() { MovieHallId = 5, HallCategory = "3D", NumberOfSeats = 50 };
            #endregion
            modelBuilder.Entity<MovieHall>().HasData(movieHall1, movieHall2, movieHall3, movieHall4, movieHall5);

            #region StaffPreData
            Staff staff1 = new Staff() { StaffID = 1, Name = "Justin Thyme", Gender = "Male", IC = "123456DE", MobileNumber = "36300123456" };
            Staff staff2 = new Staff() { StaffID = 2, Name = "Kay Oss", Gender = "Female", IC = "987654BC", MobileNumber = "36209876543" };
            Staff staff3 = new Staff() { StaffID = 3, Name = "Jack Pott", Gender = "Male", IC = "696969GF", MobileNumber = "36706660069" };
            Staff staff4 = new Staff() { StaffID = 4, Name = "Holly Wood", Gender = "Female", IC = "666666HA", MobileNumber = "36306969696" };
            Staff staff5 = new Staff() { StaffID = 5, Name = "Joe King", Gender = "Male", IC = "424242CI", MobileNumber = null };
            #endregion
            modelBuilder.Entity<Staff>().HasData(staff1, staff2, staff3, staff4, staff5);

            #region SeatsPreDataGeteratorAndSeed
            for (int i = 0; i < 50; i++)
            {
                if (i < 20)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1, SeatCategory = "Front", MovieHallId = movieHall1.MovieHallId });
                }
                else if (i < 35)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1, SeatCategory = "Middle", MovieHallId = movieHall1.MovieHallId });
                }
                else
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1, SeatCategory = "Back", MovieHallId = movieHall1.MovieHallId });
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 20)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Front", MovieHallId = movieHall2.MovieHallId });
                }
                else if (i < 40)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Middle", MovieHallId = movieHall2.MovieHallId });
                }
                else
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Back", MovieHallId = movieHall2.MovieHallId });
                }
            }
            for (int i = 0; i < 55; i++)
            {
                if (i < 20)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Front", MovieHallId = movieHall3.MovieHallId });
                }
                else if (i < 35)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Middle", MovieHallId = movieHall3.MovieHallId });
                }
                else
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Back", MovieHallId = movieHall3.MovieHallId });
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 20)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Front", MovieHallId = movieHall4.MovieHallId });
                }
                else if (i < 40)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Middle", MovieHallId = movieHall4.MovieHallId });
                }
                else
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Back", MovieHallId = movieHall4.MovieHallId });
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (i < 20)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Front", MovieHallId = movieHall5.MovieHallId });
                }
                else if (i < 35)
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Middle", MovieHallId = movieHall5.MovieHallId });
                }
                else
                {
                    modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Back", MovieHallId = movieHall5.MovieHallId });
                }
            }

            for (int i = 0; i < 20; i++)
            {
                modelBuilder.Entity<Seats>().HasData(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60 + 50, SeatCategory = null, MovieHallId = null });
            }
            #endregion

            #region ShowtimePreData
            Showtime show1 = new Showtime() { ShowtimeId = 1, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 13, 12, 0, 0) };
            Showtime show2 = new Showtime() { ShowtimeId = 2, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 13, 14, 20, 0) };
            Showtime show3 = new Showtime() { ShowtimeId = 3, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 13, 16, 40, 0) };
            Showtime show4 = new Showtime() { ShowtimeId = 4, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 13, 20, 0, 0) };

            Showtime show5 = new Showtime() { ShowtimeId = 5, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 14, 12, 0, 0) };
            Showtime show6 = new Showtime() { ShowtimeId = 6, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 14, 14, 20, 0) };
            Showtime show7 = new Showtime() { ShowtimeId = 7, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 14, 16, 40, 0) };
            Showtime show8 = new Showtime() { ShowtimeId = 8, MovieHallId = movieHall1.MovieHallId, MovieId = movie7.MovieId, Date = new DateTime(2021, 10, 14, 20, 0, 0) };

            Showtime show9 = new Showtime() { ShowtimeId = 9, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 13, 16, 0, 0) };
            Showtime show10 = new Showtime() { ShowtimeId = 10, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 13, 18, 0, 0) };
            Showtime show11 = new Showtime() { ShowtimeId = 11, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 13, 20, 0, 0) };

            Showtime show12 = new Showtime() { ShowtimeId = 12, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 13, 17, 0, 0) };
            Showtime show13 = new Showtime() { ShowtimeId = 13, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 13, 19, 0, 0) };
            Showtime show14 = new Showtime() { ShowtimeId = 14, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 13, 21, 0, 0) };

            Showtime show15 = new Showtime() { ShowtimeId = 15, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 15, 16, 0, 0) };
            Showtime show16 = new Showtime() { ShowtimeId = 16, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 15, 18, 0, 0) };
            Showtime show17 = new Showtime() { ShowtimeId = 17, MovieHallId = movieHall3.MovieHallId, MovieId = movie1.MovieId, Date = new DateTime(2021, 10, 15, 20, 0, 0) };

            Showtime show18 = new Showtime() { ShowtimeId = 18, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 15, 17, 0, 0) };
            Showtime show19 = new Showtime() { ShowtimeId = 19, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 15, 19, 0, 0) };
            Showtime show20 = new Showtime() { ShowtimeId = 20, MovieHallId = movieHall4.MovieHallId, MovieId = movie5.MovieId, Date = new DateTime(2021, 10, 15, 21, 0, 0) };

            Showtime show21 = new Showtime() { ShowtimeId = 21, MovieHallId = movieHall2.MovieHallId, MovieId = movie2.MovieId, Date = new DateTime(2021, 10, 16, 14, 0, 0) };
            Showtime show22 = new Showtime() { ShowtimeId = 22, MovieHallId = movieHall2.MovieHallId, MovieId = movie3.MovieId, Date = new DateTime(2021, 10, 16, 18, 0, 0) };
            Showtime show24 = new Showtime() { ShowtimeId = 23, MovieHallId = movieHall2.MovieHallId, MovieId = movie4.MovieId, Date = new DateTime(2021, 10, 17, 18, 0, 0) };
            Showtime show23 = new Showtime() { ShowtimeId = 24, MovieHallId = movieHall5.MovieHallId, MovieId = movie6.MovieId, Date = new DateTime(2021, 10, 16, 21, 0, 0) };

            Showtime show25 = new Showtime() { ShowtimeId = 25, MovieHallId = movieHall1.MovieHallId, MovieId = movie8.MovieId, Date = new DateTime(2021, 10, 17, 12, 0, 0) };
            Showtime show26 = new Showtime() { ShowtimeId = 26, MovieHallId = movieHall1.MovieHallId, MovieId = movie9.MovieId, Date = new DateTime(2021, 10, 17, 16, 0, 0) };
            Showtime show27 = new Showtime() { ShowtimeId = 27, MovieHallId = movieHall4.MovieHallId, MovieId = movie10.MovieId, Date = new DateTime(2021, 10, 17, 22, 0, 0) };
            #endregion
            modelBuilder
                .Entity<Showtime>()
                .HasData(show1, show2, show3, show4, show5, show6, show7, show8, show9, show10,
                         show11, show12, show13, show14, show15, show16, show17, show18, show19, show20,
                         show21, show22, show23, show24, show25, show26, show27);

            #region TicketPreData
            Ticket ticket1 = new Ticket() { TicketID = 1, ShowtimeId = show1.ShowtimeId, StaffId = staff1.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket2 = new Ticket() { TicketID = 2, ShowtimeId = show2.ShowtimeId, StaffId = staff2.StaffID, PaymentMethod = "cash", Price = 25 };
            Ticket ticket3 = new Ticket() { TicketID = 3, ShowtimeId = show3.ShowtimeId, StaffId = staff3.StaffID, PaymentMethod = "card", Price = 25 };
            Ticket ticket4 = new Ticket() { TicketID = 4, ShowtimeId = show4.ShowtimeId, StaffId = staff4.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket5 = new Ticket() { TicketID = 5, ShowtimeId = show5.ShowtimeId, StaffId = staff5.StaffID, PaymentMethod = "card", Price = 35 };
            Ticket ticket6 = new Ticket() { TicketID = 6, ShowtimeId = show6.ShowtimeId, StaffId = staff5.StaffID, PaymentMethod = "cash", Price = 35 };
            Ticket ticket7 = new Ticket() { TicketID = 7, ShowtimeId = show8.ShowtimeId, StaffId = staff4.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket8 = new Ticket() { TicketID = 8, ShowtimeId = show7.ShowtimeId, StaffId = staff3.StaffID, PaymentMethod = "card", Price = 20 };
            Ticket ticket9 = new Ticket() { TicketID = 9, ShowtimeId = show9.ShowtimeId, StaffId = staff2.StaffID, PaymentMethod = "cash", Price = 20 };
            Ticket ticket10 = new Ticket() { TicketID = 10, ShowtimeId = show10.ShowtimeId, StaffId = staff1.StaffID, PaymentMethod = "card", Price = 30 };

            Ticket ticket11 = new Ticket() { TicketID = 11, ShowtimeId = show11.ShowtimeId, StaffId = staff1.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket12 = new Ticket() { TicketID = 12, ShowtimeId = show12.ShowtimeId, StaffId = staff2.StaffID, PaymentMethod = "cash", Price = 25 };
            Ticket ticket13 = new Ticket() { TicketID = 13, ShowtimeId = show13.ShowtimeId, StaffId = staff3.StaffID, PaymentMethod = "card", Price = 25 };
            Ticket ticket14 = new Ticket() { TicketID = 14, ShowtimeId = show14.ShowtimeId, StaffId = staff4.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket15 = new Ticket() { TicketID = 15, ShowtimeId = show15.ShowtimeId, StaffId = staff5.StaffID, PaymentMethod = "card", Price = 35 };
            Ticket ticket16 = new Ticket() { TicketID = 16, ShowtimeId = show16.ShowtimeId, StaffId = staff5.StaffID, PaymentMethod = "cash", Price = 35 };
            Ticket ticket17 = new Ticket() { TicketID = 17, ShowtimeId = show18.ShowtimeId, StaffId = staff4.StaffID, PaymentMethod = "card", Price = 30 };
            Ticket ticket18 = new Ticket() { TicketID = 18, ShowtimeId = show17.ShowtimeId, StaffId = staff3.StaffID, PaymentMethod = "card", Price = 20 };
            Ticket ticket19 = new Ticket() { TicketID = 19, ShowtimeId = show19.ShowtimeId, StaffId = staff2.StaffID, PaymentMethod = "cash", Price = 20 };
            Ticket ticket20 = new Ticket() { TicketID = 20, ShowtimeId = show20.ShowtimeId, StaffId = staff1.StaffID, PaymentMethod = "card", Price = 30 };

            #endregion
            modelBuilder
                .Entity<Ticket>()
                .HasData(ticket1, ticket2, ticket3, ticket4, ticket5, ticket6, ticket7, ticket8, ticket9, ticket10,
                         ticket11, ticket12, ticket13, ticket14, ticket15, ticket16, ticket17, ticket18, ticket19, ticket20);
            #endregion

            
        }
    }
}
