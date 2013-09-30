using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUpdateService" in both code and config file together.
    [ServiceContract]
    public interface IUpdateService
    {
        /// <summary>
        /// Permet de récupérer la version actuelle du client.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetVersion();
    }
}
