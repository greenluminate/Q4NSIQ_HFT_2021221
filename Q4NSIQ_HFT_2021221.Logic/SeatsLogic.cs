using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System.Collections.Generic;

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
