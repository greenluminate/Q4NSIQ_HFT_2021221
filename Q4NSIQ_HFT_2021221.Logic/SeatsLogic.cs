using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class SeatsLogic : ISeatsLogic
    {
        ISeatsRepository seatsRepo;

        public SeatsLogic(ISeatsRepository seatsRepo)
        {
            this.seatsRepo = seatsRepo;
        }

        public void Create(Seats Seats)
        {
            seatsRepo.Create(Seats);
        }

        public Seats Read(int id)
        {
            return seatsRepo.Read(id);
        }

        public IEnumerable<Seats> ReadAll()
        {
            return seatsRepo.ReadAll();
        }

        public void Update(Seats Seats)
        {
            seatsRepo.Update(Seats);
        }

        public void Delete(int id)
        {
            seatsRepo.Delete(id);
        }
    }
}
