using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace database
{
   public partial class PlayersData
    {
       public static void Create(PlayersData data)
       {
           System.Diagnostics.Debug.Print("ICIIIIIIIIII " + data);
           tarotContainer t=new tarotContainer();
           t.PlayersData.AddObject(data);
           t.SaveChanges();
           t.Dispose();
       }
       public static void Update(PlayersData data)
       {
           tarotContainer t=new tarotContainer();
           PlayersData oldData = t.PlayersData.Single(p => p.Id == data.Id);
           if (oldData.Cards != data.Cards)
           {
               t.PlayersData.Single(p => p.Id == data.Id).Cards = data.Cards;
           } if (oldData.Points != data.Points)
           {
               t.PlayersData.Single(p => p.Id == data.Id).Points= data.Points;
           }
           t.SaveChanges();
       }
       public static void Remove(PlayersData data)
       {
           tarotContainer t=new tarotContainer();
           t.PlayersData.DeleteObject(data);
           t.SaveChanges();
       }

       public static PlayersData Get(Guid playerId)
       {
           return new tarotContainer().PlayersData.Single(p => p.Accounts.Id == playerId);
       }
    }
}
