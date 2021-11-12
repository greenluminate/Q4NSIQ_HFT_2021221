using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IStaffRepository : IRepository<Staff>
    {
        IQueryable<Staff> ReadByName(string name);
    }
}