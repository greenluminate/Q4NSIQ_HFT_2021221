using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StaffController : GenericController<Staff>
    {
        IStaffLogic staffLogic;

        public StaffController(IStaffLogic staffLogic)
        : base(staffLogic as ILogic<Staff>)
        {
            this.staffLogic = staffLogic;
        }

        [HttpGet("[action]/{name}")]
        public IEnumerable<Staff> GetByName(string name)
        {
            return staffLogic.ReadByName(name);
        }

        [HttpGet("[action]")]
        public IEnumerable<KeyValuePair<string, int>>
        CountOfSoldTicketsByStaff()
        {
            return staffLogic.CountOfSoldTicketsByStaff();
        }

        [HttpGet("[action]")]
        public IEnumerable<KeyValuePair<string, int>>
        SUMPriceOfSoldTicketsByStaff()
        {
            return staffLogic.SUMPriceOfSoldTicketsByStaff();
        }

        [HttpGet("[action]")]
        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        TopSoldTicketsByStaffPerMovie()
        {
            return staffLogic.TopSoldTicketsByStaffPerMovie();
        }

        [HttpGet("[action]")]
        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>>
        SoldTicketsByStaffPerHallType()
        {
            return staffLogic.SoldTicketsByStaffPerHallType();
        }
    }
}
