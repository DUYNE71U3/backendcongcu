using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracNghiemOnline.Access;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public class MiniQuestionRepository : IMiniQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public MiniQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MiniQuestion>> GetAllQuestionsAsync()
        {
            return await _context.MiniQuestions
                .Include(q => q.Category)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task<List<MiniQuestion>> GetQuestionsByCategoryIdAsync(int categoryId)
        {
            return await _context.MiniQuestions
                .Include(q => q.Answers)
                .Where(q => q.MiniCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<MiniQuestion> GetQuestionByIdAsync(int id)
        {
            return await _context.MiniQuestions
                .Include(q => q.Category)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.MiniQuestionId == id);
        }

        public async Task<List<MiniQuestion>> GetQuestionsByIdsAsync(List<int> ids)
        {
            return await _context.MiniQuestions
                .Include(q => q.Category)
                .Include(q => q.Answers)
                .Where(q => ids.Contains(q.MiniQuestionId))
                .ToListAsync();
        }

        public async Task<MiniQuestion> CreateQuestionAsync(MiniQuestion question)
        {
            _context.MiniQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task UpdateQuestionAsync(MiniQuestion question)
        {
            _context.MiniQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.MiniQuestions.FindAsync(id);
            if (question != null)
            {
                _context.MiniQuestions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }
    }
}