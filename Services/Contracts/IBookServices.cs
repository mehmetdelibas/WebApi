using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookServices
    {
        IEnumerable<Book> GetallBooks(bool trackChanges);
        Book GetOneBookById(int id, bool trackChanges);
        Book CreatOneBook(Book book);
        void UpdateOneBook(int id, Book book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
