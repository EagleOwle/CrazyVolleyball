using GalaxyCoreServer;
using GalaxyCoreServer.Api;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballServer.Instances
{
    public class InstanceManager
    {
        public delegate void InstanceChange(Instance instance);
        public static event InstanceChange eventInstanceAdd;
        public static event InstanceChange eventInstanceRemove;

        /// Список клиентов по ид
        /// </summary>
        ConcurrentDictionary<int, Instance> instanceID = new ConcurrentDictionary<int, Instance>();
        public InstanceManager()
        {
           // GalaxyEvents.OnGalaxyInstanceCreate += OnGalaxyInstanceCreate;
        }

        /// <summary>
        /// Это пример переопределения стандартной реализации инстанса по пользовательскому коду типа
        /// </summary>
        /// <param name="type">пользовательский код типа инстанса</param>
        /// <param name="data">массив байт дополнительной информации приложеной к запросу создания</param>
        /// <param name="client">клиент отправивший запрос</param>
        /// <returns>Вернуть любого наследника класса Inctance</returns>
        private Instance OnGalaxyInstanceCreate(byte type, byte[] data, Client client)
        {
            GalaxyCore.instances.Create("GameRoom", 2, 0, null, (Client)client);
            AddInstance(client.instanse);
            return client.instanse;
        }

        public Instance GetInstanceByID(int id)
        {
            foreach (var item in instanceID)
            {
                if (item.Value.id == id)
                {
                    return item.Value;
                }
            }

            return null;
        }

        public bool GetNotFilledInstance(out Instance instance)
        {
            foreach (var item in instanceID)
            {
                Log.Info("Instance Manager - ", "" + item.Value.id);
            }
            
            instance = null;
            foreach (var item in instanceID)
            {
                GameRoom tmp = item.Value as GameRoom;
                if (tmp.IsFull)
                {
                    instance = item.Value;
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Добавить комнату
        /// </summary>
        /// <param name="instance"></param>
        public void AddInstance(Instance instance)
        {
            instanceID.TryAdd(instance.id, instance);
            Log.Info("Server", "Добавлена новая комната ид: " + instance.id);
            eventInstanceAdd?.Invoke(instance);
            foreach (var item in instanceID)
            {
                Log.Info("Instance Manager - ", " GameRoom -> " + item.Value.id);
            }
        }

        /// <summary>
        /// Удалить комнату
        /// </summary>
        /// <param name="instance"></param>
        public void RemoveInstance(Instance instance)
        {
            instanceID.TryRemove(instance.id, out instance);
            eventInstanceRemove?.Invoke(instance);
            Log.Info("Server", "Удалена комната ид: " + instance.id);
        }

    }
}
