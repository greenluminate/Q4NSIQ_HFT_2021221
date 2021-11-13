using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface ISeatsLogic
    {
        IEnumerable<Seats> ReadByMovieHallId(int id);
    }
}