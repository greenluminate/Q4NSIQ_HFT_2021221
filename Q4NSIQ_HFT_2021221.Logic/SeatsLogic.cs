using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class SeatsLogic : Logic<Seats>, ISeatsLogic
    {
        ISeatsRepository seatsRepo;
        public SeatsLogic(ISeatsRepository seatsRepo)
        : base(seatsRepo)
        {
            this.seatsRepo = seatsRepo;
        }

        public IEnumerable<Seats> ReadByMovieHallId(int id)
        {
            return seatsRepo.ReadByMovieHallId(id);
        }
    }
}
