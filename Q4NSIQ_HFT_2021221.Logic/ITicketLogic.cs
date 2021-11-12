using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface ITicketLogic
    {
        void Create(Ticket Ticket);
        void Delete(int id);
        Ticket Read(int id);
        IEnumerable<Ticket> ReadAll();
        IEnumerable<KeyValuePair<Seats, int>> Top10MostUsedSeats();
        void Update(Ticket Ticket);
    }
}