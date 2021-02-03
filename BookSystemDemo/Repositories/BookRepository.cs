using BookSystemDemo.Data;
using BookSystemDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystemDemo.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext context;

        public BookRepository(BookContext context) => this.context = context;

        public async Task Delete(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(book != null)
            {
                context.Books.Remove(book);
            }
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await context.Books.Include(b => b.Author)
                                      .Include(b => b.Category)
                                      .ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<Book> Insert(Book book)
        {
            await context.Books.AddAsync(book);
            return book;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
