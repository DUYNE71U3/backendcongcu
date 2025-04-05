using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.DTO.MiniExamDTO;
using WebTracNghiemOnline.Service;

namespace WebTracNghiemOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniCategoryController : ControllerBase
    {
        private readonly IMiniExamService _miniExamService;

        public MiniCategoryController(IMiniExamService miniExamService)
        {
            _miniExamService = miniExamService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MiniCategoryDTO>>> GetAll()
        {
            var categories = await _miniExamService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MiniCategoryDTO>> GetById(int id)
        {
            var category = await _miniExamService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<MiniCategoryDTO>> Create(CreateMiniCategoryDTO dto)
        {
            var createdCategory = await _miniExamService.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.MiniCategoryId }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMiniCategoryDTO dto)
        {
            await _miniExamService.UpdateCategoryAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _miniExamService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}