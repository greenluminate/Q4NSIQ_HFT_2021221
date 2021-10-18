using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    interface ISeatsRepository
    {
        void Create(Seats seats);
        void Delete(int id);
        Seats Read(int id);
        IQueryable<Seats> ReadAll();
        void Update(Seats seats);
    }
}