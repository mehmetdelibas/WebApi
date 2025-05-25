using Entities.Models;
using Entities.RequestFeatrues;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) :
            base(repositoryContext)
        {
            
        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);
        public async Task<PagedList<Book>> GetAllBookAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .ToListAsync();

            return PagedList<Book>
                .ToPagedList(books,
                bookParameters.PageNumber,
                bookParameters.PageSize);
        }
        
        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
             await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
