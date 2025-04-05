using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.Access;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public class MiniCategoryRepository : IMiniCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public MiniCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MiniCategory>> GetAllCategoriesAsync()
        {
            return await _context.MiniCategories.ToListAsync();
        }

        public async Task<MiniCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.MiniCategories.FindAsync(id);
        }

        public async Task<MiniCategory> CreateCategoryAsync(MiniCategory category)
        {
            _context.MiniCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(MiniCategory category)
        {
            _context.MiniCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.MiniCategories.FindAsync(id);
            if (category != null)
            {
                _context.MiniCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
