using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface ITicketLogic : ILogic<Ticket>
    {
        IEnumerable<KeyValuePair<Seats, int>> Top10MostUsedSeats();
    }
}