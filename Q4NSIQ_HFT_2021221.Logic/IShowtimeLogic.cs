using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface IShowtimeLogic
    {
        void Create(Showtime Showtime);
        void Delete(int id);
        Showtime Read(int id);
        IEnumerable<Showtime> ReadAll();
        void Update(Showtime Showtime);
    }
}