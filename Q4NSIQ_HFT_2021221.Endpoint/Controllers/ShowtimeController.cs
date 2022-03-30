using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Endpoint.Services;
using Microsoft.AspNetCore.SignalR;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowtimeController : GenericController<Showtime>
    {
        IShowtimeLogic showtimeLogic;

        public ShowtimeController(IShowtimeLogic showtimeLogic, IHubContext<SignalRHub> hub)
        : base(showtimeLogic as ILogic<Showtime>, hub)
        {
            this.showtimeLogic = showtimeLogic;
        }

        [HttpGet("[action]/{date}")]
        public IEnumerable<Showtime> GetByDate(DateTime date)
        {
            return showtimeLogic.ReadByDate(date);
        }

        [HttpGet("[action]/")]
        public IEnumerable<Showtime> GetByDate(DateTime? date)
        {
            return showtimeLogic.ReadByDate(null);
        }
    }
}
