using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace database
{
   public partial class Anon
    {
       public static Guid Login(string displayName=null)
       {
           Anon a=new Anon();
           if(displayName!=null)
           {
               a.ScreenName = displayName;
           }
           a.Id = Guid.NewGuid();
           tarotContainer t=new tarotContainer();
           t.Accounts.AddObject(a);
           t.SaveChanges();
           return a.Id;
       }
       public static void LogOut(Guid id)
       {
           tarotContainer t=new tarotContainer();
           t.Accounts.DeleteObject(
               t.Accounts.OfType<Anon>().Single(a => a.Id == id
               ));
           t.SaveChanges();
       }

       public static void UpdateLoginDate(Guid id)
       {

           tarotContainer t = new tarotContainer();
           t.Accounts.OfType<Anon>().Single(p => p.Id == id).LastLogin = DateTime.Now;
           t.SaveChanges();
       }

       public static Anon Get(Guid id)
       {

           tarotContainer t = new tarotContainer();
           return t.Accounts.OfType<Anon>().Single(p => p.Id == id);
       }

       public static List<Anon> GetAll()
       {
           tarotContainer t = new tarotContainer();
           return t.Accounts.OfType<Anon>().ToList();
       }
    }
}
