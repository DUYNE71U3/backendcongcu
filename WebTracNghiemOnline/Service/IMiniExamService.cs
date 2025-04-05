using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.DTO.MiniExamDTO;

namespace WebTracNghiemOnline.Service
{
    public interface IMiniExamService
    {
        // Category operations
        Task<List<MiniCategoryDTO>> GetAllCategoriesAsync();
        Task<MiniCategoryDTO> GetCategoryByIdAsync(int id);
        Task<MiniCategoryDTO> CreateCategoryAsync(CreateMiniCategoryDTO dto);
        Task UpdateCategoryAsync(int id, CreateMiniCategoryDTO dto);
        Task DeleteCategoryAsync(int id);

        // Question operations
        Task<List<MiniQuestionDTO>> GetAllQuestionsAsync();
        Task<List<MiniQuestionDTO>> GetQuestionsByCategoryAsync(int categoryId);
        Task<MiniQuestionDTO> GetQuestionByIdAsync(int id);
        Task<MiniQuestionDTO> CreateQuestionAsync(CreateMiniQuestionDTO dto);
        Task UpdateQuestionAsync(int id, CreateMiniQuestionDTO dto);
        Task DeleteQuestionAsync(int id);

        // Exam operations
        Task<List<MiniExamDTO>> GetAllExamsAsync();
        Task<MiniExamDetailDTO> GetExamByIdAsync(int id);
        Task<MiniExamDetailDTO> CreateRandomExamAsync(CreateMiniExamDTO dto, string userId);
        Task DeleteExamAsync(int id);
    }
}