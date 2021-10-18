using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    class ShowtimeLogic
    {
        IShowtimeRepository showtimeRepo;

        public ShowtimeLogic(IShowtimeRepository showtimeRepo)
        {
            this.showtimeRepo = showtimeRepo;
        }

        public void Create(Showtime Showtime)
        {
            showtimeRepo.Create(Showtime);
        }

        public Showtime Read(int id)
        {
            return showtimeRepo.Read(id);
        }

        public IEnumerable<Showtime> ReadAll()
        {
            return showtimeRepo.ReadAll();
        }

        public void Update(Showtime Showtime)
        {
            showtimeRepo.Update(Showtime);
        }

        public void Delete(int id)
        {
            showtimeRepo.Delete(id);
        }
    }
}
