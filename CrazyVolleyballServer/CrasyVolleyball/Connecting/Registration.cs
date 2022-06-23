using CrasyVolleyballCommon.Messages;
using CrasyVolleyballServer.DataBase;
using GalaxyCoreServer;
using GalaxyCoreServer.Api;

namespace CrasyVolleyballServer.Connecting
{
    class Registration
    {
        public Registration()
        {
            GalaxyEvents.OnGalaxyRegistration += OnGalaxyRegistration; // подписываемся на событие регистрацию
        }

        /// <summary>
        /// Запрос регистрации
        /// </summary>
        /// <param name="approvalConnection">Неавторизированное подключение запрашивающее регистрацию</param>
        /// <param name="data">массив байт, данные приложеные к запросу</param>
        private void OnGalaxyRegistration(ApprovalConnection approvalConnection, byte[] data)
        {
            Log.Info("Попытка регистрации", "Попытка регистрации");
            MessageRegistration message = MessageRegistration.Deserialize<MessageRegistration>(data); //преобразовывем массив байт в читабельное сообщение 

            Log.Info("Registration", "Попытка регистрации");

            string report;
            if (DBEmul.FindUser(message.login, out report))
            {
                approvalConnection.Deny((byte)ServerApproval.accept, "Такой логин уже зарегистрирован");
                Log.Info("Registration", message.login.ToString() + " Ошибка регистрации. Логин занят");
            }
            else
            {
                DBEmul.AddNewUser(message.login, message.password);
                approvalConnection.Deny((byte)ServerApproval.accept, "Тестовая регистрация прошла успешно");
                Log.Info("Registration", message.login + " Регистрация нового пользователя");
            }
        }
    }
}

