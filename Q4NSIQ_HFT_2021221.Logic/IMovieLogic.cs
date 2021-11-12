using Q4NSIQ_HFT_2021221.Models;
using System.Collections.Generic;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface IMovieLogic
    {
        void Create(Movie movie);
        void Delete(int id);
        Movie Read(int id);
        IEnumerable<Movie> ReadAll();
        void Update(Movie movie);
    }
}