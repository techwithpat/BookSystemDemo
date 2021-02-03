using BookSystemDemo.Data;
using BookSystemDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystemDemo.Repositories
{
    public class CategoryRepository : IRepository<Category, int>
    {
        private readonly BookContext context;
        public CategoryRepository(BookContext context) => this.context = context;

        public async Task<IEnumerable<Category>> GetAll()
        => await context.Categories.OrderBy(a => a.Name).ToListAsync();

        public async Task<Category> GetById(int id)
         => await context.Categories.FindAsync(id);

        public async Task<Category> Insert(Category entity)
        {
            await context.Categories.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var book = await context.Categories.FindAsync(id);
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
