using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Client
{
    public class MenuHelper
    {

        RestService rest;
        public MenuHelper()
        {
            this.rest = new RestService(@"http://localhost:17133"); ;
        }

        public void Start()
        {
            Console.Title = "Cinema Database";
            RunMainMenu();
        }

        public void RunMainMenu()
        {
            string prompt = "Please choose an item You want to work with!\nPress: Up, Down Arrows and Enter keys";
            List<string> options = new List<string>() { "Movies", "MovieHalls", "Seats", "Showtimes", "Staffs", "Tickets", "Exit" };

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunMovieChoice();
                    break;
                case 1:
                    RunMovieHallChoice();
                    break;
                case 2:
                    RunSeatsChoice();
                    break;
                case 3:
                    RunShowtimeChoice();
                    break;
                case 4:
                    RunStaffChoice();
                    break;
                case 5:
                    RunTicketChoice();
                    break;
                case 6:
                    ExitCinemaDatabase();
                    break;
            }
        }

        public void RunSubMenuMovie(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Movie - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetMovie();
                    break;
                case 1:
                    RunGetMovies();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    RunDeleteMovie();
                    break;
                case 5:
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Movie menu...");
            Console.ReadKey(true);

            RunMovieChoice();
        }
        public void RunSubMenuMovieHall(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Movie hall - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetMovieHall();
                    break;
                case 1:
                    RunGetMovieHalls();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    RunDeleteMovieHall();
                    break;
                case 5:
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Movie hall menu...");
            Console.ReadKey(true);

            RunMovieHallChoice();
        }
        public void RunSubMenuSeat(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Seat - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetSeat();
                    break;
                case 1:
                    RunGetSeats();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Seat menu...");
            Console.ReadKey(true);

            RunSeatsChoice();
        }
        public void RunSubMenuShowtime(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Showtime - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetShowtime();
                    break;
                case 1:
                    RunGetShowtimes();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Showtime menu...");
            Console.ReadKey(true);

            RunShowtimeChoice();
        }
        public void RunSubMenuStaff(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Staff - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetStaff();
                    break;
                case 1:
                    RunGetStaffs();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Staff menu...");
            Console.ReadKey(true);

            RunStaffChoice();
        }
        public void RunSubMenuTicket(List<string> options, string prompt = null)
        {
            prompt = prompt ?? "Ticket - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            switch (selectedIndex)
            {
                case 0:
                    RunGetTicket();
                    break;
                case 1:
                    RunGetTickets();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    RunMainMenu();
                    break;
            }

            Console.WriteLine("\nPress any key to exit to the Ticket menu...");
            Console.ReadKey(true);

            RunTicketChoice();
        }

        private void ExitCinemaDatabase()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);//Ez így az enpoint folyamatát nem zárja be.
        }

        #region RunMainChoises
        private void RunMovieChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show movie(s) with given: Title" };

            RunSubMenuMovie(RunUniqueOptionsGenerator("movie", uniqueOptions));
        }
        private void RunMovieHallChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show movie hall(s) with given: Category" };

            RunSubMenuMovieHall(RunUniqueOptionsGenerator("movie hall", uniqueOptions));
        }
        private void RunSeatsChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show seat(s) with given: Movie Hall Id" };

            RunSubMenuSeat(RunUniqueOptionsGenerator("seat", uniqueOptions));
        }
        private void RunShowtimeChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show showtime(s) at given: Date" };

            RunSubMenuShowtime(RunUniqueOptionsGenerator("showtime", uniqueOptions));
        }
        private void RunStaffChoice()
        {
            List<string> uniqueOptions = new List<string>()
            {
                "Show staff(s) with given: Name",
                "Show how many tickets have been sold by staff",
                "Show the total amount of tickets sold by staff",
                "Show the total value of tickets sold by staff",
                "Show which staff sold the most tickets for which movie(s)",
                "Show which staff sold how many tickets for which type of movie hall"
            };

            RunSubMenuStaff(RunUniqueOptionsGenerator("staff", uniqueOptions));
        }
        private void RunTicketChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show tickets wit given: Showtime Id", "Show the Top 10 Most Used Seats" };

            RunSubMenuTicket(RunUniqueOptionsGenerator("ticket", uniqueOptions));
        }
        #endregion
        private List<string> RunUniqueOptionsGenerator(string objectName, List<string> uniqueOptions = null)
        {
            List<string> options = new List<string>() { $"Show {objectName} with given: Id", $"Show all {objectName}s", $"Reset {objectName} with given: Id", $"Add new {objectName}  with given: Id", $"Delet {objectName} given: Id" };
            if (uniqueOptions != null)
            {
                foreach (var option in uniqueOptions)
                {
                    options.Add(option);
                }
            }
            options.Add("Return to main menu");

            return options;
        }

        #region RunTaskChoices

        #region Movie
        private void RunGetMovies()
        {
            var staffs = rest.Get<Movie>("movie");

            foreach (var movie in staffs)
            {
                Console.WriteLine(movie);
            }
        }

        private void RunGetMovie()
        {
            Console.WriteLine("Please enter a movie Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A movie Id can only be a number!");
                RunGetMovie();
            }

            var movie = rest.GetSingle<Movie>($"movie/{id}");

            Console.WriteLine(movie);
        }

        private void RunDeleteMovie()
        {
            Console.WriteLine("Please enter a movie Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine("A movie Id can only be a number!");
                RunGetMovie();
            }

            try
            {
                rest.Delete<Movie>($"movie/{id}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Movie with the enetered ID does not exists!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine("Error occoured, please try again!");
            }
        }

        #endregion

        #region MovieHall

        private void RunGetMovieHalls()
        {
            var movieHallss = rest.Get<MovieHall>("moviehall");

            foreach (var movieHall in movieHallss)
            {
                Console.WriteLine(movieHall);
            }
        }

        private void RunGetMovieHall()
        {
            Console.WriteLine("Please enter a movie hall Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A movie hall Id can only be a number!");
                RunGetMovieHall();
            }

            var movieHall = rest.GetSingle<MovieHall>($"moviehall/{id}");

            Console.WriteLine(movieHall);
        }
        private void RunDeleteMovieHall()
        {
            Console.WriteLine("Please enter a movie hall Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine("A movie hall Id can only be a number!");
                RunGetMovie();
            }

            try
            {
                rest.Delete<MovieHall>($"moviehall/{id}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Movie hall with the enetered ID does not exists!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine("Error occoured, please try again!");
            }
        }
        #endregion

        #region Seat
        private void RunGetSeats()
        {
            var seats = rest.Get<Seats>("seats");

            foreach (var seat in seats)
            {
                Console.WriteLine(seat);
            }
        }

        private void RunGetSeat()
        {
            Console.WriteLine("Please enter a seat Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A seat Id can only be a number!");
                RunGetSeat();
            }

            var seat = rest.GetSingle<Seats>($"seats/{id}");

            Console.WriteLine(seat);
        }
        #endregion

        #region Showtime
        private void RunGetShowtimes()
        {
            var showtimes = rest.Get<Showtime>("showtime");

            foreach (var showtime in showtimes)
            {
                Console.WriteLine(showtime);
            }
        }

        private void RunGetShowtime()
        {
            Console.WriteLine("Please enter a showtime Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A showtime Id can only be a number!");
                RunGetShowtime();
            }

            var showtime = rest.GetSingle<Showtime>($"showtime/{id}");

            Console.WriteLine(showtime);
        }
        #endregion

        #region Staff
        private void RunGetStaffs()
        {
            var staffs = rest.Get<Staff>("staff");

            foreach (var staff in staffs)
            {
                Console.WriteLine(staff);
            }
        }

        private void RunGetStaff()
        {
            Console.WriteLine("Please enter a staff Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A staff Id can only be a number!");
                RunGetStaff();
            }

            var staff = rest.GetSingle<Staff>($"staff/{id}");

            Console.WriteLine(staff);
        }

        #endregion

        #region Ticket
        private void RunGetTickets()
        {
            var tickets = rest.Get<Ticket>("movie");

            foreach (var movie in tickets)
            {
                Console.WriteLine(movie);
            }
        }

        private void RunGetTicket()
        {
            Console.WriteLine("Please enter a movie Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("A movie Id can only be a number!");
                RunGetTicket();
            }

            var movie = rest.GetSingle<Ticket>($"movie/{id}");

            Console.WriteLine(movie);
        }

        #endregion

        #endregion
    }
}
