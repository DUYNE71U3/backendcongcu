using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public interface IMiniExamRepository
    {
        Task<List<MiniExam>> GetAllExamsAsync();
        Task<MiniExam> GetExamByIdAsync(int id);
        Task<MiniExam> GetExamWithQuestionsAsync(int id);
        Task<MiniExam> CreateExamAsync(MiniExam exam);
        Task UpdateExamAsync(MiniExam exam);
        Task DeleteExamAsync(int id);
    }
}