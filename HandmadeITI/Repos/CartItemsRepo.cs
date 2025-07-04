using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using Microsoft.EntityFrameworkCore;
namespace HandmadeITI.Respo
{
    public class CartItemsRepo: Irepo<CartItem>
    {
        private readonly ApplicationDbContext _context;
        public CartItemsRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CartItem> GetById(int? id)
        {
            return await _context.CartItem
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartItemId == id);
        }
        public async Task<IEnumerable<CartItem>> GetAll()
        {
            return _context.CartItem.Include(c => c.Cart).Include(c => c.Product);
        }
        public async Task Add(CartItem entity)
        {
            await _context.Set<CartItem>().AddAsync(entity);

        }
        public async Task Update(CartItem entity)
        {
            _context.Set<CartItem>().Update(entity);

        }
        public async Task Delete(int id)
        {
            var CartItem = await GetById(id);
            if (CartItem != null)
            {
                _context.Set<CartItem>().Remove(CartItem);

            }
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
