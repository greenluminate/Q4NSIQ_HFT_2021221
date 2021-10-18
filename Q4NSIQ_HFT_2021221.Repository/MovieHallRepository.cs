using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    class MovieHallRepository
    {
        CinemaDbContext db;

        public MovieHallRepository(CinemaDbContext db)
        {
            this.db = db;
        }
    }
}
