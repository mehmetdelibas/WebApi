using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatrues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookServices
    {
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBookAsync(BookParameters bookParameters, bool trackChanges);
        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
        Task<BookDto> CreatOneBookAsync(BookDtoForInsertion book);
        Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);

        Task SaveChangesForPatchesAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
    }
}
