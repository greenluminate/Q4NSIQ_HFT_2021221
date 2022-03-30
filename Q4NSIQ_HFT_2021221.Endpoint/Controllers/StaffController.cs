using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Logic;
using Q4NSIQ_HFT_2021221.Models;
using Microsoft.AspNetCore.SignalR;
using Q4NSIQ_HFT_2021221.Endpoint.Services;

namespace Q4NSIQ_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StaffController : GenericController<Staff>
    {
        IStaffLogic staffLogic;

        public StaffController(IStaffLogic staffLogic, IHubContext<SignalRHub> hub)
        : base(staffLogic as ILogic<Staff>, hub)
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
