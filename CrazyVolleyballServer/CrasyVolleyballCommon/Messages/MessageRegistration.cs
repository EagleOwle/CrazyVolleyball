using GalaxyCoreCommon;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballCommon.Messages
{
    /// <summary>
    /// Структура пакета регистрации
    /// </summary>
    [ProtoContract]
    public class MessageRegistration : BaseMessage
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [ProtoMember(1)]
        public string login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [ProtoMember(2)]
        public string password { get; set; }
    }
}
