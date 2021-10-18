using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Data;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Repository
{
    class StaffRepository
    {
        CinemaDbContext db;

        public StaffRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public void Create(Staff staff)
        {
            db.Staffs.Add(staff);
            db.SaveChanges();
        }

        public Staff Read(int id)
        {
            return db.Staffs.FirstOrDefault(t => t.StaffId == id);
        }

        public IQueryable<Staff> ReadAll()
        {
            return db.Staffs;
        }

        public void Update(Staff staff)
        {
            var oldStaff = Read(staff.StaffId);
            oldStaff.Name = staff.Name;
            oldStaff.Gender = staff.Gender;
            oldStaff.IC = staff.IC;
            oldStaff.MobileNumber = staff.MobileNumber;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }
    }
}
