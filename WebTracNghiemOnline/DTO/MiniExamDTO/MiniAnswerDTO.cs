namespace WebTracNghiemOnline.DTO.MiniExamDTO
{
    public class MiniAnswerDTO
    {
        public int MiniAnswerId { get; set; }
        public string AnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class CreateMiniAnswerDTO
    {
        public string AnswerText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}