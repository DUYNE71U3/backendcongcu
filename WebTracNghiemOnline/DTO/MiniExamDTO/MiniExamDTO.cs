using System;
using System.Collections.Generic;

namespace WebTracNghiemOnline.DTO.MiniExamDTO
{
    public class MiniExamDTO
    {
        public int MiniExamId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int Duration { get; set; }
        public int QuestionCount { get; set; }
    }

    public class MiniExamDetailDTO
    {
        public int MiniExamId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public int Duration { get; set; }
        public List<MiniQuestionDTO> Questions { get; set; } = new List<MiniQuestionDTO>();
    }

    public class CreateMiniExamDTO
    {
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; } = 60; // Default 60 minutes
        public List<CategoryQuestionCount> Categories { get; set; } = new List<CategoryQuestionCount>();
    }

    public class CategoryQuestionCount
    {
        public int CategoryId { get; set; }
        public int QuestionCount { get; set; }
    }
}