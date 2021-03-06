using CrasyVolleyballServer.Connecting;
using CrasyVolleyballServer.Instances;
using CrasyVolleyballServer.NetClient;
using GalaxyCoreServer;
using GalaxyCoreServer.Api;

namespace CrasyVolleyballServer
{
    /// <summary>
    /// Основной пользовательский класс сервера
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Пример реализации авторизации
        /// </summary>
        Authorization authorization = new Authorization();
        /// <summary>
        /// Пример реализации регистрации
        /// </summary>
        Registration registration = new Registration();
        /// <summary>
        /// Конфигурация сервера
        /// </summary>
        Config config = new Config();
        /// <summary>
        /// Класс получающий входящие сообщения
        /// </summary>
        InMessages inMessages = new InMessages();
        /// <summary>
        /// Класс логирования
        /// </summary>
        LogVisualizator logs = new LogVisualizator();
        /// <summary>
        /// Переопределение сетевых сущностей
        /// </summary>
        NetEntityOverrider entityOverrider = new NetEntityOverrider();
        /// <summary>
        /// Показывать ли дебаг сообщения
        /// </summary>
        internal static bool debugLog = true;

        internal static ClientManager clientManager = new ClientManager();
        internal static Instances.InstanceManager instanceManager = new Instances.InstanceManager();

        public Server()
        {
            config.incomingMessage = inMessages; // Регистрируем обработчик входящих сообщений
            //GalaxyEvents.OnGalaxyInstanceCreate += OnGalaxyInstanceCreate; //Отлавливаем событие создания нового инстанса

            //Задаем имя сервера
            //Важно что бы имя сервера совпадало с именем указанным в клиенте
            config.SERVER_NAME = "CrasyVolleyballServer";
            config.LISTEN_PORT = 30200; // Указываем рабочий порт
            config.AUTO_FLUSH_SEND = true; // включаем авто управление буфером отправки сообщений
            config.NET_FRAME_RATE = 20;
            GalaxyCore.Start(config); // Запускаем сервер         
        }

        ///// <summary>
        ///// Это пример переопределения стандартной реализации инстанса по пользовательскому коду типа
        ///// </summary>
        ///// <param name="type">пользовательский код типа инстанса</param>
        ///// <param name="data">массив байт дополнительной информации приложеной к запросу создания</param>
        ///// <param name="client">клиент отправивший запрос</param>
        ///// <returns>Вернуть любого наследника класса Inctance</returns>
        //private Instance OnGalaxyInstanceCreate(byte type, byte[] data, Client client)
        //{
        //    return new GameRoom();
        //}
    }
}
