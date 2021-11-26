using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Client
{
    public class MenuTasks//MenuInteraction Külön? Bekér kiír? Lehet nem jó ötlet.
    {
        RestService rest;
        public MenuTasks()
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

            MenuDriver mainMenu = new MenuDriver(prompt, options);

            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunSubChoice<Movie>();
                    break;
                case 1:
                    RunSubChoice<MovieHall>();
                    break;
                case 2:
                    RunSubChoice<Seats>();
                    break;
                case 3:
                    RunSubChoice<Showtime>();
                    break;
                case 4:
                    RunSubChoice<Staff>();
                    break;
                case 5:
                    RunSubChoice<Ticket>();
                    break;
                case 6:
                    ExitCinemaDatabase();
                    break;
            }
        }

        public void RunSubMenu<T>(List<string> options, string prompt)
        {
            MenuDriver mainMenu = new MenuDriver(prompt, options);

            int selectedIndex = mainMenu.Run();

            Console.Clear();

            RunSwitchCase<T>(selectedIndex);

            Console.WriteLine("\nPress any key to exit to the Movie menu...");
            Console.ReadKey(true);

            RunSubChoice<T>();
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
        private void RunSubChoice<T>()
        {
            switch (typeof(T).Name)
            {
                case "Movie":
                    RunMovieChoice();
                    break;
                case "MovieHall":
                    RunMovieHallChoice();
                    break;
                case "Seats":
                    RunSeatsChoice();
                    break;
                case "Showtime":
                    RunShowtimeChoice();
                    break;
                case "Staff":
                    RunStaffChoice();
                    break;
                case "Ticket":
                    RunTicketChoice();
                    break;
            }
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
                    RunUpdate<T>();
                    break;
                case 3:
                    RunCreate<T>();
                    break;
                case 4:
                    RunDelete<T>();
                    break;
                default:
                    switch (typeof(T).Name)
                    {
                        case "Movie":
                            RunMovieSwitchCase(selectedIndex);
                            break;
                        case "MovieHall":
                            RunMovieHallSwitchCase(selectedIndex);
                            break;
                        case "Seats":
                            RunSeatsSwitchCase(selectedIndex);
                            break;
                        case "Showtime":
                            RunShowtimeSwitchCase(selectedIndex);
                            break;
                        case "Staff":
                            RunStaffSwitchCase(selectedIndex);
                            break;
                        case "Ticket":
                            RunTicketSwitchCase(selectedIndex);
                            break;
                    }
                    break;
            }
        }

        #region RunUniqueSwitchCase
        private void RunMovieSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    MovieReadByTitle();
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }

        private void RunMovieHallSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    MovieHallReadByCategory();
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }

        private void RunSeatsSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    SeatsReadByMovieHallId();
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }
        private void RunShowtimeSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    ShowtimeReadByDate();
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }

        private void RunStaffSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    StaffReadByName();
                    break;
                case 6:
                    StaffCountOfSoldTicketsByStaff();
                    break;
                case 7:
                    StaffSUMPriceOfSoldTicketsByStaff();
                    break;
                case 8:
                    StaffTopSoldTicketsByStaffPerMovie();
                    break;
                case 9:
                    StaffSoldTicketsByStaffPerHallType();
                    break;
                case 10:
                    RunMainMenu();
                    break;
            }
        }

        private void RunTicketSwitchCase(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 5:
                    TicketReadByShowtimeId();
                    break;
                case 6:
                    TicketTop10MostUsedSeats();
                    break;
                case 7:
                    RunMainMenu();
                    break;
            }
        }
        #endregion

        private void ConsoleWriter<T>(IEnumerable<T> entities)
        {
            Type type = typeof(T);
            var properties = type.GetProperties().Where(prop => prop.GetCustomAttribute<NotMappedAttribute>() is null);
            foreach (var property in properties)
            {
                Console.Write(property.Name + "\t");
            }
            Console.Write("\n");

            foreach (var entity in entities)
            {
                foreach (var property in properties)
                {
                    Console.Write(property.GetValue(entity) + "\t");
                }
                Console.Write("\n");
            }
        }//Ha class a models rétegemből a type, akkor írja ki annak is foreac-csel az adatait.

        #region RunGenericChoices
        private void RunGet<T>()
        {
            Console.WriteLine($"Please enter a {typeof(T).Name} Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine($"A {typeof(T).Name} Id can only be a number!");
                RunGet<T>();
            }

            var entity = rest.GetSingle<T>($"{typeof(T).Name.ToLower()}/{id}");

            ConsoleWriter<T>(new List<T>() { (T)entity });
        }

        private void RunGets<T>()
        {
            Type type = typeof(T);
            var entities = rest.Get<T>(type.Name.ToLower());

            ConsoleWriter(entities);
        }

        private void RunCreate<T>()
        {
            Type type = typeof(T);
            T newEntity = (T)Activator.CreateInstance(type);
            var propertiesAll = type.GetProperties();
            var properties = propertiesAll.Where(p => !p.PropertyType.AssemblyQualifiedName.Contains("ICollection") &&
                                                      !p.PropertyType.AssemblyQualifiedName.Contains(".Models")).ToArray();

            Console.WriteLine($"New {type.Name.ToLower()} - Please enter the following properties:");
            for (int i = 1; i < properties.Length; i++)
            {
                var property = properties[i];

                Console.WriteLine(property.Name);

                var propertyType = property.PropertyType;
                var parser = propertyType.GetMethods().FirstOrDefault(t => t.Name == "Parse");

                bool doParse = true;
                do
                {
                    string value = Console.ReadLine().Trim();
                    if (parser != null)
                    {
                        try
                        {
                            var converted = parser
                            .Invoke(null, new[] { value });

                            property.SetValue(newEntity, converted);

                            doParse = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.InnerException.Message);
                            Console.WriteLine($"Please reenter the coveted {property.Name} value!");
                        }
                    }
                    else
                    {
                        if (value == "" && Nullable.GetUnderlyingType(propertyType) != null)
                        {
                            property = null;
                        }
                        else
                        {
                            property.SetValue(newEntity, value);
                        }
                        doParse = false;
                    }

                } while (doParse);
            }

            rest.Post(newEntity, $"{type.Name.ToLower()}");

            //It displays the last entity; Equals is not an option, because of the ID field in the hashes.
            var entitites = rest.Get<T>($"{type.Name.ToLower()}");
            var entityCount = entitites.Count;
            ConsoleWriter(new List<T>() { entitites[entityCount - 1] });
            Console.WriteLine("\nIf this is not the one record you have provided to the database,\nsomething went wreong during the Creation.");
        }

        private void RunUpdate<T>()
        {
            Type type = typeof(T);

            Console.WriteLine($"Update {type.Name.ToLower()} - Please enter the {type.Name.ToLower()}'s If You want to update!");

            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine($"A {typeof(T).Name} Id can only be a number!");
                RunUpdate<T>();
            }

            var entity = rest.GetSingle<T>($"{typeof(T).Name.ToLower()}/{id}");

            if (entity is null)
            {
                Console.WriteLine($"Please press 'y' if You want to enter another {typeof(T).Name.ToLower()} Id!");
                Console.WriteLine($"Please press any other button if You want to exit to the main menu!");
                string nextTask = Console.ReadKey().ToString();
                if (nextTask.Trim() == "y")
                {
                    RunUpdate<T>();
                }
                else
                {
                    RunMainMenu();
                }
            }

            var propertiesAll = type.GetProperties();
            var properties = propertiesAll.Where(p => !p.PropertyType.AssemblyQualifiedName.Contains("ICollection") &&
                                                      !p.PropertyType.AssemblyQualifiedName.Contains(".Models")).ToArray();

            Console.WriteLine("Instructions");
            Console.WriteLine("Delete:\n\tPlease enter \"del\" if you vant to delete a not necessary value.");
            Console.WriteLine("Leave:\n\tPlease leave it blank if you want to leave the property value as it is.");
            Console.WriteLine("Update:\n\tPlease enter the property value if you want to update it.");

            Console.WriteLine($"\nUpdate {type.Name.ToLower()} with the given Id: {id}\nPlease enter the following properties then press enter:");
            for (int i = 1; i < properties.Length; i++)
            {
                var property = properties[i];
                var value = entity.GetType().GetProperty(property.Name).GetValue(entity);
                var isRequired = property.GetCustomAttribute<RequiredAttribute>() != null;
                if (isRequired)
                {
                    Console.WriteLine($"Property: {property.Name} CurrentValue: {value} [VALUE IS NECESSARY]");
                }
                else
                {
                    Console.WriteLine($"Property: {property.Name} CurrentValue: {value}");
                }
                Console.WriteLine("New value:");

                var propertyType = property.PropertyType;
                var parser = propertyType.GetMethods().FirstOrDefault(t => t.Name == "Parse");

                bool doParse = true;
                do
                {
                    string inputValue = Console.ReadLine().Trim();
                    if (parser != null)
                    {
                        try
                        {
                            var converted = parser
                            .Invoke(null, new[] { inputValue });

                            property.SetValue(entity, converted);

                            doParse = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.InnerException.Message);
                            Console.WriteLine($"Please reenter the coveted {property.Name} value!");
                        }
                    }
                    else
                    {
                        if (inputValue == "del" && Nullable.GetUnderlyingType(propertyType) != null)
                        {
                            property = null;
                        }
                        else if (inputValue == "")
                        {
                            property.SetValue(entity, value);
                        }
                        else
                        {
                            property.SetValue(entity, inputValue);
                        }
                        doParse = false;
                    }

                } while (doParse);
            }

            Console.WriteLine("You have updated this record:\n\t");
            ConsoleWriter(new List<T>() { rest.GetSingle<T>($"{typeof(T).Name.ToLower()}/{id}") });

            rest.Put(entity, $"{type.Name.ToLower()}");


            Console.WriteLine("\nTo this record:\n\t");
            ConsoleWriter(new List<T>() { rest.GetSingle<T>($"{typeof(T).Name.ToLower()}/{id}") });

            Console.WriteLine("\nIf there is no difference, something went wreong during the update.");
        }

        private void RunDelete<T>()
        {
            Console.WriteLine($"Please enter a {typeof(T).Name.ToLower()} Id!");
            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine().Trim());
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

        #region RunUniqueCRUDChoices
        private void MovieReadByTitle()
        {
            Console.WriteLine($"Please enter a movie title!");

            string movieTitle = Console.ReadLine().Trim();

            var movies = rest.Get<Movie>($"movie/GetByTitle/{movieTitle}");

            ConsoleWriter(movies);
        }
        private void MovieHallReadByCategory()
        {
            Console.WriteLine($"Please enter a movie hall category!");

            string hallCategory = Console.ReadLine().Trim();

            var halls = rest.Get<MovieHall>($"moviehall/GetByCategory/{hallCategory}");

            ConsoleWriter(halls);
        }

        private void SeatsReadByMovieHallId()
        {
            Console.WriteLine($"Please enter a movie hall Id!");

            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine($"A movie hall Id can only be a number!");
                SeatsReadByMovieHallId();
            }

            var seats = rest.Get<Seats>($"seats/GetByMovieHallId/{id}");

            ConsoleWriter(seats);
        }

        private void ShowtimeReadByDate()
        {
            Console.WriteLine($"Please enter a date! E.g.: 2021.12.14 || 2021,12,14 || 2021-12-14");

            string date = Console.ReadLine().Trim();

            IEnumerable<Showtime> shows;
            if (date == "")
            {
                date = null;
                shows = rest.Get<Showtime>($"showtime/GetByDate/");
            }
            else
            {
                try
                {
                    DateTime.Parse(date);
                }
                catch (Exception)
                {
                    Console.WriteLine($"A date can look like: 2021.12.14 or 2021,12,14 or 2021-12-14!");
                    ShowtimeReadByDate();
                }

                shows = rest.Get<Showtime>($"showtime/GetByDate/{date}");
            }

            ConsoleWriter(shows);
        }

        private void StaffReadByName()
        {
            Console.WriteLine($"Please enter a staff name!");

            string name = Console.ReadLine().Trim();

            var staffs = rest.Get<Staff>($"staff/GetByName/{name}");

            ConsoleWriter(staffs);
        }

        private void TicketReadByShowtimeId()
        {
            Console.WriteLine($"Please enter a showtime Id!");

            int id = 0;
            try
            {
                id = int.Parse(Console.ReadLine().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine($"A showtime Id can only be a number!");
                TicketReadByShowtimeId();
            }

            var tickets = rest.Get<Ticket>($"ticket/GetByShowtimeId/{id}");

            ConsoleWriter(tickets);
        }
        #endregion

        #region RunNONECRUDChoices
        private void StaffCountOfSoldTicketsByStaff()
        {
            var entities = rest.Get<KeyValuePair<string, int>>("staff/CountOfSoldTicketsByStaff");

            ConsoleWriter(entities);
        }

        private void StaffSUMPriceOfSoldTicketsByStaff()
        {
            var entities = rest.Get<KeyValuePair<string, int>>("staff/SUMPriceOfSoldTicketsByStaff");

            ConsoleWriter(entities);
        }

        private void StaffTopSoldTicketsByStaffPerMovie()
        {
            var entities = rest.Get<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>("staff/TopSoldTicketsByStaffPerMovie");

            ConsoleWriter(entities);
        }

        private void StaffSoldTicketsByStaffPerHallType()
        {
            var entities = rest.Get<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>("staff/SoldTicketsByStaffPerHallType");

            ConsoleWriter(entities);
        }

        private void TicketTop10MostUsedSeats()
        {
            var entities = rest.Get<KeyValuePair<Seats, int>>("ticket/Top10MostUsedSeats");

            ConsoleWriter(entities);
        }
        #endregion
    }
}
