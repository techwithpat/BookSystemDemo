using Microsoft.EntityFrameworkCore;
using BookSystemDemo.Models;

namespace BookSystemDemo.Data
{
    public class BookContext : DbContext
    {
        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
