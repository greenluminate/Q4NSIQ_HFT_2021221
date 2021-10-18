using Q4NSIQ_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Logic
{
    class SeatsLogic
    {
        ISeatsRepository SeatsRepo;

        public SeatsLogic(ISeatsRepository SeatsRepo)
        {
            this.SeatsRepo = SeatsRepo;
        }
    }
}
