using GalaxyCoreServer;

namespace CrasyVolleyballServer.Instances
{
    public class GameRoom : Instance
    {
        public override void Close()
        {
            //throw new System.NotImplementedException();
        }

        public override void IncomingClient(Client clientConnection)
        {
            //throw new System.NotImplementedException();
        }

        public override void InMessage(byte code, byte[] data, Client clientConnection)
        {
            //throw new System.NotImplementedException();
        }

        public override void OutcomingClient(Client client)
        {

        }

        public override void Start()
        {
            //throw new System.NotImplementedException();
            Log.Info("GameRoom " + id, "Start");
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }

        public bool IsFull
        {
            get
            {
                if (clients.Count == maxClients)
                {
                    return true;
                }
                else

                {
                    return false;
                }
            }

        }
    }
}
