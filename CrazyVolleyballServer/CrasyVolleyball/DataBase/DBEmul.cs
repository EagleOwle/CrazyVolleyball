using System;
using System.Collections.Generic;
using System.Text;

namespace CrasyVolleyballServer.DataBase
{
    public static class DBEmul
    {
        private static List<DBUser> users = new List<DBUser>
            {
            new DBUser(0, "Tester", "Tester"),
            new DBUser(1, "Tester1", "Tester"),
            new DBUser(2, "Tester2", "Tester")
        };

        public static bool FindUser(string login, string password, out string report)
        {
            report = "Пользователь не найден.";

            foreach (DBUser item in users)
            {
                if (item.login == login)
                {
                    if (item.password == password)
                    {
                        report = "Пользователь найден.";
                        //Console.WriteLine("DBCashe || " + login + ": " + report);
                        return true;
                    }
                    else
                    {
                        report = "Не верный пароль.";
                        //Console.WriteLine("DBCashe || " + login + ": " + report);
                        return false;
                    }
                }
            }

            //Console.WriteLine("Find User || " + login + " : " + report);
            return false;
        }

        public static bool FindUser(string login, out string report)
        {
            report = "Пользователь не найден.";

            foreach (DBUser item in users)
            {
                if (item.login == login)
                {
                    report = "Пользователь найден.";
                    //Console.WriteLine("DBCashe || " + login + ": " + report);
                    return true;
                }
            }

            //Console.WriteLine("Find User || " + login + " : " + report);
            return false;
        }

        public static void AddNewUser(string login, string password)
        {
            users.Add(new DBUser(users.Count + 1, login, password));
        }

        public static int GetId(string login)
        {
            foreach (DBUser item in users)
            {
                if (item.login == login)
                {
                    return item.id;

                }
            }

            return -1;
        }
    }
}

