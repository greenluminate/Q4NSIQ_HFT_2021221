using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    class TicketLogic
    {
        ITicketRepository ticketRepo;

        public TicketLogic(ITicketRepository ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public void Create(Ticket Ticket)
        {
            ticketRepo.Create(Ticket);
        }

        public Ticket Read(int id)
        {
            return ticketRepo.Read(id);
        }

        public IEnumerable<Ticket> ReadAll()
        {
            return ticketRepo.ReadAll();
        }

        public void Update(Ticket Ticket)
        {
            ticketRepo.Update(Ticket);
        }

        public void Delete(int id)
        {
            ticketRepo.Delete(id);
        }
    }
}
