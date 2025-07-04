using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Irepo<Review> _reviewRepo;


        public ReviewsController(ApplicationDbContext context, Irepo<Review> reviewRepo)
        {
            _context = context;
            _reviewRepo = reviewRepo;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewRepo.GetAll();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await _reviewRepo.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Email");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,ProductId,UserId,Rating,Comment")] Review review)
        {
            if (ModelState.IsValid)
            {
                await _reviewRepo.Add(review);
                await _reviewRepo.SaveChanges();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Email", review.UserId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await _reviewRepo.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Email", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,ProductId,UserId,Rating,Comment")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reviewRepo.Update(review);
                    await _reviewRepo.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", review.ProductId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "UserId", "Email", review.UserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await _reviewRepo.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _reviewRepo.GetById(id);
            if (review != null)
            {
                await _reviewRepo.Delete(id);
                await _reviewRepo.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            try
            {
                return _reviewRepo.GetById(id) != null;
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
