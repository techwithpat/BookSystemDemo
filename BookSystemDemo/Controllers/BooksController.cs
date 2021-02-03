using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystemDemo.Data;
using BookSystemDemo.Models;

namespace BookSystemDemo.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookContext _context;

        public BooksController(BookContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Author)
                                           .Include(b => b.Category)
                                           .ToListAsync();
            return View(books);
        }

        public IActionResult Create()
        {
            PopulateAuthorsList();
            PopulateCategoriesList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private async void PopulateAuthorsList()
        {
            var authors = await _context.Authors.OrderBy(a => a.LastName).ToListAsync();
            ViewBag.Authors = authors;
        }

        private async void PopulateCategoriesList()
        {
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Categories = categories; 
        }
    }
}
