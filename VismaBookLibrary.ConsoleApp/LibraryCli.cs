using System;
using System.Globalization;
using VismaBookLibrary.ConsoleApp.Interfaces;
using VismaBookLibrary.ConsoleApp.Models;
using VismaBookLibrary.ConsoleApp.Services;

namespace VismaBookLibrary.ConsoleApp
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
            input = input.ToLower();
            if (input == "add")
            {
                var book = ReadBookFromInput();
                if (bookService.AddBook(book))
                {
                    _logger.WriteLine("Book was added");
                }
                else
                    _logger.WriteLine("Book was not added");

            }

            if (input == "borrow")
            {
                _logger.WriteLine("Can I get a name of the person who's borrowing the book?");
                var borrowedBy = _logger.ReadLine();
                var borrowedBookCount = bookService.GetBooksForUser(borrowedBy).Count;
                var maxBooksToBorrow = 3;
                if (borrowedBookCount >= maxBooksToBorrow)
                {
                    _logger.WriteLine($"You can't borrow more than {maxBooksToBorrow} books");
                    return;
                }
                _logger.WriteLine("Whats the name of the book?");
                var bookName = _logger.ReadLine();
                DateTime expextedReturnDate;
                bool dateConverted;
                bool dateValid = false;

                do
                {
                    _logger.WriteLine("Please enter book return date (d.M.yyyy)");
                    string dateInput = _logger.ReadLine();
                    dateConverted = DateTime.TryParseExact(dateInput, "d'.'M'.'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expextedReturnDate);
                    if (!dateConverted)
                    {
                        _logger.WriteLine("Invalid date try again, expected data type is d.M.yyyy");
                        continue;
                    }
                    if (expextedReturnDate < DateTime.Today)
                    {
                        _logger.WriteLine($"Return date can't be lower than {DateTime.Today.ToString("dd.MM.yyyy")}");
                        continue;
                    }
                    dateValid = DateTime.Today.AddMonths(2) >= expextedReturnDate;

                    if (!dateValid)
                    {
                        _logger.WriteLine("You can't borrow book for longer than 2 months");
                        _logger.WriteLine("Enter valid return date");

                    }


                }
                while (!dateConverted || !dateValid);

                if (bookService.BorrowBook(bookName, borrowedBy, expextedReturnDate))
                {
                    _logger.WriteLine("The book was borrowed successfuly");
                }
                else
                    _logger.WriteLine("Book was not borrowed");


            }

            if (input == "return")
            {

                _logger.WriteLine("What book do you wish to return?");
                string bookName = _logger.ReadLine();
                var bookToReturn = bookService.GetBook(bookName);
                if (bookToReturn == null)
                {
                    _logger.WriteLine("Book not found");
                    return;
                }
                else if (DateTime.Today > bookToReturn.expextedReturnDate)
                {
                    _logger.WriteLine("Someone used this book to fix the table, eh?");
                }
                if (bookService.ReturnBook(bookName))
                {
                    _logger.WriteLine("Book has been returned");
                }
                else
                    _logger.WriteLine("Book could not be returned");
            }

            if (input == "list")
            {
                _logger.WriteLine("How do you wish to filter books?");
                _logger.WriteLine("name, author, category, language, ISBN, taken, available");
                string orderBy = _logger.ReadLine();

                if (bookService.GetBooksByOrder(orderBy) != null)
                {
                    foreach (var book in bookService.GetBooksByOrder(orderBy))
                    {
                        _logger.WriteLine(book.ToString());
                    }
                }
                else
                    _logger.WriteLine("Filter name was incorrect, or library is empty");

            }

            if (input == "remove")
            {
                bool safeSwitch = false;
                _logger.WriteLine("Type ISBN of the book you wish to remove");
                do
                {
                    string bookIsbn = _logger.ReadLine();
                    if (bookIsbn.Length == 13)
                    {
                        if (bookService.RemoveBook(bookIsbn))
                        {
                            _logger.WriteLine("Book has been removed");
                        }
                        else
                            _logger.WriteLine("Book could not be removed");
                        safeSwitch = false;
                    }
                    else
                    {
                        _logger.WriteLine("Incorrect ISBN. ISBN must be made of 13 numbers");
                        safeSwitch = true;
                    }
                } while (safeSwitch);

            }

        }


        public Book ReadBookFromInput()
        {
            _logger.WriteLine("Please enter book name");
            var bookName = _logger.ReadLine();
            _logger.WriteLine("Please enter book author");
            var bookAuthor = _logger.ReadLine();
            _logger.WriteLine("Please enter book category");
            var bookCategory = _logger.ReadLine();
            _logger.WriteLine("Please enter book language");
            var bookLanguage = _logger.ReadLine();
            DateTime dateValue;
            bool dateConverted = false;
            do
            {
                _logger.WriteLine("Please enter book publication date (d.M.yyyy)");
                string bookPublicationDate = _logger.ReadLine();
                dateConverted = DateTime.TryParseExact(bookPublicationDate, "d'.'M'.'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);

                if (!dateConverted)
                    _logger.WriteLine("Invalid date try again, expected data type is d.M.yyyy");
            }
            while (!dateConverted);




            string bookIsbn = null;
            do
            {
                _logger.WriteLine("Please enter book ISBN (ISBN consists of 13 numbers)");
                bookIsbn = _logger.ReadLine();
            } while (bookIsbn.Length != 13);

            return new Book()
            {
                Name = bookName,
                Author = bookAuthor,
                Category = bookCategory,
                Language = bookLanguage,
                PublicationDate = dateValue,
                Isbn = bookIsbn,
                Taken = false,
            };
        }
    }
}
