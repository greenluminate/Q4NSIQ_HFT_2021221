using Q4NSIQ_HFT_2021221.Models;
using Q4NSIQ_HFT_2021221.Client;
using System;
using System.Linq;

namespace Q4NSIQ_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);

            MenuTasks menuHelper = new MenuTasks();
            menuHelper.Start();

            ;
        }
    }
}
