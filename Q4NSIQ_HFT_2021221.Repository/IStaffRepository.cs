using Q4NSIQ_HFT_2021221.Models;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public interface IStaffRepository
    {
        void Create(Staff staff);
        void Delete(int id);
        Staff Read(int id);
        IQueryable<Staff> ReadAll();
        void Update(Staff staff);
    }
}