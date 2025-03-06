using Application.Dto;
using FluentResults;

namespace Application.Interfaces.Services;

public interface IQuestionService
{
    Task<Result<QuestionResponseDto>> GetQuestionByPublicIdAsync(Guid questionId);
}