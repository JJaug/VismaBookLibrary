using AutoFixture;
using FluentAssertions;
using System;
using System.Linq;
using VismaBookLibrary.ConsoleApp.Models;
using VismaBookLibrary.ConsoleApp.Services;
using Xunit;

namespace VismaBookLibrary.IntegrationTests
{
    public class BookServiceIntegrationTests
    {
        [Fact]
        public void AddBook_GivenNewBook_GetAllReturnsIt()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            var fileService = new FileService("/Data/TestData.json");

            var bookService = new BookService(fileService);

            bookService.AddBook(book);

            var books = bookService.GetBooks();

            books.Select(b => b.Name).ToList().Should().Contain(book.Name);

            bookService.RemoveBook(book.Isbn);

        }
        [Fact]
        public void BorrowedBook_GivenBookToBorrow_GetAllReturnsIt()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            var fileService = new FileService("/Data/TestData.json");

            var bookService = new BookService(fileService);

            bookService.AddBook(book);

            var books = bookService.GetBooks();


            bookService.BorrowBook(book.Name, "MrTester", DateTime.Now);

            var takenBook = books.FirstOrDefault(b => b.Name == book.Name);

            takenBook.Taken.Should().BeTrue();

            bookService.RemoveBook(book.Isbn);


        }
        [Fact]
        public void RerrnedBook_GivenBookToReturn_GetAllReturnsIt()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            var fileService = new FileService("/Data/TestData.json");

            var bookService = new BookService(fileService);

            bookService.AddBook(book);

            var books = bookService.GetBooks();

            bookService.BorrowBook(book.Name, "MrTester", DateTime.Now);
            bookService.ReturnBook(book.Name);

            var updatedBooks = bookService.GetBooks();

            var returnedBook = updatedBooks.FirstOrDefault(b => b.Name == book.Name);

            returnedBook.Taken.Should().BeFalse();

            bookService.RemoveBook(book.Isbn);

        }
        [Fact]
        public void RemoveBook_GivenBookToRemove_GetAllReturnsIt()
        {
            var fixture = new Fixture();
            var book = fixture.Create<Book>();
            var fileService = new FileService("/Data/TestData.json");

            var bookService = new BookService(fileService);

            bookService.AddBook(book);

            var books = bookService.GetBooks();

            books.Select(b => b.Isbn).ToList().Should().Contain(book.Isbn);

            bookService.RemoveBook(book.Isbn);

            var updatedBooks = bookService.GetBooks();

            updatedBooks.Should().BeEmpty();



        }

    }
}

