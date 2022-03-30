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
    public class MovieController : GenericController<Movie>
    {
        IMovieLogic movieLogic;
        public MovieController(IMovieLogic movieLogic, IHubContext<SignalRHub> hub)
        : base(movieLogic as ILogic<Movie>, hub)
        {
            this.movieLogic = movieLogic;
        }

        [HttpGet("[action]/{title}")]

        public IEnumerable<Movie> GetByTitle(string title)
        {
            return movieLogic.ReadByTitle(title);
        }
    }
}
