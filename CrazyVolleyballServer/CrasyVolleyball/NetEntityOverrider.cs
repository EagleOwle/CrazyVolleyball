using CrasyVolleyballServer.NetEntitys;
using GalaxyCoreServer;
using GalaxyCoreServer.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballServer
{
    /// <summary>
    /// Переопределение сетевых сущностей созданных с стороны клиента
    /// </summary>
    class NetEntityOverrider
    {
        public NetEntityOverrider()
        {
            GalaxyEvents.OnNetEntityInstantiate += OnNetEntityInstantiate;
        }


        private NetEntity OnNetEntityInstantiate(string name, byte[] data, Client client)
        {
            switch (name)
            {
                case "Player":
                    Player player = new Player(client.instanse);
                    return player;
                default:
                    return null;
            }

        }

    }
}
