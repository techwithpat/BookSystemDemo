using BookSystemDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystemDemo.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Insert(Book book);
        Task Delete(int id);
        Task Save();
    }
}
