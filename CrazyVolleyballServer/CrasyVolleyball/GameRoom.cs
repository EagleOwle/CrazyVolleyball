using GalaxyCoreServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballServer
{
    class GameRoom : Instance
    {
        public override void Close()
        {
        }

        public override void IncomingClient(Client clientConnection)
        {
        }

        public override void InMessage(byte code, byte[] data, Client client)
        {
            Log.Debug("Instance", "InMessage code:" + code);
        }

        public override void OutcomingClient(Client clientConnection)
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
