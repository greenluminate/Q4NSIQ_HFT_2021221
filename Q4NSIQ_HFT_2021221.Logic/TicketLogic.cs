using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class TicketLogic : Logic<Ticket>, ITicketLogic
    {
        ITicketRepository ticketRepo;
        public TicketLogic(ITicketRepository ticketRepo)
        : base(ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public IEnumerable<Ticket> ReadByShowtimeId(int id)
        {
            return ticketRepo.ReadByShowtimeId(id);
        }

        public IEnumerable<KeyValuePair<Seats, int>> Top10MostUsedSeats()
        {
            return (from ticket in repo.ReadAll().ToList()
                    group ticket by ticket.Seat into seatGrp
                    let count = seatGrp.Count()
                    orderby count descending, seatGrp.Key.SeatId ascending
                    select new KeyValuePair<Seats, int>
                    (
                        seatGrp.Key,
                        count
                    )).Take(10).AsEnumerable();
        }
    }
}
