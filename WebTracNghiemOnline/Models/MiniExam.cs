using System;
using System.Collections.Generic;

namespace WebTracNghiemOnline.Models
{
    public class MiniExam
    {
        public int MiniExamId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public int Duration { get; set; } // Thời gian làm bài (phút)
        
        // Navigation property
        public ICollection<MiniExamQuestion>? ExamQuestions { get; set; }
    }
}