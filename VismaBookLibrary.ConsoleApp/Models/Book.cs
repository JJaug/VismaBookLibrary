using System;

namespace VismaBookLibrary.ConsoleApp.Models
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Isbn { get; set; }
        public bool Taken { get; set; }
        public string TakenBy { get; set; }
        public DateTime? expextedReturnDate { get; set; }
        public DateTime? borrowDate { get; set; }


        public override string ToString()
        {
            return $"Book Name: {Name}, Book Author {Author}, Book Category {Category}, Book Language {Language}," +
                $"Book Publication Date {PublicationDate.ToString("dd.MM.yyyy")}, Book ISBN {Isbn}, Is Book Taken: {Taken}";
            ;
        }
    }
}
