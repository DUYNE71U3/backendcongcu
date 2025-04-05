using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public interface IMiniQuestionRepository
    {
        Task<List<MiniQuestion>> GetAllQuestionsAsync();
        Task<List<MiniQuestion>> GetQuestionsByCategoryIdAsync(int categoryId);
        Task<MiniQuestion> GetQuestionByIdAsync(int id);
        Task<List<MiniQuestion>> GetQuestionsByIdsAsync(List<int> ids);
        Task<MiniQuestion> CreateQuestionAsync(MiniQuestion question);
        Task UpdateQuestionAsync(MiniQuestion question);
        Task DeleteQuestionAsync(int id);
    }
}