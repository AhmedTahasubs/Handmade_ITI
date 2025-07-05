using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Repos;
using HandmadeITI.Respo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Irepo<Category> _categoryRepo;

        public CategoriesController(ApplicationDbContext context, Irepo<Category> categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepo.GetAll();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _categoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,CreatedAt")] Category category)
        {
            if (ModelState.IsValid)
            {
                // Set CreatedAt to current time
                category.CreatedAt = DateTime.Now;
                await _categoryRepo.Add(category);
                await _categoryRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _categoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,CreatedAt")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Dont Update CreatedAt to current time Cause its Creation Time
                    await _categoryRepo.Update(category);
                    await _categoryRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _categoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryRepo.GetById(id);
            if (category != null)
            {
                await _categoryRepo.Delete(id);
                await _categoryRepo.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            try
            {
                return _categoryRepo.GetById(id) != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
    }
}
