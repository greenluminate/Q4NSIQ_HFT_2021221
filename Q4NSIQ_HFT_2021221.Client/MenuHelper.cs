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
                    RunChoice<Movie>();
                    break;
                case 1:
                    RunChoice<MovieHall>();
                    break;
                case 2:
                    RunChoice<Seats>();
                    break;
                case 3:
                    RunChoice<Showtime>();
                    break;
                case 4:
                    RunChoice<Staff>();
                    break;
                case 5:
                    RunChoice<Ticket>();
                    break;
                case 6:
                    ExitCinemaDatabase();
                    break;
            }
        }

        public void RunSubMenu<T>(List<string> options, string prompt)
        {
            Menu mainMenu = new Menu(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            RunSwitchCase<T>(selectedIndex);

            Console.WriteLine("\nPress any key to exit to the Movie menu...");
            Console.ReadKey(true);

            RunChoice<T>();
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

            RunSubMenu<Movie>(RunUniqueOptionsGenerator("movie", uniqueOptions), RunUniquePromptGenerator("Movie"));
        }
        private void RunMovieHallChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show movie hall(s) with given: Category" };

            RunSubMenu<MovieHall>(RunUniqueOptionsGenerator("movie hall", uniqueOptions), RunUniquePromptGenerator("Movie hall"));
        }
        private void RunSeatsChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show seat(s) with given: Movie Hall Id" };

            RunSubMenu<Seats>(RunUniqueOptionsGenerator("seat", uniqueOptions), RunUniquePromptGenerator("Seat"));
        }
        private void RunShowtimeChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show showtime(s) at given: Date" };

            RunSubMenu<Showtime>(RunUniqueOptionsGenerator("showtime", uniqueOptions), RunUniquePromptGenerator("Showtime"));
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

            RunSubMenu<Staff>(RunUniqueOptionsGenerator("staff", uniqueOptions), RunUniquePromptGenerator("Staff"));
        }
        private void RunTicketChoice()
        {
            List<string> uniqueOptions = new List<string>() { "Show tickets wit given: Showtime Id", "Show the Top 10 Most Used Seats" };

            RunSubMenu<Ticket>(RunUniqueOptionsGenerator("ticket", uniqueOptions), RunUniquePromptGenerator("Ticket"));
        }
        #endregion
        private void RunChoice<T>()
        {
            List<string> uniqueOptions = new List<string>() { $"Show {typeof(T).Name.ToLower()}s with given: Title" };

            RunSubMenu<T>(RunUniqueOptionsGenerator($"{typeof(T).Name}", uniqueOptions), RunUniquePromptGenerator($"{typeof(T).Name}"));
        }

        private string RunUniquePromptGenerator(string objectName)
        {
            string prompt = $"{objectName} - Please choose a task You want to performe!\nPress: Up, Down Arrows and Enter keys";

            return prompt;
        }
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

        private void RunSwitchCase<T>(int selectedIndex)
        {

            switch (selectedIndex)
            {
                case 0:
                    RunGet<T>();
                    break;
                case 1:
                    RunGets<T>();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    RunDelete<T>();
                    break;
                case 5:
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }

        #region RunGenericChoices
        private void RunGet<T>()
        {
            Console.WriteLine($"Please enter a {typeof(T).Name} Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine($"A {typeof(T).Name} Id can only be a number!");
                RunGet<T>();
            }

            var entity = rest.GetSingle<T>($"{typeof(T).Name.ToLower()}/{id}");

            Console.WriteLine(entity);
        }

        private void RunGets<T>()
        {
            Type type = typeof(T);
            var entities = rest.Get<T>(type.Name.ToLower());

            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        private void RunDelete<T>()
        {
            Console.WriteLine($"Please enter a {typeof(T).Name.ToLower()} Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine($"A {typeof(T).Name.ToLower()} Id can only be a number!");
                RunDelete<T>();
            }

            try
            {
                rest.Delete<Movie>($"{typeof(T).Name.ToLower()}/{id}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"{typeof(T).Name} with the enetered Id does not exists!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine("Error occoured, please try again!");
            }
        }
        #endregion
    }
}
