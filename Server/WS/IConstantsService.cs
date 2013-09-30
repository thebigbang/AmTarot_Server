using System.Collections.Generic;
using System.ServiceModel;
using Server.WS.Item;

namespace Server.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IConstantsService" in both code and config file together.
    [ServiceContract]
    public interface IConstantsService
    {
        /// <summary>
        /// Permet de récupérer une liste de cartes.
        /// </summary>
        /// <param name="typeOfCards">voir enum pour le types de cartes à récupérer</param>
        /// <param name="isRandomOrder">ordre classique ou random?</param>
        /// <returns></returns>
        [OperationContract]
        List<Card> GetCards(CardsType typeOfCards,bool isRandomOrder);
    }
}
