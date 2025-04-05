namespace WebTracNghiemOnline.DTO.MiniExamDTO
{
    public class MiniCategoryDTO
    {
        public int MiniCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CreateMiniCategoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}