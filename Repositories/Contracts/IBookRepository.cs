using Entities.Models;
using Entities.RequestFeatrues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBookAsync(BookParameters bookParameters, bool trackChanges);

        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);

        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);

    }
}
