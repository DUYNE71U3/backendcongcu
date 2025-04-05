using System.Collections.Generic;

namespace WebTracNghiemOnline.DTO.MiniExamDTO
{
    public class MiniQuestionDTO
    {
        public int MiniQuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int MiniCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<MiniAnswerDTO> Answers { get; set; } = new List<MiniAnswerDTO>();
    }

    public class CreateMiniQuestionDTO
    {
        public string QuestionText { get; set; } = string.Empty;
        public int MiniCategoryId { get; set; }
        public List<CreateMiniAnswerDTO> Answers { get; set; } = new List<CreateMiniAnswerDTO>();
    }
}