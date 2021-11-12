using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class StaffLogic
    {
        IStaffRepository staffRepo;
        IMovieHallRepository movieHallRepo;
        IMovieRepository movieRepo;

        public StaffLogic(IStaffRepository staffRepo, IMovieHallRepository movieHallRepo, IMovieRepository movieRepo)
        {
            this.staffRepo = staffRepo;
            this.movieHallRepo = movieHallRepo;
            this.movieRepo = movieRepo;
        }

        public void Create(Staff Staff)
        {
            staffRepo.Create(Staff);
        }

        public Staff Read(int id)
        {
            return staffRepo.Read(id);
        }

        public IEnumerable<Staff> ReadAll()
        {
            return staffRepo.ReadAll();
        }

        public void Update(Staff Staff)
        {
            staffRepo.Update(Staff);
        }

        public void Delete(int id)
        {
            staffRepo.Delete(id);
        }

        #region NON-CRUD

        public IEnumerable<KeyValuePair<string, int>>
        CountOfSoldTicketsByStaff()
        {
            return from staff in staffRepo.ReadAll()
                   orderby staff.Tickets.Count() descending, staff.Name ascending
                   let count = staff.Tickets.Count()
                   select new KeyValuePair<string, int>
                   (staff.Name, count);
        }

        public IEnumerable<KeyValuePair<string, int>>
        SUMPriceOfSoldTicketsByStaff()
        {
            return (from staff in staffRepo.ReadAll().ToArray()
                    orderby staff.Tickets.Sum(x => x.Price) descending, staff.Name ascending
                    select new KeyValuePair<string, int>
                    (staff.Name, staff.Tickets.Sum(x => x.Price))).ToArray();
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        TopSoldTicketsByStaffPerMovie()
        {
            return from staff in staffRepo.ReadAll().ToArray()
                   let movieTitles = movieRepo.ReadAll().Select(m => m.MovieTitle).ToArray()
                   let moviesTitelsOrdered = movieTitles.OrderByDescending(m => staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == m).Count()).ToArray()
                   let maxCount = moviesTitelsOrdered.Select(mt => staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == mt).Count()).FirstOrDefault()
                   orderby staff.Name
                   select new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                   (
                       staff.Name,
                       moviesTitelsOrdered.Select(mt => new KeyValuePair<string, int>
                                                        (mt, staff.Tickets.Where(t => t.Showtime.Movie.MovieTitle == mt).Count()))
                                                        .Where(kv => kv.Value == maxCount)
                   );
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        SoldTicketsByStaffPerHallType()
        {
            return from staff in staffRepo.ReadAll().ToList()
                   let categories = movieHallRepo.ReadAll().Select(hall => hall.HallCategory).Distinct().ToList()
                   orderby staff.Name, categories
                   select new KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>
                   (
                       staff.Name,
                       categories.Select(c => new KeyValuePair<string, int>
                                              (c, staff.Tickets.Where(ticket => ticket.Showtime.MovieHall.HallCategory.Equals(c))
                                              .Count()))
                   );
        }
        #endregion
    }
}
