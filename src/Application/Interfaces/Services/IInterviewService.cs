using Application.Dto;
using FluentResults;

namespace Application.Interfaces.Services;

public interface IInterviewService
{
    Task<Result<InterviewResponseDto>> CreateInterviewAsync();

    Task<Result<SaveQuestionResultResponseDto>> SaveQuestionResultAsync(
        Guid interviewPublicId,
        Guid questionPublicId,
        SaveQuestionResultRequestDto request);
}