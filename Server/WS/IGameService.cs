using System;
using System.Collections.Generic;
using System.ServiceModel;
using Server.WS.Item;
using database;

namespace Server.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGameService" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        bool IsOnline();
        /// <summary>
        /// Définit que quelqu'un souhaite créer une partie de jeux.
        /// 
        /// </summary>
        /// <param name="hoster">hoster: guid ou username à tester.</param>
        [OperationContract]
        void HostGame(Players hoster);

        /// <summary>
        /// Permet au hoster de lancer le jeux.
        /// </summary>
        [OperationContract]
        void StartGame(Players hoster);
        [OperationContract]
        void StartGameAnon(Guid guid);
        
        /// <summary>
        /// Permet de vérifier si la partie est lancée par le hoster ou non.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool IsGameStarted(Guid playerId);

        [OperationContract]
        Players GetPlayer(string username);

        [OperationContract]
        List<Card> GetCards(Guid playerId);

        [OperationContract]
        void PlayCard(Guid playerId, Card card);

        [OperationContract]
        List<HostedGames> GetAvaiableGames();

        [OperationContract]
        bool JoinGame(int hostedGameid, Guid anonOrPlayerId);
        [OperationContract]
        bool isHostedGameFull(Guid id);
    }
}
