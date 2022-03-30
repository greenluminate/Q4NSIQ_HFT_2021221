using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;
using Microsoft.AspNetCore.SignalR;
using Q4NSIQ_HFT_2021221.Endpoint.Services;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeatsController : GenericController<Seats>
    {
        ISeatsLogic seatsLogic;

        public SeatsController(ISeatsLogic seatsLogic, IHubContext<SignalRHub> hub)
        : base(seatsLogic as ILogic<Seats>, hub)
        {
            this.seatsLogic = seatsLogic;
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<Seats> GetByMovieHallId(int id)
        {
            return seatsLogic.ReadByMovieHallId(id);
        }
    }
}
