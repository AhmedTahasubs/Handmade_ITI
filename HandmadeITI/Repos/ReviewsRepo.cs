using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Respo;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Repos
{
    public class ReviewsRepo : Irepo<Review>
    {
        private readonly ApplicationDbContext _context;

        public ReviewsRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Review entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Review cannot be null.");
            }
            await _context.Review.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var review = await GetById(id);
            if (review != null)
            {
                _context.Review.Remove(review);
            }
            else
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Review
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Review> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            }
            var review = await _context.Review
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }
            return review;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Review entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Review cannot be null.");
            }
            _context.Review.Update(entity);
            await SaveChanges();
        }
    }
}
