using GalaxyCoreServer;

namespace CrasyVolleyballServer.Instances
{
    class EmptyInstance : Instance
    {
        public override void Close()
        {
            //throw new System.NotImplementedException();
        }

        public override void IncomingClient(Client clientConnection)
        {
            //throw new System.NotImplementedException();
        }

        public override void OutcomingClient(Client client)
        {

        }

        public override void InMessage(byte code, byte[] data, Client clientConnection)
        {
            if (owner == clientConnection.id) ChangeOwner();
        }


        public override void Start()
        {
            Log.Info("ExampleEmptyInstance", "instance id:" + id);
            SetFrameRate(30);
        }

        public override void Update()
        {
        }
    }
}
