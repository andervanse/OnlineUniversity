
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;


namespace OnlineUniversity.Infrastructure
{
    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [{logLevel}] {exception.Message}");
        }

        public void LogCritical(string message, object[] args)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [CRITICAL] {message}", args);
        }

        public void LogDebug(string message, object[] args)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [DEBUG] {message}", args);
        }

        public void LogError(string message, object[] args)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [ERROR] {message}", args);
        }

        public void LogInfo(string message, object[] args)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [INFO] {message}", args);
        }

        public void LogTrace(string message, object[] args)
        {
            Console.WriteLine($" [{DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")}] [TRACE] {message}", args);
        }
    }
}
