using BookSystemDemo.Data;
using BookSystemDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystemDemo.Repositories
{
    public class AuthorRepository : IRepository<Author, int>
    {
        private readonly BookContext context;

        public AuthorRepository(BookContext context) => this.context = context;

        public async Task<IEnumerable<Author>> GetAll() 
            => await context.Authors.OrderBy(a => a.LastName).ToListAsync();

        public async Task<Author> GetById(int id) 
            => await context.Authors.FindAsync(id);

        public async Task<Author> Insert(Author entity)
        {
            await context.Authors.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var book = await context.Authors.FindAsync(id);
            if (book != null)
            {
                context.Remove(book);
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
