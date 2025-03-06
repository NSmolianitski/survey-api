using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using FluentResults;

namespace Infrastructure.Services;

public class QuestionService(IUnitOfWork unitOfWork) : IQuestionService
{
    public async Task<Result<QuestionResponseDto>> GetQuestionByPublicIdAsync(Guid questionId)
    {
        var question = await unitOfWork.QuestionRepository.TryGetByPublicIdAsync(questionId);
        if (question is null)
            return Result.Fail($"Question with id: '{questionId}' not found");

        return new QuestionResponseDto(
            question.Text,
            question.Answers.Select(a => new AnswerResponseDto(a.PublicId, a.Value))
        );
    }
}