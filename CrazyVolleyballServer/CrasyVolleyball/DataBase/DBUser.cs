using System;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballServer.DataBase
{
    public class DBUser
    {
        public DBUser(int id, string login, string password)
        {
            this.id = id;
            this.login = login;
            this.password = password;
        }

        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
