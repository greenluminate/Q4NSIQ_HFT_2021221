using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<Ticket> ReadByShowtimeId(int id)
        {
            return dbSet.Where(ticket => ticket.ShowtimeId.Equals(id)).AsQueryable();
        }
    }
}
