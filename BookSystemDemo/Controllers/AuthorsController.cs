using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookSystemDemo.Models;
using BookSystemDemo.Repositories;

namespace BookSystemDemo.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author, int> authorRepository;

        public AuthorsController(IRepository<Author, int> authorRepository) => this.authorRepository = authorRepository;

        public async Task<IActionResult> Index()
        {
            var authors = await authorRepository.GetAll();
            return View(authors);
        }
               
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                await authorRepository.Insert(author);
                await authorRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await authorRepository.GetById((int)id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await authorRepository.Delete(id);
            await authorRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
