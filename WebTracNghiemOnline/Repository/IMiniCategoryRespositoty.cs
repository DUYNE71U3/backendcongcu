using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.Models;

namespace WebTracNghiemOnline.Repository
{
    public interface IMiniCategoryRepository
    {
        Task<List<MiniCategory>> GetAllCategoriesAsync();
        Task<MiniCategory> GetCategoryByIdAsync(int id);
        Task<MiniCategory> CreateCategoryAsync(MiniCategory category);
        Task UpdateCategoryAsync(MiniCategory category);
        Task DeleteCategoryAsync(int id);
    }
}