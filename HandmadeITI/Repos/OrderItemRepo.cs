using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Repos
{
    public class OrderItemRepo : Irepo<OrderItem>
    {
        private readonly ApplicationDbContext db;

        public OrderItemRepo(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task Add(OrderItem entity)
        {
            await db.OrderItem.AddAsync(entity);
        }

        public Task Delete(int id)
        {
            var orderItem = db.OrderItem.FindAsync(id).Result;
            if (orderItem != null)
            {
                db.OrderItem.Remove(orderItem);
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await db.OrderItem
                .Include(x => x.Order)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<OrderItem> GetById(int? id)
        {
            return await db.OrderItem
                .Include(x => x.Order)
                .Include(x => x.Product)
                .SingleOrDefaultAsync(i => i.OrderItemId == id);
        }

        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }

        public async Task Update(OrderItem entity)
        {
            db.OrderItem.Update(entity);
        }
    }
}
