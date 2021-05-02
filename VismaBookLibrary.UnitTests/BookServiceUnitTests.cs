using AutoFixture;
using FluentAssertions;
using Moq;
using System.Linq;
using VismaBookLibrary.ConsoleApp.Interfaces;
using VismaBookLibrary.ConsoleApp.Models;
using VismaBookLibrary.ConsoleApp.Services;
using Xunit;

namespace VismaBookLibrary.UnitTests
{
    public class BookServiceTests
    {
        [Fact]
        public void GetBooksByOrder_GivenName_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var books = fixture.CreateMany<Book>(4).ToList();
            var mockFileService = new Mock<IFileService>();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("name");

            filteredBooks.Should().BeInAscendingOrder(b => b.Name);
        }
        [Fact]
        public void GetBooksByOrder_GivenAuthor_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var books = fixture.CreateMany<Book>(4).ToList();
            var mockFileService = new Mock<IFileService>();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("author");

            filteredBooks.Should().BeInAscendingOrder(b => b.Author);
        }
        [Fact]
        public void GetBooksByOrder_GivenCategory_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var mockFileService = new Mock<IFileService>();
            var books = fixture.CreateMany<Book>(4).ToList();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("category");

            filteredBooks.Should().BeInAscendingOrder(b => b.Category);
        }
        [Fact]
        public void GetBooksByOrder_GivenLanguage_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var mockFileService = new Mock<IFileService>();
            var books = fixture.CreateMany<Book>(4).ToList();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("language");

            filteredBooks.Should().BeInAscendingOrder(b => b.Language);
        }
        [Fact]
        public void GetBooksByOrder_GivenIsbn_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var mockFileService = new Mock<IFileService>();
            var books = fixture.CreateMany<Book>(4).ToList();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("isbn");

            filteredBooks.Should().BeInAscendingOrder(b => b.Isbn);
        }
        [Fact]
        public void GetBooksByOrder_GivenTaken_ReturnCorrectBooks()
        {
            var fixture = new Fixture();
            var mockFileService = new Mock<IFileService>();
            var books = fixture.CreateMany<Book>(4).ToList();

            mockFileService.Setup(m => m.GetBooks()).Returns(books);

            var bookService = new BookService(mockFileService.Object);
            var filteredBooks = bookService.GetBooksByOrder("taken");

            filteredBooks.Should().BeInAscendingOrder(b => b.Taken);
        }

    }
}