using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Respo
{
    public class CartsRepo:  Irepo<Cart>
    {
       
        private readonly ApplicationDbContext _context;
        public CartsRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> GetById(int? id)
        {
            return await _context.Cart
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
        }
        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Cart.Include(p => p.User).ToListAsync();
        }
        public async Task Add(Cart entity)
        {
            await _context.Set<Cart>().AddAsync(entity);

        }
        public async Task Update(Cart entity)
        {
            _context.Set<Cart>().Update(entity);

        }
        public async Task Delete(int id)
        {
            var Cart = await GetById(id);
            if (Cart != null)
            {
                _context.Set<Cart>().Remove(Cart);

            }
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
