using Entities.DataTransferObject;
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
        IEnumerable<BookDto> GetallBooks(bool trackChanges);
        BookDto GetOneBookById(int id, bool trackChanges);
        BookDto CreatOneBook(BookDtoForInsertion book);
        void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
        (BookDtoForUpdate BookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges);

        void SaveChangesForPatches(BookDtoForUpdate bookDtoForUpdate, Book book);
    }
}
