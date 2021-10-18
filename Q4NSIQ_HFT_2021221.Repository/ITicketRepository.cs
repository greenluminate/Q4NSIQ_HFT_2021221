using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    interface ITicketRepository
    {
        void Create(Ticket ticket);
        void Delete(int id);
        Ticket Read(int id);
        IQueryable<Ticket> ReadAll();
        void Update(Ticket ticket);
    }
}