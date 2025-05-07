using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookServices
    {
        private readonly IRepositoryManager _manager;

        public BookManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Book CreatOneBook(Book book)
        {
            if (book == null)
                throw new ArgumentException(nameof(book));

            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
                throw new Exception($"Book with id{id} could not found");

            _manager.Book.DeleteeOneBook(entity);
            _manager.Save();          
        }

        public IEnumerable<Book> GetallBooks(bool trackChanges)
        {
            return _manager.Book.GetAllBook(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            return _manager.Book.GetOneBookById(id,trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id,trackChanges);
            if (entity is null)
                throw new Exception($"Book with id{id} could not found");

            if(book is null)
                throw new ArgumentException(nameof (book));

            entity.Title = book.Title;
            entity.Price = book.Price;
            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
