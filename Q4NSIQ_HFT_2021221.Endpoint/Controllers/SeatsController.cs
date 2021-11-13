using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeatsController : GenericController<Seats>
    {
        ISeatsLogic seatsLogic;

        public SeatsController(ISeatsLogic seatsLogic)
        : base(seatsLogic as ILogic<Seats>)
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
