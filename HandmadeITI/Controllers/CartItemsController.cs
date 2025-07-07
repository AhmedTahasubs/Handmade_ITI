using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeITI.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Irepo<CartItem> db;

        public CartItemsController(ApplicationDbContext context, Irepo<CartItem> CartItemsRepo)
        {
            _context = context;
            db = CartItemsRepo;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await db.GetAll();
            return View(applicationDbContext);
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await db.GetById(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItemId,CartId,ProductId,Quantity,UnitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Add(cartItem);
                await db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,CartId,ProductId,Quantity,UnitPrice")] CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(cartItem);
                    await db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartItem.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Title", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await db.GetById(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem != null)
            {
                await db.Delete(id);
            }

            await db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.CartItemId == id);
        }
    }
}
