using System.Linq;

namespace database
{
    public partial class Versions
    {
        public static Versions Get()
        {
            tarotContainer tc = new tarotContainer();
            Versions v = tc.Versions.FirstOrDefault();
            if (v == null)
            {
                v = new Versions
                        {
                            Client = "0.0",
                            Db = "0.3.2",
                            Server = "0.1"
                        };
                tc.Versions.AddObject(v);
                tc.SaveChanges();
            }
            return v;
        }
    }
}
