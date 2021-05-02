using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.ConsoleApp.Interfaces;

namespace VismaBookLibrary.ConsoleApp.Services
{
    class LibraryCli : ILibraryCli
    {
        private readonly BookService bookService;
        private readonly ILogger _logger;

        public LibraryCli(BookService bookService, ILogger logger)
        {
            this.bookService = bookService;
            _logger = logger;
        }
        public void PrintCommands()
        {
            _logger.WriteLine("All commands : add, borrow, return, list, remove, exit");
        }
        public void ExecuteCommand(string input)
        {
        }
    }
}
