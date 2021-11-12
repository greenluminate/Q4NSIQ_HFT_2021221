using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public class StaffLogic
    {
        IStaffRepository staffRepo;

        public StaffLogic(IStaffRepository staffRepo)
        {
            this.staffRepo = staffRepo;
        }

        public void Create(Staff Staff)
        {
            staffRepo.Create(Staff);
        }

        public Staff Read(int id)
        {
            return staffRepo.Read(id);
        }

        public IEnumerable<Staff> ReadAll()
        {
            return staffRepo.ReadAll();
        }

        public void Update(Staff Staff)
        {
            staffRepo.Update(Staff);
        }

        public void Delete(int id)
        {
            staffRepo.Delete(id);
        }
    }
}
