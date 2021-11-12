using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface ISeatsLogic
    {
        void Create(Seats Seats);
        void Delete(int id);
        Seats Read(int id);
        IEnumerable<Seats> ReadAll();
        void Update(Seats Seats);
    }
}