using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTracNghiemOnline.DTO.MiniExamDTO;
using WebTracNghiemOnline.Service;

namespace WebTracNghiemOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniQuestionController : ControllerBase
    {
        private readonly IMiniExamService _miniExamService;

        public MiniQuestionController(IMiniExamService miniExamService)
        {
            _miniExamService = miniExamService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MiniQuestionDTO>>> GetAll()
        {
            var questions = await _miniExamService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<MiniQuestionDTO>>> GetByCategory(int categoryId)
        {
            var questions = await _miniExamService.GetQuestionsByCategoryAsync(categoryId);
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MiniQuestionDTO>> GetById(int id)
        {
            var question = await _miniExamService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();
            
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<MiniQuestionDTO>> Create(CreateMiniQuestionDTO dto)
        {
            var createdQuestion = await _miniExamService.CreateQuestionAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdQuestion.MiniQuestionId }, createdQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateMiniQuestionDTO dto)
        {
            await _miniExamService.UpdateQuestionAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _miniExamService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}