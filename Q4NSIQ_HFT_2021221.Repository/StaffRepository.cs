using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        public StaffRepository(CinemaDbContext db) : base(db) { }

        public IQueryable<Staff> ReadByName(string name)
        {
            return dbSet.Where(staff => staff.Name.Equals(name)).AsQueryable();
        }
    }
}
