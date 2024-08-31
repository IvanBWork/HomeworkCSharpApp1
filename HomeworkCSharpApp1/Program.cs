using System.Net.Sockets;
using System.Net;
using System.Text;

namespace HomeworkCSharpApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ChatProgram.Server();
            }
            else
            {
                ChatProgram.Client(args);
            }
        }
    }
}
