using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class TicketLogic : Logic<Ticket>, ITicketLogic
    {
        public TicketLogic(IRepository<Ticket> ticketRepo) : base(ticketRepo) { }

        public IEnumerable<KeyValuePair<Seats, int>> Top10MostUsedSeats()
        {
            return (from ticket in repo.ReadAll().ToList()
                    group ticket by ticket.Seat into seatGrp
                    let count = seatGrp.Count()
                    orderby count
                    select new KeyValuePair<Seats, int>
                    (
                        seatGrp.Key,
                        count
                    )).Take(10);
        }
    }
}
