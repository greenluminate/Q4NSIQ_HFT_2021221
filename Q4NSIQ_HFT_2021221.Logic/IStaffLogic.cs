using System.Collections.Generic;
using Q4NSIQ_HFT_2021221.Models;

namespace Q4NSIQ_HFT_2021221.Logic
{
    public interface IStaffLogic : ILogic<Staff>
    {
        IEnumerable<Staff> ReadByName(string name);
        IEnumerable<KeyValuePair<string, int>> CountOfSoldTicketsByStaff();
        IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> SoldTicketsByStaffPerHallType();
        IEnumerable<KeyValuePair<string, int>> SUMPriceOfSoldTicketsByStaff();
        IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, int>>>> TopSoldTicketsByStaffPerMovie();
    }
}