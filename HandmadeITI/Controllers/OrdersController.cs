using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Repos;

namespace HandmadeITI.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepo ordersRepo;
        private readonly ApplicationDbContext _context;

        public OrdersController(IOrdersRepo _ordersRepo, ApplicationDbContext context)
        {
            ordersRepo = _ordersRepo;
            _context = context;
        }

        // GET: Orders
        public  IActionResult Index()
        {
            return View(ordersRepo.GetAllOrders());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = ordersRepo.GetOrder(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Set<User>(), "UserId", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CreatedAt,UserId,ShippingAddress,OrderStatus,PaymentStatus,TotalPrice,PaymentMethod")] Order order)
        {
            ViewBag.Errors = ModelState.SelectMany(kv => kv.Value.Errors.Select(e => $"Field: {kv.Key} - Error: {e.ErrorMessage}")).ToList();
            if (ModelState.IsValid)
            {
                ordersRepo.AddOrder(order);
                ordersRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = new SelectList(_context.Set<User>(), "UserId", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = ordersRepo.GetOrder(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.UserId = new SelectList(_context.Set<User>(), "UserId", "Email");
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CreatedAt,UserId,ShippingAddress,OrderStatus,PaymentStatus,TotalPrice,PaymentMethod")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   ordersRepo.UpdateOrder(order);
                    ordersRepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordersRepo.OrderExists(order.OrderId))
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
            ViewBag.UserId = new SelectList(_context.Set<User>(), "UserId", "Email");
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = ordersRepo.GetOrder(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ordersRepo.DeleteOrder(id);
            ordersRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
