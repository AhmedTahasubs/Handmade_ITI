using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;

namespace HandmadeITI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Irepo<Product> db;

        public ProductController(ApplicationDbContext context, Irepo<Product> productRepo)
        {
            _context = context;
            db = productRepo;
        }


        // Fix for the CS0029 error in the Index method
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await db.GetAll();
            return View(applicationDbContext);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()//هتاخد منه id بتاعه هوه بس دا لو سيلار //admin عادي كل يظهر لي

        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["SellerId"] = new SelectList(_context.Set<User>(), "UserId", "Email");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Price,Description,Quantity,IsApproved,CreatedAt,SellerId,CategoryId")] Product product, IFormFile? imageFile)
        {
            // تحقق يدوي من الصورة
            if (imageFile == null || imageFile.Length == 0)
            {
                ViewBag.ImageError = "Please upload an image.";
                ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
                ViewData["SellerId"] = new SelectList(_context.Set<User>(), "UserId", "Email", product.SellerId);
                return View(product);
            }
            //ViewBag.Errors = ModelState.SelectMany(kv => kv.Value.Errors.Select(e => $"Field: {kv.Key} - Error: {e.ErrorMessage}")).ToList();



            if (ModelState.IsValid)
            {
                // حفظ الصورة
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // حفظ الـ URL
                product.ImageUrl = $"/images/{uniqueFileName}";
                product.CreatedAt = DateTime.Now;

                db.Add(product);
                await db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Set<User>(), "UserId", "Email", product.SellerId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Set<User>(), "UserId", "Email", product.SellerId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Price,Description,Quantity,IsApproved,CreatedAt,SellerId,CategoryId")] Product product, IFormFile? imageFile)
        {
            if (id != product.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the original product from DB
                    var existingProduct = await _context.Product.FindAsync(id);
                    if (existingProduct == null)
                        return NotFound();

                    // لو الصورة الجديدة موجودة
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // Replace image URL
                        existingProduct.ImageUrl = $"/images/{uniqueFileName}";
                    }

                    // Update باقي الخصائص
                    existingProduct.Title = product.Title;
                    existingProduct.Price = product.Price;
                    existingProduct.Description = product.Description;
                    existingProduct.Quantity = product.Quantity;
                    existingProduct.IsApproved = product.IsApproved;
                    existingProduct.CreatedAt = product.CreatedAt;
                    existingProduct.SellerId = product.SellerId;
                    existingProduct.CategoryId = product.CategoryId;

                    db.Update(existingProduct);
                    await db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Set<User>(), "UserId", "Email", product.SellerId);
            return View(product);
        }


        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
