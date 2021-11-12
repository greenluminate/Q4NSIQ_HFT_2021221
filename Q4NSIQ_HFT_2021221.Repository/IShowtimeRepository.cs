using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IShowtimeRepository
    {
        IQueryable<Showtime> ReadByDate(DateTime date);
    }
}