using System;
using VismaBookLibrary.ConsoleApp.Interfaces;

namespace VismaBookLibrary.ConsoleApp.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public int Read()
        {
            return Console.Read();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

    }
}
