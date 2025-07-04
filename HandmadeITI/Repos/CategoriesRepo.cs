using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Repos
{
    public class CategoriesRepo : Irepo<Category>
    {
        private readonly ApplicationDbContext _context;
        public CategoriesRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Category entity cannot be null.");
            }
            await _context.Category.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var category = await GetById(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }
            else
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();

        }

        public async Task<Category> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return category;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
            var existingCategory = await GetById(entity.CategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {entity.CategoryId} not found.");
            }
            _context.Entry(existingCategory).CurrentValues.SetValues(entity);
        }
    }
}
