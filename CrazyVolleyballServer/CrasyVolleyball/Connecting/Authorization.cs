using CrasyVolleyballCommon.Messages;
using CrasyVolleyballServer.NetClient;
using GalaxyCoreCommon;
using GalaxyCoreServer;
using GalaxyCoreServer.Api;

namespace CrasyVolleyballServer.Connecting
{
    /// <summary>
    /// An example of authorization implementation
    /// </summary>
    public class Authorization
    {
        public Authorization()
        {
            GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect; // подписываемся на событие
        }

        /// <summary>
        /// Сюда нам придет запрос на авторизацию
        /// </summary>
        /// <param name="approvalConnection">Временное не авторизированное соеденение</param>
        /// <param name="data">Массив байт, данные присланные вместе с запросом авторизации</param>
        private void OnGalaxyConnect(ApprovalConnection approvalConnection, byte[] data)
        {
            MessageAuth message = MessageAuth.Deserialize<MessageAuth>(data); //преобразовывем массив байт в читабельное сообщение  

            #region Проверка соединения с сервером
            if (message.login == "Test Connection")
            {
                Log.Info(message.login, ": Проверка подключения");
                approvalConnection.Deny((byte)ServerApproval.accept, "Проверка подключения");
                return;
            }
            #endregion

            //Создадим пакет который мы отправим клиенту вместе с разрешением коннекта
            var response = new MessageApproval
            {
                Name = message.login
            };
            // Получаем ид, сюда следует подставить реальный ид клиента по базе
            int clientID = Tools.GetNewID();

            // ClientConnection connection;  
            // Create your own client implementation
            var client = new BaseClient(clientID, message.login);
            // возвращяем данные вместе с разрешением, так же мы получим уже рабочий экземпляр авторизированного соеденения 
            // так же приклепляем собственную реализацию клиента, для того что бы в бущем, можно было её оперативно получить из коннекшена
            approvalConnection.Approve(response, clientID, client);
            Log.Debug("OnGalaxyConnect", "Client " + clientID + " connected");
        }

        #region Original
        ///// <summary>
        ///// Сюда нам придет запрос на авторизацию
        ///// </summary>
        ///// <param name="approvalConnection">Временное не авторизированное соеденение</param>
        ///// <param name="data">Массив байт, данные присланные вместе с запросом авторизации</param>
        //private void OnGalaxyConnect(ApprovalConnection approvalConnection, byte[] data)
        //{
        //    MessageAuth message = MessageAuth.Deserialize<MessageAuth>(data); //преобразовывем массив байт в читабельное сообщение  

        //    #region Проверка соединения с сервером
        //    if (message.login == "Test Connection")
        //    {
        //        Log.Info(message.login, ": Проверка подключения");
        //        approvalConnection.Deny((byte)ServerApproval.accept, "Проверка подключения");
        //        return;
        //    }
        //    #endregion

        //    if (message.login.Contains("test"))
        //    {
        //        //Создадим пакет который мы отправим клиенту вместе с разрешением коннекта
        //        var response = new MessageApproval
        //        {
        //            Name = message.login
        //        };
        //        // Получаем ид, сюда следует подставить реальный ид клиента по базе
        //        int clientID = Tools.GetNewID();

        //        // ClientConnection connection;  
        //        // Create your own client implementation
        //        var client = new BaseClient(clientID, message.login);
        //        // возвращяем данные вместе с разрешением, так же мы получим уже рабочий экземпляр авторизированного соеденения 
        //        // так же приклепляем собственную реализацию клиента, для того что бы в бущем, можно было её оперативно получить из коннекшена
        //        approvalConnection.Approve(response, clientID, client);
        //        Log.Debug("OnGalaxyConnect", "Client " + clientID + " connected");
        //    }
        //    else
        //    {
        //        approvalConnection.Deny(1, "Deny");
        //        Log.Debug("OnGalaxyConnect", "Deny");
        //    }

        //}
        #endregion

    }
}
