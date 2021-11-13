using Q4NSIQ_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface IShowtimeLogic
    {
        IEnumerable<Showtime> ReadByDate(DateTime? date);
    }
}