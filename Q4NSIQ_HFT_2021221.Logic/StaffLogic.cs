using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class StaffLogic : Logic<Staff>, IStaffLogic
    {
        IStaffRepository staffRepo;
        IRepository<MovieHall> movieHallRepo;
        IRepository<Movie> movieRepo;

        public StaffLogic(IStaffRepository staffRepo, IRepository<MovieHall> movieHallRepo, IRepository<Movie> movieRepo)
        : base(staffRepo)
        {
            this.staffRepo = staffRepo;
            this.movieHallRepo = movieHallRepo;
            this.movieRepo = movieRepo;
        }

        public IEnumerable<Staff> ReadByName(string name)
        {
            return staffRepo.ReadByName(name);
        }

        public IEnumerable<KeyValuePair<string, int>>
        CountOfSoldTicketsByStaff()
        {
            return from staff in repo.ReadAll()
                   orderby staff.Tickets.Count() descending, staff.Name ascending
                   let count = staff.Tickets.Count()
                   select new KeyValuePair<string, int>
                   (staff.Name, count);
        }

        public IEnumerable<KeyValuePair<string, int>>
        SUMPriceOfSoldTicketsByStaff()
        {
            return (from staff in repo.ReadAll().ToArray()
                    orderby staff.Tickets.Sum(x => x.Price) descending, staff.Name ascending
                    select new KeyValuePair<string, int>
                    (staff.Name, staff.Tickets.Sum(x => x.Price))).ToArray();
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        TopSoldTicketsByStaffPerMovie()
        {
            return from staff in repo.ReadAll().ToArray()
                   let movieTitles = movieRepo.ReadAll().Select(m => m.MovieTitle).ToArray()
                   let moviesTitelsOrdered = movieTitles.OrderByDescending(m => staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == m).Count()).ToArray()
                   let maxCount = moviesTitelsOrdered.Select(mt => staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == mt).Count()).FirstOrDefault()
                   orderby staff.Name
                   select new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                   (
                       staff.Name,
                       moviesTitelsOrdered.Select(mt => new KeyValuePair<string, int>
                                                        (mt, staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == mt).Count()))
                                                        .Where(kv => kv.Value == maxCount && kv.Value != 0)
                   );
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        SoldTicketsByStaffPerHallType()
        {
            return from staff in repo.ReadAll().ToList()
                   let categories = movieHallRepo.ReadAll().Select(hall => hall.HallCategory).Distinct().OrderBy(c => c).ToList()
                   orderby staff.Name
                   select new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                   (
                       staff.Name,
                       categories.Select(c => new KeyValuePair<string, int>
                                              (c, staff.Tickets.Where(ticket => ticket.Showtime.MovieHall.HallCategory.Equals(c))
                                              .Count()))
                   );
        }
    }
}
