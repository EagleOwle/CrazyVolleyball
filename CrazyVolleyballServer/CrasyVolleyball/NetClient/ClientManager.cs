using System;
using System.Collections.Generic;
using GalaxyCoreServer;
using System.Collections.Concurrent;
using GalaxyCoreServer.Api;

namespace CrasyVolleyballServer.NetClient
{
    public class ClientManager
    {
        public delegate void ClientChange(Client client);
        public static event ClientChange eventClientAdd;
        public static event ClientChange eventClientRemove;

         /// Список клиентов по ид
        /// </summary>
        ConcurrentDictionary<int, BaseClient> clientsID = new ConcurrentDictionary<int, BaseClient>();

        public ClientManager()
        {
            GalaxyEvents.OnGalaxyDisconnect += OnGalaxyDisconnect; // Подписываемся на событие дисконекта клиента
        }

        public Client GetClientByID(int dbID)
        {
            foreach (var item in clientsID)
            {
                if(item.Value.dbID == dbID)
                {
                    return item.Value;
                }
            }

            return null;
        }

        private void OnGalaxyDisconnect(Client client)
        {
            RemoveClient(client);
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(BaseClient client)
        {
            Log.Info("Server", "Добавлен новый клиент ид: " + client.id);

            clientsID.TryAdd(client.id, client);

            eventClientAdd?.Invoke(client);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="connection"></param>
        public void RemoveClient(Client client)
        {
            BaseClient tmp = client as BaseClient;
            clientsID.TryRemove(client.id, out tmp);

            eventClientRemove?.Invoke(client);

            Log.Info("Server", "Клиент решил нас покинуть: " + client.id);
        }

    }
}
