using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VismaBookLibrary.ConsoleApp.Interfaces;
using VismaBookLibrary.ConsoleApp.Models;

namespace VismaBookLibrary.ConsoleApp.Services
{
    public class FileService : IFileService
    {
        private readonly string dataJsonUrl;
        private readonly string filePath;


        public FileService() : this("/Data/data.json")
        {
        }

        public FileService(string fileUrl)
        {
            dataJsonUrl = fileUrl;
            filePath = AppDomain.CurrentDomain.BaseDirectory + dataJsonUrl;
        }

        public List<Book> GetBooks()
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Book>>(jsonData)
                      ?? new List<Book>();

        }
        public bool ExportBooks(List<Book> bookList)
        {
            var bookListJson = JsonConvert.SerializeObject(bookList);
            System.IO.File.WriteAllText(filePath, bookListJson);
            return true;
        }
    }
}
