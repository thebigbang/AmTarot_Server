using System;
using database;

namespace Server.WS
{
    /// <summary>
    /// permet de
    /// *connecter un joueur
    ///         avec compte
    ///         sans compte
    /// *vérifier qu'un joueur est présent (boucle permettant de dire quand le joueur était actif pour la derniere fois, est lié au service tournant en fond dans notre serveur permettant lui de déconnecter un joueur inactif)
    /// *créer un compte pour un joueur.
    /// 
    /// todo: possibilité de déconnection propre?
    /// </summary>
    public class LoginService : ILoginService
    {
        public bool LoginWithAccount(string login, string password)
        {
            return Players.Login(login, password);

        }

        public Guid LoginWithoutAccount(string displayName = null)
        {
            return Anon.Login(displayName);
        }
        /// <summary>
        /// Permet de verifier la présence d'un joueur.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAnon"></param>
        public void CheckClientPresence(Guid id, bool isAnon)
        {
            if (isAnon && Anon.Get(id).LastLogin.AddMinutes(7) > DateTime.Now)
            {
                Anon.LogOut(id);
            }
            else if (Players.Get(id).LastLogin.AddMinutes(7) > DateTime.Now)
            {
                Players.LogOff(id);
            }
            else
            {
                if (isAnon)
                {
                    Anon.UpdateLoginDate(id);
                }
                else
                {
                    Players.UpdateLoginDate(id);
                }
            }
        }

        public void CreateAccount(string username, string pwd, string email)
        {
            Players.Create(new Players
                               {
                                   Email = email,
                                   LastLogin = DateTime.Now,
                                   Pwd = pwd,
                                   ScreenName = username,
                                   Username = username
                               });
        }
    }
}
