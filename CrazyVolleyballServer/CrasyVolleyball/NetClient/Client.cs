using GalaxyCoreServer;

namespace CrasyVolleyballServer.NetClient
{
    public class BaseClient : Client
    {
        public BaseClient(int dbID, string name)
        {
            this.dbID = dbID;
            this.name = name;
        }

        public int dbID;
    }
}
