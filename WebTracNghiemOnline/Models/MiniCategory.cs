using System.Collections.Generic;

namespace WebTracNghiemOnline.Models
{
    public class MiniCategory
    {
        public int MiniCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Navigation property
        public ICollection<MiniQuestion>? Questions { get; set; }
    }
}