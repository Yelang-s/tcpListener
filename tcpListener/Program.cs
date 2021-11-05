using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tcpListener
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpServer = new TcpListener(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));
            tcpServer.Start();

            //tcpServer.AcceptSocket();
            // 等待客户端接入
            TcpClient client = tcpServer.AcceptTcpClient();
            Console.WriteLine($"{client.Client.RemoteEndPoint}接入");
            NetworkStream stream = client.GetStream();
            while (true)
            {
                byte[] readDatas = new byte[1024];
                int len = stream.Read(readDatas, 0, readDatas.Length);
                string data = Encoding.UTF8.GetString(readDatas, 0, len);
                Console.WriteLine($"{client.Client.RemoteEndPoint}-->{data}");
                string data1 = data + "!!!";
                byte[] writeD = Encoding.UTF8.GetBytes(data1);
                stream.Write(writeD, 0, writeD.Length);
            }
        }
    }
}
