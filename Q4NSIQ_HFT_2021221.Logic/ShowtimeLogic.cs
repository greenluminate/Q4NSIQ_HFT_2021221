using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class ShowtimeLogic : Logic<Showtime>, IShowtimeLogic
    {
        IShowtimeRepository showtimeRepo;
        public ShowtimeLogic(IShowtimeRepository showtimeRepo)
        : base(showtimeRepo)
        {
            this.showtimeRepo = showtimeRepo;
        }

        public IEnumerable<Showtime> ReadByDate(DateTime? date)
        {
            return showtimeRepo.ReadByDate(date);
        }
    }
}
