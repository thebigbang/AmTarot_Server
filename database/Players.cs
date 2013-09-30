using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace database
{
    public partial class Players
    {
        public static bool Login(string username, string password)
        {
            try{
            tarotContainer t = new tarotContainer();
            if (t.Accounts.OfType<Players>().Single(p => p.Username == username).Pwd == password)
                return true;
                }catch(Exception e)
                {
                    Console.WriteLine("error: username doesn't exist.");
                }
            return false;
        }
        public static void Create(Players p)
        {
            tarotContainer t = new tarotContainer();
            p.Id = Guid.NewGuid();
            t.Accounts.AddObject(p);
            t.SaveChanges();
        }
        public void Update(Players p)
        {
            tarotContainer t = new tarotContainer();
            Players oldP = t.Accounts.OfType<Players>().Single(a => a.Id == p.Id);
            if (oldP.ScreenName != p.ScreenName)
            {
                t.Accounts.OfType<Players>().Single(a => a.Id == p.Id).ScreenName = p.ScreenName;
            } if (oldP.Username != p.Username)
            {
                t.Accounts.OfType<Players>().Single(a => a.Id == p.Id).Username = p.Username;
            } if (oldP.Pwd != p.Pwd)
            {
                t.Accounts.OfType<Players>().Single(a => a.Id == p.Id).Pwd = p.Pwd;
            }
            t.SaveChanges();
        }

        public static Players Get(string login)
        {
            tarotContainer t = new tarotContainer();
            return t.Accounts.OfType<Players>().Single(p => p.Username == login);
        }
        public static Players Get(Guid id)
        {

            tarotContainer t = new tarotContainer();
            return t.Accounts.OfType<Players>().SingleOrDefault(p => p.Id == id);
            
        }

        public static List<Players> GetAll()
        {
            tarotContainer t = new tarotContainer();
            return t.Accounts.OfType<Players>().ToList();
        } 

        public static void UpdateLoginDate(Guid id)
        {
            tarotContainer t=new tarotContainer();
            t.Accounts.OfType<Players>().Single(p => p.Id == id).LastLogin = DateTime.Now;
            t.SaveChanges();
        }

        public static void LogOff(Guid id)
        {
            tarotContainer t = new tarotContainer();
            t.Accounts.OfType<Players>().Single(p => p.Id == id).LastLogin = new DateTime();
            t.SaveChanges();
        }
    }
}
