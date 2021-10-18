using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    interface IShowtimeRepository
    {
        void Create(Showtime showtime);
        void Delete(int id);
        Showtime Read(int id);
        IQueryable<Showtime> ReadAll();
        void Update(Showtime showtime);
    }
}