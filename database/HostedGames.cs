using System;
using System.Collections.Generic;
using System.Linq;

namespace database
{
    public partial class HostedGames
    {
        static public void NewGame(HostedGames game)
        {
            tarotContainer t = new tarotContainer();
            t.HostedGames.AddObject(game);
            t.SaveChanges();
        }

        public static bool Start(Players hoster)
        {
            try
            {
                tarotContainer t = new tarotContainer();
                t.HostedGames.Single(h => h.Hoster.Id == hoster.Id).IsLaunched = true;
                t.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }/*
        public static bool Start(Anon player)
        {
            tarotContainer t=new tarotContainer();
            t.HostedGames.Single(h=>h.Players.Any(p=>p.Id==player))
        }*/

        public static bool GetIsLaunched(Players player)
        {
            tarotContainer t = new tarotContainer();
            return t.HostedGames.Single(h => h.Players.Any(p => p.Id == player.Id)).IsLaunched;
        }
        public static bool GetIsLaunched(Guid playerId)
        {
            tarotContainer t = new tarotContainer();
            HostedGames hh = t.HostedGames.SingleOrDefault(h => h.Players.Any(p => p.Id == playerId));
            if (hh != null)
            {
                return hh.IsLaunched;
            }
            return false;
        }

        public static HostedGames Get(Players hoster)
        {
            tarotContainer t = new tarotContainer();
            return t.HostedGames.Single(h => h.Hoster.Id == hoster.Id);
        }

        /// <summary>
        /// Permet de récupérer un jeux grâce à un id d'un participant.
        /// /!\ renvoie null si le jeux n'est pas actif!
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static HostedGames Get(Guid playerId)
        {
            tarotContainer t = new tarotContainer();
            return t.HostedGames.SingleOrDefault(h => h.IsLaunched && h.Players.Any(p => p.Id == playerId));
        }

        /// <summary>
        /// est censé mettre à jours les informations au fur et a mesure de l'avancée du tour.
        /// a vérifier.
        /// </summary>
        /// <param name="hg"></param>
        public static void Update(HostedGames hg)
        {
            tarotContainer t = new tarotContainer();
            foreach (PlayersData pla in t.HostedGames.Single(g => g.Id == hg.Id).PlayersData)
            {
                if (t.HostedGames.Single(h => h.Id == hg.Id).PlayersData.Any(p => p == pla)) continue;
                t.HostedGames.Single(h => h.Id == hg.Id).PlayersData.Single(p => p.Id == pla.Id).Cards = pla.Cards;
                t.HostedGames.Single(h => h.Id == hg.Id).PlayersData.Single(p => p.Id == pla.Id).IsPlayersTurn = pla.IsPlayersTurn;
            }

            if (t.HostedGames.Single(h => h.Id == hg.Id).Players.Count != hg.Players.Count)
            {
                foreach (Accounts accounts in hg.Players)
                {
                    if(t.HostedGames.Single(h => h.Id == hg.Id).Players.Any(p=>p.Id==accounts.Id))continue;
                    t.HostedGames.Single(h => h.Id == hg.Id).Players.Add(accounts);
                }
            }
            t.SaveChanges();
        }

        public static List<HostedGames> GetAll()
        {
            return new tarotContainer().HostedGames.Where(h => !h.IsLaunched && h.Players.Count <= h.PlayerMax).ToList();
        }

        public static HostedGames Get(int hostedGameid)
        {
            return new tarotContainer().HostedGames.SingleOrDefault(h => h.Id == hostedGameid);
        }
    }
}
