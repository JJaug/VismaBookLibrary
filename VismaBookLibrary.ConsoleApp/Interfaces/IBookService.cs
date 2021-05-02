using System;
using System.Collections.Generic;
using VismaBookLibrary.ConsoleApp.Models;

namespace VismaBookLibrary.ConsoleApp.Interfaces
{
    public interface IBookService
    {
        bool AddBook(Book book);
        bool BorrowBook(string bookName, string borrowedBy, DateTime expextedReturnDate);
        List<Book> GetBooks();
        List<Book> GetBooksByOrder(string orderBy);
        bool RemoveBook(string Isbn);
        bool ReturnBook(string bookName);
        List<Book> GetBooksForUser(string userName);
        Book GetBook(string bookName);
    }
}
