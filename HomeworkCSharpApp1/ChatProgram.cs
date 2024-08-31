using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkCSharpApp1
{
    internal class ChatProgram
    {
        public static void Client(string[] args)
        {
            string myNickName = args[0];
            string ip = args[1];    

            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                string messageTextOut;
                do
                {
                    Console.WriteLine("Введите сообщение: ");
                    messageTextOut = Console.ReadLine()!;
                }
                while (string.IsNullOrEmpty(messageTextOut));

                Message message = new Message()
                {
                    DateTime = DateTime.Now,
                    NickNameFrom = myNickName,
                    NickNameTo = "Server",
                    Text = messageTextOut
                };
                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                udpClient.Send(data, data.Length, iPEndPoint);

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                if (buffer != null)
                {
                    var messageTextIn = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(messageTextIn);
                    Console.WriteLine();
                }
                else Console.WriteLine("Сообщение не дошло");
            }
        }

        public static void Server()
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Сервер ожидает сообщение от клиента... ");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                if (buffer == null) break;

                var messageText = Encoding.UTF8.GetString(buffer);
                Message? messageServer = Message.DeserializeFromJsonToMessage(messageText);

                Console.WriteLine(messageServer);

                if (!string.IsNullOrEmpty(messageText))
                {
                    Thread.Sleep(1000);
                    byte[] data = Encoding.UTF8.GetBytes("Сообщение доставлено.");
                    udpClient.Send(data, data.Length, iPEndPoint);
                }
            }
        }
    }
}
