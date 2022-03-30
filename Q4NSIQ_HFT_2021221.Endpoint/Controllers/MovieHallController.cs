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
    public class MovieHallController : GenericController<MovieHall>
    {
        IMovieHallLogic movieHallLogic;

        public MovieHallController(IMovieHallLogic movieHallLogic, IHubContext<SignalRHub> hub)
        : base(movieHallLogic as ILogic<MovieHall>, hub)
        {
            this.movieHallLogic = movieHallLogic;
        }

        [HttpGet("[action]/{category}")]

        public IEnumerable<MovieHall> GetByCategory(string category)
        {
            return movieHallLogic.ReadByCategory(category);
        }
    }
}
