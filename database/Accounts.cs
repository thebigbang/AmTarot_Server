using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace database
{
    public partial class Accounts
    {
        static public Accounts Get(Guid id)
        {
            return new tarotContainer().Accounts.SingleOrDefault(a => a.Id == id);
        }
    }
}
