namespace WebTracNghiemOnline.Models
{
    public class MiniAnswer
    {
        public int MiniAnswerId { get; set; }
        public string AnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int MiniQuestionId { get; set; }
        
        // Navigation property
        public MiniQuestion? Question { get; set; }
    }
}