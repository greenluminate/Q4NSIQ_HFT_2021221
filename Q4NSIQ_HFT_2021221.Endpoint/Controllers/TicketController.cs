using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TicketController : GenericController<Ticket>
    {
        ITicketLogic ticketLogic;

        public TicketController(ITicketLogic ticketLogic)
        : base(ticketLogic as ILogic<Ticket>)
        {
            this.ticketLogic = ticketLogic;
        }

        [HttpGet("{id}")]
        public IEnumerable<Ticket> GetByShowtimeId(int id)
        {
            return ticketLogic.ReadByShowtimeId(id);
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<Seats, int>> Top10MostUsedSeats()
        {
            return ticketLogic.Top10MostUsedSeats();

        }
    }
}

