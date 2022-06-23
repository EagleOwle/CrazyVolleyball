using System;
using GalaxyCoreServer;

namespace CrasyVolleyballServer
{
    class Program
    {
        static Server server;
        static void Main(string[] args)
        {
            server = new Server();
            while (true)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "exit":
                        GalaxyCore.Stop();
                        return;

                }

            }

        }
    }
}
