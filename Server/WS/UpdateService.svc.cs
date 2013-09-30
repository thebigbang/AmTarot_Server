using database;

namespace Server.WS
{
    /// <summary>
    /// Permet de
    /// *récupérer la version du client.
    /// todo: non peut-être rien de plus ici...
    /// </summary>
    public class UpdateService : IUpdateService
    {
        public string GetVersion()
        {
            return Versions.Get().Client;
        }
    }
}
