using Microsoft.Extensions.DependencyInjection;
using System;
using VismaBookLibrary.ConsoleApp.Interfaces;
using VismaBookLibrary.ConsoleApp.Loggers;
using VismaBookLibrary.ConsoleApp.Services;

namespace VismaBookLibrary.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var cli = serviceProvider.GetService<LibraryCli>();

            cli.PrintCommands();
            while (true)
            {
                Console.WriteLine("Enter command:");
                var command = Console.ReadLine().ToLower();
                if (command == "exit")
                {
                    break;
                }
                cli.ExecuteCommand(command);
            }
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<BookService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<LibraryCli>();

        }
    }
}
