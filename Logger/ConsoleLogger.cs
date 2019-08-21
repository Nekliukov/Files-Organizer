using System;
using System.Collections.Generic;
using System.Text;

namespace FilesDistributor.Logger
{
    class ConsoleLogger : ILogger
    {
        public delegate void LoggingActionHandler();

        public event LoggingActionHandler OnMessageLogged;

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            OnMessageLogged();
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            OnMessageLogged();
        }

        public void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            OnMessageLogged();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            OnMessageLogged();
        }
    }
}
