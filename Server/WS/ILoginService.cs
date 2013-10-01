using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using database;

namespace Server.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILoginService" in both code and config file together.
    [ServiceContract]
    public interface ILoginService
    {
        /// <summary>
        /// Permet de se connecter en utilisant un compte de la db.
        /// </summary>
        /// <param name="login">le login envoyé en clair</param>
        /// <param name="password">le pwd envoyé chiffré ici</param>
        /// <returns>true: login ok // false: login nok</returns>
        [OperationContract]
        bool LoginWithAccount(string login, string password);

        /// <summary>
        /// Permet de se connecter sans avoir de compte.
        /// </summary>
        /// <returns>un genre de GUID à validité limité, stocké en db lui.</returns>
        [OperationContract]
        Guid LoginWithoutAccount(string displayName = null);

        [OperationContract]
        void CheckClientPresence(Guid id, bool isAnon);

        [OperationContract]
        void CreateAccount(string username, string pwd, string email);
        ///<summary>
        ///Permet de déconnecter un joueur, anonyme ou non.
        ///</summary>
        [OperationContract]
        void LogOff(Guid id);
    }
}
