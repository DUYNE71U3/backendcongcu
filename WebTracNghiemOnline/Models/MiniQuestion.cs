using System.Collections.Generic;

namespace WebTracNghiemOnline.Models
{
    public class MiniQuestion
    {
        public int MiniQuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int MiniCategoryId { get; set; }
        
        // Navigation properties
        public MiniCategory? Category { get; set; }
        public ICollection<MiniAnswer>? Answers { get; set; }
        public ICollection<MiniExamQuestion>? ExamQuestions { get; set; }
    }
}