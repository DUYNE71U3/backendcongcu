using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTracNghiemOnline.DTO.MiniExamDTO;
using WebTracNghiemOnline.Models;
using WebTracNghiemOnline.Repository;

namespace WebTracNghiemOnline.Service
{
    public class MiniExamService : IMiniExamService
    {
        private readonly IMiniCategoryRepository _categoryRepository;
        private readonly IMiniQuestionRepository _questionRepository;
        private readonly IMiniExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly Random _random = new Random();

        public MiniExamService(
            IMiniCategoryRepository categoryRepository,
            IMiniQuestionRepository questionRepository,
            IMiniExamRepository examRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _mapper = mapper;
        }

        #region Category Methods
        public async Task<List<MiniCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<MiniCategoryDTO>>(categories);
        }

        public async Task<MiniCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<MiniCategoryDTO>(category);
        }

        public async Task<MiniCategoryDTO> CreateCategoryAsync(CreateMiniCategoryDTO dto)
        {
            var category = _mapper.Map<MiniCategory>(dto);
            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
            return _mapper.Map<MiniCategoryDTO>(createdCategory);
        }

        public async Task UpdateCategoryAsync(int id, CreateMiniCategoryDTO dto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                category.Name = dto.Name;
                category.Description = dto.Description;
                await _categoryRepository.UpdateCategoryAsync(category);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
        #endregion

        #region Question Methods
        public async Task<List<MiniQuestionDTO>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();
            return _mapper.Map<List<MiniQuestionDTO>>(questions);
        }

        public async Task<List<MiniQuestionDTO>> GetQuestionsByCategoryAsync(int categoryId)
        {
            var questions = await _questionRepository.GetQuestionsByCategoryIdAsync(categoryId);
            return _mapper.Map<List<MiniQuestionDTO>>(questions);
        }

        public async Task<MiniQuestionDTO> GetQuestionByIdAsync(int id)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            return _mapper.Map<MiniQuestionDTO>(question);
        }

        public async Task<MiniQuestionDTO> CreateQuestionAsync(CreateMiniQuestionDTO dto)
        {
            var question = _mapper.Map<MiniQuestion>(dto);
            
            // Make sure at least one answer is marked as correct
            if (!dto.Answers.Any(a => a.IsCorrect))
            {
                throw new ArgumentException("At least one answer must be marked as correct");
            }
            
            // Create answers
            question.Answers = dto.Answers.Select(a => new MiniAnswer
            {
                AnswerText = a.AnswerText,
                IsCorrect = a.IsCorrect
            }).ToList();
            
            var createdQuestion = await _questionRepository.CreateQuestionAsync(question);
            return _mapper.Map<MiniQuestionDTO>(createdQuestion);
        }

        public async Task UpdateQuestionAsync(int id, CreateMiniQuestionDTO dto)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            if (question == null) return;

            question.QuestionText = dto.QuestionText;
            question.MiniCategoryId = dto.MiniCategoryId;
            
            // Update answers (simple implementation - delete old answers and add new ones)
            if (question.Answers != null)
            {
                question.Answers.Clear();
            }
            else
            {
                question.Answers = new List<MiniAnswer>();
            }

            foreach (var answerDto in dto.Answers)
            {
                question.Answers.Add(new MiniAnswer
                {
                    AnswerText = answerDto.AnswerText,
                    IsCorrect = answerDto.IsCorrect,
                    MiniQuestionId = question.MiniQuestionId
                });
            }

            await _questionRepository.UpdateQuestionAsync(question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
        }
        #endregion

        #region Exam Methods
        public async Task<List<MiniExamDTO>> GetAllExamsAsync()
        {
            var exams = await _examRepository.GetAllExamsAsync();
            return _mapper.Map<List<MiniExamDTO>>(exams);
        }

        public async Task<MiniExamDetailDTO> GetExamByIdAsync(int id)
        {
            var exam = await _examRepository.GetExamWithQuestionsAsync(id);
            return _mapper.Map<MiniExamDetailDTO>(exam);
        }

        public async Task<MiniExamDetailDTO> CreateRandomExamAsync(CreateMiniExamDTO dto, string userId)
        {
            var exam = new MiniExam
            {
                Title = dto.Title,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userId,
                Duration = dto.Duration,
                ExamQuestions = new List<MiniExamQuestion>()
            };

            // Process each category and randomly select questions
            foreach (var categoryItem in dto.Categories)
            {
                // Get all questions for this category
                var categoryQuestions = await _questionRepository.GetQuestionsByCategoryIdAsync(categoryItem.CategoryId);
                
                // Check if we have enough questions
                if (categoryQuestions.Count < categoryItem.QuestionCount)
                {
                    throw new ArgumentException($"Not enough questions in category {categoryItem.CategoryId}. " +
                                                $"Requested {categoryItem.QuestionCount}, available {categoryQuestions.Count}");
                }
                
                // Randomly select questions
                var selectedQuestionIds = categoryQuestions
                    .OrderBy(q => _random.Next())
                    .Take(categoryItem.QuestionCount)
                    .Select(q => q.MiniQuestionId)
                    .ToList();
                
                // Add questions to the exam
                foreach (var questionId in selectedQuestionIds)
                {
                    exam.ExamQuestions.Add(new MiniExamQuestion
                    {
                        MiniExamId = exam.MiniExamId, // Will be set after save
                        MiniQuestionId = questionId
                    });
                }
            }

            // Save the exam to the database
            var createdExam = await _examRepository.CreateExamAsync(exam);
            
            // Load the full exam with questions and return
            var examWithQuestions = await _examRepository.GetExamWithQuestionsAsync(createdExam.MiniExamId);
            return _mapper.Map<MiniExamDetailDTO>(examWithQuestions);
        }

        public async Task DeleteExamAsync(int id)
        {
            await _examRepository.DeleteExamAsync(id);
        }
        #endregion
    }
}