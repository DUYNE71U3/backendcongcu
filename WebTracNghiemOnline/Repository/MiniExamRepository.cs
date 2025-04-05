using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.Access;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public class MiniExamRepository : IMiniExamRepository
    {
        private readonly ApplicationDbContext _context;

        public MiniExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MiniExam>> GetAllExamsAsync()
        {
            return await _context.MiniExams.ToListAsync();
        }

        public async Task<MiniExam> GetExamByIdAsync(int id)
        {
            return await _context.MiniExams.FindAsync(id);
        }

        public async Task<MiniExam> GetExamWithQuestionsAsync(int id)
        {
            return await _context.MiniExams
                .Include(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.Answers)
                .Include(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.Category)
                .FirstOrDefaultAsync(e => e.MiniExamId == id);
        }

        public async Task<MiniExam> CreateExamAsync(MiniExam exam)
        {
            _context.MiniExams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task UpdateExamAsync(MiniExam exam)
        {
            _context.MiniExams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _context.MiniExams.FindAsync(id);
            if (exam != null)
            {
                _context.MiniExams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }
    }
}