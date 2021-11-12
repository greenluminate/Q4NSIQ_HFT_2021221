﻿using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface ISeatsRepository
    {
        IQueryable<Seats> ReadByMovieHallId(int id);
    }
}