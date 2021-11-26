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
            Console.WriteLine("Hello!");
            Console.WriteLine(@"POSTMAN invite link: https://app.getpostman.com/join-team?invite_code=0279391ad6191a2a9dd581246b082782");
            System.Threading.Thread.Sleep(8000);

            MenuTasks menuHelper = new MenuTasks();
            menuHelper.Start();

            ;
        }
    }
}
