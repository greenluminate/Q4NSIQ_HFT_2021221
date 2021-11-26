using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieHallController : GenericController<MovieHall>
    {
        IMovieHallLogic movieHallLogic;

        public MovieHallController(IMovieHallLogic movieHallLogic)
        : base(movieHallLogic as ILogic<MovieHall>)
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
