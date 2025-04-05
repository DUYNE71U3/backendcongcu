namespace WebTracNghiemOnline.Models
{
    public class MiniExamQuestion
    {
        public int MiniExamId { get; set; }
        public int MiniQuestionId { get; set; }
        
        // Navigation properties
        public MiniExam? Exam { get; set; }
        public MiniQuestion? Question { get; set; }
    }
}