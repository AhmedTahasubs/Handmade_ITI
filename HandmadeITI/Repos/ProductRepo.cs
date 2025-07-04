using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Respo
{
    public class ProductRepo : Irepo<Product>
    {
        // Assuming you have a DbContext injected here for database operations
        private readonly ApplicationDbContext _context;
        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Models.Product> GetById(int? id)
        {
            return await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
        }
        public async Task<IEnumerable<Core.Models.Product>> GetAll()
        {
            return await _context.Product.Include(p => p.Category).Include(p => p.Seller).ToListAsync();
        }
        public async Task Add(Core.Models.Product entity)
        {
            await _context.Set<Core.Models.Product>().AddAsync(entity);
            
        }
        public async Task Update(Core.Models.Product entity)
        {
            _context.Set<Core.Models.Product>().Update(entity);
            
        }
        public async Task Delete(int id)
        {
            var product = await GetById(id);
            if (product != null)
            {
                _context.Set<Core.Models.Product>().Remove(product);
               
            }
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
    


