using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebTracNghiemOnline.DTO.MiniExamDTO;
using WebTracNghiemOnline.Service;

namespace WebTracNghiemOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniExamController : ControllerBase
    {
        private readonly IMiniExamService _miniExamService;

        public MiniExamController(IMiniExamService miniExamService)
        {
            _miniExamService = miniExamService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MiniExamDTO>>> GetAll()
        {
            var exams = await _miniExamService.GetAllExamsAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MiniExamDetailDTO>> GetById(int id)
        {
            var exam = await _miniExamService.GetExamByIdAsync(id);
            if (exam == null)
                return NotFound();
            
            return Ok(exam);
        }

        [HttpPost]
        public async Task<ActionResult<MiniExamDetailDTO>> Create(CreateMiniExamDTO dto)
        {
            // Get the current user ID (you need to be authenticated)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
            
            try
            {
                var createdExam = await _miniExamService.CreateRandomExamAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = createdExam.MiniExamId }, createdExam);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _miniExamService.DeleteExamAsync(id);
            return NoContent();
        }
    }
}