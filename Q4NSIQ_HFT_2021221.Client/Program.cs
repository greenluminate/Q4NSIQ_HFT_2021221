using System;

namespace Q4NSIQ_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("POSTMAN invite link:");
            Console.WriteLine(@"https://app.getpostman.com/join-team?invite_code=2f276736ee8cf9143cfdbc071aaa1185&ws=fd1fa18d-e16e-4bb0-8387-0acc48a7c7f7");
            Console.WriteLine
            (@"
                                      ___________I____________
                                     ( _____________________ ()
                                   _.-'|                    ||
                               _.-'   ||       WELCOME      ||
              ______       _.-'       ||                    ||
             |      |_ _.-'           ||   CINEMA DATABASE  ||
             |      |_|_              ||                    ||
             |______|   `-._          ||         IS         ||
                /\          `-._      ||                    ||
               /  \             `-._  ||       LOADING      ||
              /    \                `-.|____________________||
             /      \                 ------------------------
            /________\___________________/________________\______"
            );
            System.Threading.Thread.Sleep(8000);
            
            MenuTasks menuHelper = new MenuTasks(new RestService(@"http://localhost:17133"));
            menuHelper.Start();
        }
    }
}
