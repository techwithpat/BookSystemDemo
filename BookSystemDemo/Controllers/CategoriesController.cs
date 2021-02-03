using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSystemDemo.Data;
using BookSystemDemo.Models;
using BookSystemDemo.Repositories;

namespace BookSystemDemo.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepository<Category, int> categoryRepository;

        public CategoriesController(IRepository<Category, int> categoryRepository) => this.categoryRepository = categoryRepository;

        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAll();
            return View(categories);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryRepository.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryRepository.Insert(category);
                await categoryRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryRepository.GetById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await categoryRepository.Delete(id);
            await categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
