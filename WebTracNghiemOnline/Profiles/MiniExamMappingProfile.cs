using AutoMapper;
using WebTracNghiemOnline.DTO.MiniExamDTO;
using WebTracNghiemOnline.Models;
using System.Linq;

namespace WebTracNghiemOnline.Profiles
{
    public class MiniExamMappingProfile : Profile
    {
        public MiniExamMappingProfile()
        {
            // Category mappings
            CreateMap<MiniCategory, MiniCategoryDTO>();
            CreateMap<CreateMiniCategoryDTO, MiniCategory>();

            // Answer mappings
            CreateMap<MiniAnswer, MiniAnswerDTO>();
            CreateMap<CreateMiniAnswerDTO, MiniAnswer>();

            // Question mappings
            CreateMap<MiniQuestion, MiniQuestionDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));
            CreateMap<CreateMiniQuestionDTO, MiniQuestion>();

            // Exam mappings
            CreateMap<MiniExam, MiniExamDTO>()
                .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.ExamQuestions.Count));

            CreateMap<MiniExam, MiniExamDetailDTO>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.ExamQuestions.Select(eq => eq.Question)));
        }
    }
}