using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystemDemo.Data;
using BookSystemDemo.Models;
using BookSystemDemo.Repositories;

namespace BookSystemDemo.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepository<Book, int> bookRepository;
        private readonly IRepository<Author, int> authorRepository;
        private readonly IRepository<Category, int> categoryRepository;

        public BooksController(IRepository<Book, int> bookRepository,
            IRepository<Author, int> authorRepository,
            IRepository<Category, int> categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await bookRepository.GetAll();
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
                await bookRepository.Insert(book);
                await bookRepository.Save();

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

            var book = await bookRepository.GetById((int)id);
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
            await bookRepository.Delete(id);
            await bookRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private async void PopulateAuthorsList()
        {
            var authors = await authorRepository.GetAll();
            ViewBag.Authors = authors;
        }

        private async void PopulateCategoriesList()
        {
            var categories = await categoryRepository.GetAll();
            ViewBag.Categories = categories; 
        }
    }
}
