using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using database;

namespace LogOutProCesS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Server LogOut check");
            while (true)
            {
                Console.WriteLine("Beginning logout loop");
                Console.WriteLine("Updating lastlogin for players.... at " + DateTime.Now);
                foreach (Players p in Players.GetAll())
                {
                    if (DateTime.Compare(p.LastLogin.AddMinutes(10), DateTime.Now) < 0 && p.LastLogin != new DateTime())
                    {
                        Console.WriteLine("User logedOut: Player " + p.ScreenName + "   son last login : " + p.LastLogin);
                        Players.LogOff(p.Id);
                    }
                }
                Console.WriteLine("Deleting anonymous users..");
                foreach (Anon anon in Anon.GetAll())
                {
                    if (DateTime.Compare(anon.LastLogin.AddMinutes(10), DateTime.Now) < 0)
                    {
                        Console.WriteLine("User logedOut: Anonymous n°" + anon.Id);
                        Anon.LogOut(anon.Id);
                    }
                }
                Console.WriteLine("Waiting for 10 minutes until new loop...");
                Thread.Sleep(600000);
            }
        }
    }
}
