using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeworkCSharpApp1
{
    internal class Message
    {
        public string? Text { get; set; }
        public string? NickNameFrom { get; set; }
        public string? NickNameTo { get; set; }
        public DateTime? DateTime { get; set; }

        public string SerializeMessageToJson() => JsonSerializer.Serialize(this);
        public static Message? DeserializeFromJsonToMessage(string message) => JsonSerializer.Deserialize<Message>(message);

        public void PrintMessage()
        {
            Console.WriteLine(this.Text);
        }
        public override string ToString()
        {
            return $"Дата: {this.DateTime}\n" +
                $"Сообщение отправил: {this.NickNameFrom}\n" +
                $"Сообщение получил: {this.NickNameTo}\n " +
                $"Сообщение: {this.Text}\n";
        }
    }
}
