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
    public class MovieController : GenericController<Movie>
    {
        IMovieLogic movieLogic;

        public MovieController(IMovieLogic movieLogic)
        : base(movieLogic as ILogic<Movie>)
        {
            this.movieLogic = movieLogic;
        }

        [HttpGet("{title}")]

        public IEnumerable<Movie> GetByTitle(string title)
        {
            return movieLogic.ReadByTitle(title);
        }
    }
}
