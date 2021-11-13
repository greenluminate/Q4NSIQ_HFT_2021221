using NUnit.Framework;
using Moq;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using Q4NSIQ_HFT_2021221.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Test
{
    [TestFixture]
    class Tester
    {
        MovieLogic movieLogic;
        MovieHallLogic movieHallLogic;
        StaffLogic staffLogic;

        SeatsLogic seatsLogic;
        ShowtimeLogic showtimeLogic;
        TicketLogic ticketLogic;

        DateTime today;

        [SetUp]
        public void Init()
        {
            today = DateTime.Today;

            #region FakeMovies
            var movies = new List<Movie>();
            movies.Add(new Movie() { MovieId = 1, MovieTitle = "30 Days Left (30 Jours Max)", Category = "Action, Comedy, Crime", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 27, 0), Rating = 4 });
            movies.Add(new Movie() { MovieId = 2, MovieTitle = "A Feleségem Története", Category = "Drama, Romance", AgeRestriction = 16, Languages = "HU", Duration = new TimeSpan(2, 49, 0), Rating = 1 });
            movies.Add(new Movie() { MovieId = 3, MovieTitle = "After We Fell", Category = "Drama, Romance", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 38, 0), Rating = 3 });
            movies.Add(new Movie() { MovieId = 4, MovieTitle = "Candyman", Category = "Drama", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 31, 0), Rating = 4 });
            movies.Add(new Movie() { MovieId = 5, MovieTitle = "Eltörölni Frankot", Category = "Action, Comedy, Crime", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 27, 0), Rating = 5 });
            movies.Add(new Movie() { MovieId = 6, MovieTitle = "Escape Room: Tournament of Champions", Category = "Action, Adventure, Horror", AgeRestriction = 16, Languages = "HU, EN", Duration = new TimeSpan(1, 28, 0), Rating = 3 });
            movies.Add(new Movie() { MovieId = 7, MovieTitle = "Free Guy", Category = "Action, Comedy, Fantasy", AgeRestriction = 12, Languages = "HU, EN", Duration = new TimeSpan(1, 55, 0), Rating = 4 });
            movies.Add(new Movie() { MovieId = 8, MovieTitle = "Jungle Cruise", Category = "Adventure, Family, Fantasy", AgeRestriction = 12, Languages = "HU, EN", Duration = new TimeSpan(2, 7, 0), Rating = 5 });
            movies.Add(new Movie() { MovieId = 9, MovieTitle = "Luca", Category = "Animation, Family", AgeRestriction = 6, Languages = "HU, EN", Duration = new TimeSpan(1, 35, 0), Rating = 3 });
            movies.Add(new Movie() { MovieId = 10, MovieTitle = "The Night House", Category = "Horror, Thriller", AgeRestriction = 16, Languages = "EN", Duration = new TimeSpan(1, 48, 0), Rating = 4 });
            #endregion

            #region FakeMovieHalls
            var movieHalls = new List<MovieHall>();
            movieHalls.Add(new MovieHall() { MovieHallId = 1, HallCategory = "2D", NumberOfSeats = 50 });
            movieHalls.Add(new MovieHall() { MovieHallId = 2, HallCategory = "2D", NumberOfSeats = 60 });
            movieHalls.Add(new MovieHall() { MovieHallId = 3, HallCategory = "IMAX 3D", NumberOfSeats = 55 });
            movieHalls.Add(new MovieHall() { MovieHallId = 4, HallCategory = "2D ScreenX", NumberOfSeats = 60 });
            movieHalls.Add(new MovieHall() { MovieHallId = 5, HallCategory = "3D", NumberOfSeats = 50 });
            #endregion

            #region FakeStaffs
            var staffs = new List<Staff>();
            staffs.Add(new Staff() { StaffId = 1, Name = "Justin Thyme", Gender = "Male", IC = "123456DE", MobileNumber = "36300123456" });
            staffs.Add(new Staff() { StaffId = 2, Name = "Kay Oss", Gender = "Female", IC = "987654BC", MobileNumber = "36209876543" });
            staffs.Add(new Staff() { StaffId = 3, Name = "Jack Pott", Gender = "Male", IC = "696969GF", MobileNumber = "36706660069" });
            staffs.Add(new Staff() { StaffId = 4, Name = "Holly Wood", Gender = "Female", IC = "666666HA", MobileNumber = "36306969696" });
            staffs.Add(new Staff() { StaffId = 5, Name = "Joe King", Gender = "Male", IC = "424242CI", MobileNumber = null });
            #endregion

            #region FakeSeats
            var seats = new List<Seats>();
            for (int i = 0; i < 50; i++)
            {
                if (i < 20)
                {
                    seats.Add(new Seats() { SeatId = i + 1, SeatCategory = "Front", SeatNumber = i + 1, MovieHallId = movieHalls[0].MovieHallId });
                }
                else if (i < 35)
                {
                    seats.Add(new Seats() { SeatId = i + 1, SeatCategory = "Middle", SeatNumber = i + 1, MovieHallId = movieHalls[0].MovieHallId });
                }
                else
                {
                    seats.Add(new Seats() { SeatId = i + 1, SeatCategory = "Back", SeatNumber = i + 1, MovieHallId = movieHalls[0].MovieHallId });
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 20)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Front", SeatNumber = i + 1, MovieHallId = movieHalls[1].MovieHallId });
                }
                else if (i < 40)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Middle", SeatNumber = i + 1, MovieHallId = movieHalls[1].MovieHallId });
                }
                else
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50, SeatCategory = "Back", SeatNumber = i + 1, MovieHallId = movieHalls[1].MovieHallId });
                }
            }
            for (int i = 0; i < 55; i++)
            {
                if (i < 20)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Front", SeatNumber = i + 1, MovieHallId = movieHalls[2].MovieHallId });
                }
                else if (i < 35)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Middle", SeatNumber = i + 1, MovieHallId = movieHalls[2].MovieHallId });
                }
                else
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60, SeatCategory = "Back", SeatNumber = i + 1, MovieHallId = movieHalls[2].MovieHallId });
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 20)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Front", SeatNumber = i + 1, MovieHallId = movieHalls[3].MovieHallId });
                }
                else if (i < 40)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Middle", SeatNumber = i + 1, MovieHallId = movieHalls[3].MovieHallId });
                }
                else
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55, SeatCategory = "Back", SeatNumber = i + 1, MovieHallId = movieHalls[3].MovieHallId });
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if (i < 20)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Front", SeatNumber = i + 1, MovieHallId = movieHalls[4].MovieHallId });
                }
                else if (i < 35)
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Middle", SeatNumber = i + 1, MovieHallId = movieHalls[4].MovieHallId });
                }
                else
                {
                    seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60, SeatCategory = "Back", SeatNumber = i + 1, MovieHallId = movieHalls[4].MovieHallId });
                }
            }

            for (int i = 0; i < 20; i++)
            {
                seats.Add(new Seats() { SeatId = i + 1 + 50 + 60 + 55 + 60 + 50, SeatCategory = null, MovieHallId = null });
            }
            #endregion

            #region FakeShowtimes
            var showtimes = new List<Showtime>();
            showtimes.Add(new Showtime() { ShowtimeId = 1, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 13, 12, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 2, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 13, 14, 20, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 3, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 13, 16, 40, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 4, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 13, 20, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 5, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 14, 12, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 6, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 14, 14, 20, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 7, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 14, 16, 40, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 8, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[6].MovieId, Date = new DateTime(2021, 10, 14, 20, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 9, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 13, 16, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 10, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 13, 18, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 11, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 13, 20, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 12, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 13, 17, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 13, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 13, 19, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 14, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 13, 21, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 15, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 15, 16, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 16, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 15, 18, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 17, MovieHallId = movieHalls[2].MovieHallId, MovieId = movies[0].MovieId, Date = new DateTime(2021, 10, 15, 20, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 18, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 15, 17, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 19, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 15, 19, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 20, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[4].MovieId, Date = new DateTime(2021, 10, 15, 21, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 21, MovieHallId = movieHalls[1].MovieHallId, MovieId = movies[1].MovieId, Date = new DateTime(2021, 10, 16, 14, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 22, MovieHallId = movieHalls[1].MovieHallId, MovieId = movies[2].MovieId, Date = new DateTime(2021, 10, 16, 18, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 23, MovieHallId = movieHalls[1].MovieHallId, MovieId = movies[3].MovieId, Date = new DateTime(2021, 10, 17, 18, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 24, MovieHallId = movieHalls[4].MovieHallId, MovieId = movies[5].MovieId, Date = new DateTime(2021, 10, 16, 21, 0, 0) });

            showtimes.Add(new Showtime() { ShowtimeId = 25, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[7].MovieId, Date = new DateTime(2021, 10, 17, 12, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 26, MovieHallId = movieHalls[0].MovieHallId, MovieId = movies[8].MovieId, Date = new DateTime(2021, 10, 17, 16, 0, 0) });
            showtimes.Add(new Showtime() { ShowtimeId = 27, MovieHallId = movieHalls[3].MovieHallId, MovieId = movies[0].MovieId, Date = today });
            #endregion

            #region FakeTickets
            var tickets = new List<Ticket>();
            tickets.Add(new Ticket() { TicketId = 1, ShowtimeId = showtimes[0].ShowtimeId, SeatId = 1, StaffId = staffs[0].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 3, ShowtimeId = showtimes[2].ShowtimeId, SeatId = 1, StaffId = staffs[2].StaffId, PaymentMethod = "card", Price = 25 });
            tickets.Add(new Ticket() { TicketId = 4, ShowtimeId = showtimes[3].ShowtimeId, SeatId = 4, StaffId = staffs[3].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 5, ShowtimeId = showtimes[4].ShowtimeId, SeatId = 5, StaffId = staffs[4].StaffId, PaymentMethod = "card", Price = 35 });
            tickets.Add(new Ticket() { TicketId = 7, ShowtimeId = showtimes[7].ShowtimeId, SeatId = 7, StaffId = staffs[3].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 8, ShowtimeId = showtimes[6].ShowtimeId, SeatId = 8, StaffId = staffs[2].StaffId, PaymentMethod = "card", Price = 20 });
            tickets.Add(new Ticket() { TicketId = 10, ShowtimeId = showtimes[9].ShowtimeId, SeatId = 112, StaffId = staffs[0].StaffId, PaymentMethod = "card", Price = 30 });

            tickets.Add(new Ticket() { TicketId = 11, ShowtimeId = showtimes[10].ShowtimeId, SeatId = 113, StaffId = staffs[0].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 13, ShowtimeId = showtimes[12].ShowtimeId, SeatId = 167, StaffId = staffs[2].StaffId, PaymentMethod = "card", Price = 25 });
            tickets.Add(new Ticket() { TicketId = 14, ShowtimeId = showtimes[13].ShowtimeId, SeatId = 168, StaffId = staffs[3].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 15, ShowtimeId = showtimes[14].ShowtimeId, SeatId = 114, StaffId = staffs[4].StaffId, PaymentMethod = "card", Price = 35 });
            tickets.Add(new Ticket() { TicketId = 16, ShowtimeId = showtimes[15].ShowtimeId, SeatId = 114, StaffId = staffs[4].StaffId, PaymentMethod = "cash", Price = 35 });
            tickets.Add(new Ticket() { TicketId = 17, ShowtimeId = showtimes[17].ShowtimeId, SeatId = 114, StaffId = staffs[3].StaffId, PaymentMethod = "card", Price = 30 });
            tickets.Add(new Ticket() { TicketId = 18, ShowtimeId = showtimes[16].ShowtimeId, SeatId = 169, StaffId = staffs[2].StaffId, PaymentMethod = "card", Price = 20 });
            tickets.Add(new Ticket() { TicketId = 20, ShowtimeId = showtimes[19].ShowtimeId, SeatId = 171, StaffId = staffs[0].StaffId, PaymentMethod = "card", Price = 30 });

            #endregion

            #region InitMockRepo
            var mockMovieRepo = new Mock<IMovieRepository>();
            var mockMovieHallRepo = new Mock<IMovieHallRepository>();
            var mockStaffRepo = new Mock<IStaffRepository>();

            var mockSeatsRepo = new Mock<ISeatsRepository>();
            var mockShowtimeRepo = new Mock<IShowtimeRepository>();
            var mockTicketRepo = new Mock<ITicketRepository>();
            #endregion

            #region SetUp-Create
            mockMovieRepo.Setup(mmr => mmr.Create(It.IsAny<Movie>())).Callback<Movie>(movie => movies.Add(movie));
            mockMovieHallRepo.Setup(mmhr => mmhr.Create(It.IsAny<MovieHall>())).Callback<MovieHall>(movieHall => movieHalls.Add(movieHall));
            mockMovieHallRepo.Setup(mmhr => mmhr.Create(It.IsAny<MovieHall>())).Throws<NullReferenceException>();
            #endregion

            #region SetUp-Read
            mockMovieRepo.Setup(mr => mr.Read(It.IsAny<int>())).Returns((int id) => movies.Where(m => m.MovieId == id).Single());
            mockMovieHallRepo.Setup(mhr => mhr.Read(It.IsAny<int>())).Returns((int id) => movieHalls.Where(mh => mh.MovieHallId == id).Single());
            mockStaffRepo.Setup(str => str.Read(It.IsAny<int>())).Returns((int id) => staffs.Where(st => st.StaffId == id).Single());

            mockSeatsRepo.Setup(setr => setr.Read(It.IsAny<int>())).Returns((int? id) => seats.Where(se => se.SeatId == id).Single());
            mockShowtimeRepo.Setup(shr => shr.Read(It.IsAny<int>())).Returns((int id) => showtimes.Where(sh => sh.ShowtimeId == id).Single());
            mockTicketRepo.Setup(tr => tr.Read(It.IsAny<int>())).Returns((int id) => tickets.Where(t => t.TicketId == id).Single());
            #endregion

            #region SetUp-ReadAll
            mockMovieRepo.Setup(mr => mr.ReadAll()).Returns(movies.AsQueryable());
            mockMovieHallRepo.Setup(mhr => mhr.ReadAll()).Returns(movieHalls.AsQueryable());
            mockStaffRepo.Setup(str => str.ReadAll()).Returns(staffs.AsQueryable());

            mockSeatsRepo.Setup(setr => setr.ReadAll()).Returns(seats.AsQueryable());
            mockShowtimeRepo.Setup(shr => shr.ReadAll()).Returns(showtimes.AsQueryable());
            mockTicketRepo.Setup(tr => tr.ReadAll()).Returns(tickets.AsQueryable());
            #endregion

            #region SetUp-UniqueCRUD
            mockMovieRepo.Setup(mr => mr.ReadByTitle(It.IsAny<string>())).Returns((string title) => movies.Where(m => m.MovieTitle == title).AsQueryable());
            mockMovieHallRepo.Setup(mhr => mhr.ReadByCategory(It.IsAny<string>())).Returns((string category) => movieHalls.Where(mh => mh.HallCategory == category).AsQueryable());
            mockStaffRepo.Setup(str => str.ReadByName(It.IsAny<string>())).Returns((string name) => staffs.Where(st => st.Name == name).AsQueryable());

            mockSeatsRepo.Setup(setr => setr.ReadByMovieHallId(It.IsAny<int>())).Returns((int id) => seats.Where(seat => seat.MovieHallId == id).AsQueryable());
            mockShowtimeRepo.Setup(shr => shr.ReadByDate(It.IsAny<DateTime?>())).Returns((DateTime? date) => showtimes.Where(sh => sh.Date.Date == (date is null ? today.Date : date.Value.Date)).AsQueryable());
            mockTicketRepo.Setup(tr => tr.ReadByShowtimeId(It.IsAny<int>())).Returns((int id) => tickets.Where(t => t.ShowtimeId == id).AsQueryable());
            #endregion

            #region InitLogic
            movieLogic = new MovieLogic(mockMovieRepo.Object);
            movieHallLogic = new MovieHallLogic(mockMovieHallRepo.Object);
            staffLogic = new StaffLogic(mockStaffRepo.Object, mockMovieHallRepo.Object, mockMovieRepo.Object);

            seatsLogic = new SeatsLogic(mockSeatsRepo.Object);
            showtimeLogic = new ShowtimeLogic(mockShowtimeRepo.Object);
            ticketLogic = new TicketLogic(mockTicketRepo.Object);
            #endregion

            #region SetNotMappedData
            foreach (var movie in movies)
            {
                foreach (var show in showtimes)
                {
                    if (show.MovieId == movie.MovieId)
                    {
                        movie.Showtimes.Add(show);
                    }
                }
            }

            foreach (var movieHall in movieHalls)
            {
                foreach (var show in showtimes)
                {
                    if (show.MovieHallId == movieHall.MovieHallId)
                    {
                        movieHall.Showtimes.Add(show);
                    }
                }
            }

            foreach (var showtime in showtimes)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.ShowtimeId == showtime.ShowtimeId)
                    {
                        showtime.Tickets.Add(ticket);
                    }
                }

                showtime.Movie = movieLogic.Read(showtime.MovieId);
                showtime.MovieHall = movieHallLogic.Read(showtime.MovieHallId);
            }

            foreach (var staff in staffs)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.StaffId == staff.StaffId)
                    {
                        staff.Tickets.Add(ticket);
                    }
                }
            }

            foreach (var seat in seats)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.SeatId == seat.SeatId)
                    {
                        seat.Tickets.Add(ticket);
                    }
                }

                int mhId = seat.MovieHallId is null ? 0 : (int)seat.MovieHallId;
                if (mhId != 0)
                {
                    seat.MovieHall = movieHallLogic.Read((int)seat.MovieHallId);
                }
            }

            foreach (var movieHall in movieHalls)
            {
                foreach (var seat in seats)
                {
                    if (seat.MovieHallId == movieHall.MovieHallId)
                    {
                        movieHall.Seats.Add(seat);
                    }
                }
            }

            foreach (var ticket in tickets)
            {
                ticket.Seat = seatsLogic.Read(ticket.SeatId);
                ticket.Staff = staffLogic.Read(ticket.StaffId);
                ticket.Showtime = showtimeLogic.Read(ticket.ShowtimeId);
            }
            #endregion
        }

        #region CRUDTests
        [Test]
        public void TicketReadTest()
        {
            Ticket testTicket = new Ticket() { TicketId = 10, ShowtimeId = 10, SeatId = 112, StaffId = 1, PaymentMethod = "card", Price = 30 };
            Assert.That(testTicket, Is.EqualTo(ticketLogic.Read(10)));
        }

        [Test]
        public void MovieCreatTest()
        {
            Movie newMovie = new Movie()
            {
                MovieId = 999,
                MovieTitle = "New Movie",
                Category = "Horror, Thriller",
                AgeRestriction = 18,
                Languages = "HU",
                Duration = new TimeSpan(1, 52, 0),
                Rating = 3,
            };

            movieLogic.Create(newMovie);
            Assert.That(newMovie, Is.EqualTo(movieLogic.Read(newMovie.MovieId)));
        }

        [Test]
        public void MovieHallCreatTest()
        {
            MovieHall newMovieHall = new MovieHall()
            {
                MovieHallId = 6,
                HallCategory = "3D"
            };

            Assert.Throws(typeof(NullReferenceException), () => movieHallLogic.Create(newMovieHall));
        }
        #endregion

        #region UniqueCRUDTests
        [Test]
        public void MovieReadByTitleTest()
        {
            var result = movieLogic.ReadByTitle("Free Guy");
            var expected = new List<Movie>()
            {
                new Movie() { MovieId = 7, MovieTitle = "Free Guy", Category = "Action, Comedy, Fantasy",
                              AgeRestriction = 12, Languages = "HU, EN", Duration = new TimeSpan(1, 55, 0),
                              Rating = 4 }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void MovieHallReadByCategoryTest()
        {
            var result = movieHallLogic.ReadByCategory("2D");
            var expected = new List<MovieHall>()
            {
                new MovieHall() { MovieHallId = 1, HallCategory = "2D", NumberOfSeats = 50 },
                new MovieHall() { MovieHallId = 2, HallCategory = "2D", NumberOfSeats = 60 }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void StaffReadByNameTest()
        {
            var result = staffLogic.ReadByName("Jack Pott");
            var expected = new List<Staff>()
            {
                new Staff() { StaffId = 3, Name = "Jack Pott", Gender = "Male", IC = "696969GF", MobileNumber = "36706660069" }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void StaffReadByNameTestWithNonExisted()
        {
            var result = staffLogic.ReadByName("Anonymous");
            var expected = new List<Staff>() { };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShowtimeReadByDateTestToday()
        {
            var result = showtimeLogic.ReadByDate(null);
            var expected = new List<Showtime>()
            {
                new Showtime() { ShowtimeId = 27, MovieHallId = 4, MovieId = 1, Date = today }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ShowtimeReadByDateTest()
        {
            var result = showtimeLogic.ReadByDate(new DateTime(2021, 10, 14));
            var expected = new List<Showtime>()
            {
                new Showtime() { ShowtimeId = 5, MovieHallId = 1, MovieId = 7, Date = new DateTime(2021, 10, 14, 12, 0, 0) },
                new Showtime() { ShowtimeId = 6, MovieHallId = 1, MovieId = 7, Date = new DateTime(2021, 10, 14, 14, 20, 0) },
                new Showtime() { ShowtimeId = 7, MovieHallId = 1, MovieId = 7, Date = new DateTime(2021, 10, 14, 16, 40, 0) },
                new Showtime() { ShowtimeId = 8, MovieHallId = 1, MovieId = 7, Date = new DateTime(2021, 10, 14, 20, 0, 0) }
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReadByShowtimeIdTest()
        {
            var result = ticketLogic.ReadByShowtimeId(4);
            var expected = new List<Ticket>()
            {
                new Ticket() { TicketId = 4, ShowtimeId = 4, SeatId = 4, StaffId = 4, PaymentMethod = "card", Price = 30 }
            };
        }
        #endregion

        #region StaffLogicTests
        [Test]
        public void CountOfSoldTicketsByStaffTest()
        {
            var result = staffLogic.CountOfSoldTicketsByStaff();
            var expected = new List
            <KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>
                ("Holly Wood", 4),
                new KeyValuePair<string, int>
                ("Jack Pott", 4),
                new KeyValuePair<string, int>
                ("Justin Thyme", 4),
                new KeyValuePair<string, int>
                ("Joe King", 3),
                new KeyValuePair<string, int>
                ("Kay Oss", 0)
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SUMPriceOfSoldTicketsByStaffTest()
        {
            var result = staffLogic.SUMPriceOfSoldTicketsByStaff();
            var expected = new List
            <KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>
                ("Holly Wood", 120),
                new KeyValuePair<string, int>
                ("Justin Thyme", 120),
                new KeyValuePair<string, int>
                ("Joe King", 105),
                new KeyValuePair<string, int>
                ("Jack Pott", 90),
                new KeyValuePair<string, int>
                ("Kay Oss", 0)
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TopSoldTicketsByStaffPerMovieTest()
        {
            var result = staffLogic.TopSoldTicketsByStaffPerMovie();
            var expected = new List
            <KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>()
            {
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Holly Wood",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("Eltörölni Frankot", 2),
                       new KeyValuePair<string, int>
                       ("Free Guy", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Jack Pott",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("Free Guy", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Joe King",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("30 Days Left (30 Jours Max)", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Justin Thyme",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("30 Days Left (30 Jours Max)", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Kay Oss",
                    new List<KeyValuePair<string, int>>(){}
                )
            };

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SoldTicketsByStaffPerHallTypeTest()
        {
            var result = staffLogic.SoldTicketsByStaffPerHallType();
            var expected = new List
            <KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>()
            {
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Holly Wood",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("2D", 2),
                       new KeyValuePair<string, int>
                       ("2D ScreenX", 2),
                       new KeyValuePair<string, int>
                       ("3D", 0),
                        new KeyValuePair<string, int>
                       ("IMAX 3D", 0)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Jack Pott",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("2D", 2),
                       new KeyValuePair<string, int>
                       ("2D ScreenX", 1),
                       new KeyValuePair<string, int>
                       ("3D", 0),
                        new KeyValuePair<string, int>
                       ("IMAX 3D", 1)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Joe King",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("2D", 1),
                       new KeyValuePair<string, int>
                       ("2D ScreenX", 0),
                       new KeyValuePair<string, int>
                       ("3D", 0),
                        new KeyValuePair<string, int>
                       ("IMAX 3D", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Justin Thyme",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("2D", 1),
                       new KeyValuePair<string, int>
                       ("2D ScreenX", 1),
                       new KeyValuePair<string, int>
                       ("3D", 0),
                        new KeyValuePair<string, int>
                       ("IMAX 3D", 2)
                    }
                ),
                new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                (
                    "Kay Oss",
                    new List<KeyValuePair<string, int>>()
                    {
                       new KeyValuePair<string, int>
                       ("2D", 0),
                       new KeyValuePair<string, int>
                       ("2D ScreenX", 0),
                       new KeyValuePair<string, int>
                       ("3D", 0),
                        new KeyValuePair<string, int>
                       ("IMAX 3D", 0)
                    }
                )
            };

            Assert.That(result, Is.EqualTo(expected));
        }
        #endregion

        #region TicketLogicTests
        [Test]
        public void Top10UsesNoBySeat()
        {
            var result = ticketLogic.Top10MostUsedSeats();
            var expected = new List
            <KeyValuePair<Seats, int>>()
            {
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(114), 3),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(1), 2),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(4), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(5), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(7), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(8), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(112), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(113), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(167), 1),
                new KeyValuePair<Seats, int>
                (seatsLogic.Read(168), 1)
            };

            Assert.That(result, Is.EqualTo(expected));
        }
        #endregion
    }
}
