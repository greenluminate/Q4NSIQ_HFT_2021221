using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    class TicketRepository
    {
        CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }

        public Ticket Read(int id)
        {
            return db.Tickets.FirstOrDefault(t => t.TicketId == id);
        }

        public IQueryable<Ticket> ReadAll()
        {
            return db.Tickets;
        }

        public void Update(Ticket ticket)
        {
            var oldTicket = Read(ticket.TicketId);
            oldTicket.PaymentMethod = ticket.PaymentMethod;
            oldTicket.Price = ticket.Price;
            oldTicket.ShowtimeId = ticket.ShowtimeId;
            oldTicket.StaffId = ticket.StaffId;
            oldTicket.TicketId = ticket.TicketId;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
