using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Server.WS.Item;
using database;

namespace Server.WS
{
    /// <summary>
    /// permet de
    /// *todo vérifier si on es en ligne? à finir
    /// *Hoster une partie sur le serveur (identifié avec un joueur qui est le maitre de la partie, son créateur)
    ///     todo: finir la logique permettant de stocker et effectivement hoster le jeux sur le serveur.
    /// *lancer une partie en cours d'hosting.
    ///     jette une exception en cas de partie non lancée, joueur n'étant pas le hosteur...
    ///     devrait être OK
    /// *todo lancer une partie en joueur anonyme utile/futile?
    /// *savoir si une partie est lancée ou non (utilise le GUID du joueur pour lui répondre)
    /// *récupère un joueur selon son nom de login
    /// *récupère un joueur anonume selon son guid.
    /// *récupère la liste des cartes d'un joueur grâce à son guid.
    /// *joue la carte demandée par un joueur grâce à son guid et une classe carte représentant la carte à jouer.
    ///     todo: voir si on prends en compte la force de la carte maintenant ou en fin de tour.
    /// *récupère la liste des parties disponibles pour jouer.
    /// *rejoindre une partie
    /// *savoir si la partie que l'on souhaite est pleine ou non.
    /// *distribue les cartes entre les joueurs  (4 pour le moment apparement)
    ///     todo: voir si cette méthode est terminée.
    /// 
    /// todo: déterminer quand et comment un tour est finie, finir le tour selon:
    ///         logique: si sur playcard c'est notre dernier joueur du tour alors le tour est logiquemement terminé et il ne reste plus qu'à finir le tour
    ///                 force des cartes et savoir quiqui remporte le tour actuel.
    /// 
    /// </summary>
    public class GameService : IGameService
    {
        public bool IsOnline()
        {
            //voir si bool de debug dans la db ou pas...
            return true;
        }

        /// <summary>
        /// Permet de hoster un jeux.
        /// Note: ne permet que le tarot pour le moment
        /// </summary>
        /// <param name="hoster"></param>
        public void HostGame(Players hoster)
        {
            //todo: logik for hosting a game
            HostedGames hg = new HostedGames
                                 {
                                     Hoster = hoster,
                                     GameType = CardsType.Tarot.ToString(),
                                     PlayerMax = 5
                                 };

            List<Card> cards = new ConstantsService().GetCards(CardsType.Tarot, true);
            foreach (Card card in cards)
            {
                hg.CardsOrderList += "#" + card.Name;
            }
            HostedGames.NewGame(hg);
        }
        public void StartGame(Players hoster)
        {
            if(!HostedGames.Start(hoster)){throw new Exception("Impossible de lancer la partie");}
            Thread t = new Thread(() => DealCards(hoster));
            t.Start();
        }
        public void StartGameAnon(Guid h)
        {
          /*  Anon p = GetPlayer(h);
            if (!HostedGames.Start(p)) { throw new Exception("Impossible de lancer la partie"); }
            Thread t = new Thread(() => DealCards(p));
            t.Start();*/
        }
/*
        public bool OldIsGameStarted(Players player)
        {
            return HostedGames.GetIsLaunched(player);
        }*/
        public bool IsGameStarted(Guid playerId)
        {
            return HostedGames.GetIsLaunched(playerId);
        }

        public Players GetPlayer(string username)
        {
            return Players.Get(username);
        }

        public Anon GetPlayer(Guid g)
        {
            return Anon.Get(g);
        }

        public List<Card> GetCards(Guid playerId)
        {
            PlayersData pd= PlayersData.Get(playerId);
            string[] s = pd.Cards.Split('#');
            return s.Select(Card.Get).ToList();
        }

        public void PlayCard(Guid playerId, Card card)
        {
            //supprime la carte du paquet du joueur.
            PlayersData pd = PlayersData.Get(playerId);
            // ce n'est pas au tour de ce joueur houuu le vilain.
            if(!pd.IsPlayersTurn)return;
            HostedGames hg = HostedGames.Get(playerId);
            //le joueur a deja joué houuu le gros vilain à falsifier 2 fois les informations...
            if(hg.CardsInTurn!=null && hg.CardsInTurn.Contains(playerId.ToString()))return;
            int posi = hg.PlayersData.ToList().IndexOf(pd) + 1;
            hg.PlayersData.ToList()[posi].IsPlayersTurn = true;
            string[] s = pd.Cards.Split('#');
            List<Card> cards= s.Select(Card.Get).ToList();
            string build= cards.Aggregate("", (current, c) => current + (c.Name + "#"));
            pd.Cards = build;
            pd.IsPlayersTurn = false;
            PlayersData.Update(pd);
            HostedGames.Update(hg);
            //note: peut-être ca serait intelligent d'avoir une liste des cartes jouées dans le tour en cours...? :p
            if (hg.CardsInTurn == null) hg.CardsInTurn = "";
            hg.CardsInTurn += "[" + playerId + "]" + card.Name + "#";
            //t..odo: ajouter la carte aux cartes jouées du tour en cours
            //todo: prendre en compte le niveau de la carte. ou voir pour le faire en fin de tour?
            //t..odo: tour effectué par un joueur.
        }

        public List<HostedGames> GetAvaiableGames()
        {
            return HostedGames.GetAll();
        }

        public bool JoinGame(int hostedGameid, Guid anonOrPlayerId)
        {
            HostedGames hg = HostedGames.Get(hostedGameid);
            if (hg == null) return false;
            Accounts a = Accounts.Get(anonOrPlayerId);
            if (a == null) return false;
            hg.Players.Add(a);
            HostedGames.Update(hg);
            return true;
        }

        public bool isHostedGameFull(Guid id)
        {
            HostedGames hg = HostedGames.Get(id);
            if (hg.Players.Count <= hg.PlayerMax - 1) return true;
            return false;
        }

        private void DealCards(Players hoster)
        {
            //18 cartes pour 4

            HostedGames game = HostedGames.Get(hoster);
            foreach(Players p in game.Players)
            {
                PlayersData data=new PlayersData
                                     {
                                         Accounts = p,HostedGames =game
                                     };
                string[] daCardsStr  = game.CardsOrderList.Split('#').Where(ss=>!string.IsNullOrEmpty(ss)).ToArray();
                for(int i = 0; i<18; i++)
                {
                    data.Cards+="#"+(daCardsStr[i+18*game.Players.ToList().IndexOf(p)]);
                }
                PlayersData.Create(data);
            }
        }
    }
}
