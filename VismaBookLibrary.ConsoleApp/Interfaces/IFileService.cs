using System.Collections.Generic;
using VismaBookLibrary.ConsoleApp.Models;

namespace VismaBookLibrary.ConsoleApp.Interfaces
{
    public interface IFileService
    {
        List<Book> GetBooks();
        bool ExportBooks(List<Book> bookList);
    }
}
