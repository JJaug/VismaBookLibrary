using System;
using System.Collections.Generic;
using System.Linq;
using VismaBookLibrary.ConsoleApp.Interfaces;
using VismaBookLibrary.ConsoleApp.Models;

namespace VismaBookLibrary.ConsoleApp.Services
{
    public class BookService : IBookService
    {
        private readonly IFileService _fileService;

        public BookService(IFileService fileService)
        {
            _fileService = fileService;
        }
        public List<Book> GetBooks()
        {
            return _fileService.GetBooks();
        }
        public bool AddBook(Book book)
        {
            var bookList = GetBooks();
            bookList.Add(book);
            return _fileService.ExportBooks(bookList);

        }
        public bool BorrowBook(string bookName, string borrowedBy, DateTime expextedReturnDate)
        {
            var bookList = GetBooks();
            var bookToBorrow = bookList.FirstOrDefault(b => b.Name == bookName);
            if (bookToBorrow != null && !bookToBorrow.Taken)
            {
                bookToBorrow.Taken = true;
                bookToBorrow.TakenBy = borrowedBy;
                bookToBorrow.expextedReturnDate = expextedReturnDate;
                bookToBorrow.borrowDate = DateTime.Today;

                return _fileService.ExportBooks(bookList);
            }
            return false;
        }
        public bool ReturnBook(string bookName)
        {

            var bookList = GetBooks();
            var bookToReturn = bookList.FirstOrDefault(b => b.Name == bookName);
            if (bookToReturn != null && bookToReturn.Taken)
            {
                bookToReturn.Taken = false;
                bookToReturn.TakenBy = null;
                bookToReturn.expextedReturnDate = null;
                bookToReturn.borrowDate = null;

                return _fileService.ExportBooks(bookList);
            }
            return false;

        }
        public List<Book> GetBooksByOrder(string orderBy)
        {
            orderBy = orderBy.ToLower();
            var bookList = GetBooks();
            switch (orderBy)
            {
                case "name":
                    List<Book> orderByName = bookList.OrderBy(b => b.Name).ToList();
                    return orderByName;
                case "author":
                    List<Book> orderByAuthor = bookList.OrderBy(b => b.Author).ToList();
                    return orderByAuthor;
                case "category":
                    List<Book> orderByCategory = bookList.OrderBy(b => b.Category).ToList();
                    return orderByCategory;
                case "language":
                    List<Book> orderByLanguage = bookList.OrderBy(b => b.Language).ToList();
                    return orderByLanguage;
                case "isbn":
                    List<Book> orderByISBN = bookList.OrderBy(b => b.Isbn).ToList();
                    return orderByISBN;
                case "taken":
                    List<Book> orderByTaken = bookList.OrderBy(b => b.Taken).ToList();
                    return orderByTaken;
                case "available":
                    List<Book> orderByAvailable = bookList.OrderByDescending(b => b.Taken).ToList();
                    return orderByAvailable;
                default: return null;
            }
        }
        public bool RemoveBook(string Isbn)
        {
            var bookList = GetBooks();
            var bookToRemove = bookList.FirstOrDefault(b => b.Isbn == Isbn);
            if (bookToRemove == null)
            {
                return false;
            }
            else if (bookToRemove.Isbn == Isbn && bookToRemove != null)
            {
                bookList.Remove(bookToRemove);
                return _fileService.ExportBooks(bookList);
            }
            return false;

        }
        public List<Book> GetBooksForUser(string userName)
        {
            return GetBooks().Where(b => b.TakenBy == userName).ToList();
        }
        public Book GetBook(string bookName)
        {
            var bookList = GetBooks();
            return bookList.FirstOrDefault(b => b.Name == bookName);
        }
    }


}
