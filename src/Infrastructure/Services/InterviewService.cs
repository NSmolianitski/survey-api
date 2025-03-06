using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain;
using FluentResults;
using Microsoft.Extensions.Logging;
using Result = FluentResults.Result;

namespace Infrastructure.Services;

public class InterviewService(
    IUnitOfWork unitOfWork,
    ILogger<InterviewService> logger
) : IInterviewService
{
    public async Task<Result<InterviewResponseDto>> CreateInterviewAsync()
    {
        const int tempSurveyId = 1;
        var interview = new Interview
        {
            SurveyId = tempSurveyId
        };

        unitOfWork.InterviewRepository.AddInterview(interview);
        await unitOfWork.InterviewRepository.SaveChangesAsync();

        var orderedSurveyQuestions = await unitOfWork.QuestionRepository.GetOrderedSurveyQuestionsAsync(tempSurveyId);

        return new InterviewResponseDto(interview.PublicId, orderedSurveyQuestions[0].PublicId);
    }

    public async Task<Result<SaveQuestionResultResponseDto>> SaveQuestionResultAsync(
        Guid interviewPublicId,
        Guid questionPublicId,
        SaveQuestionResultRequestDto request)
    {
        var interview = await unitOfWork.InterviewRepository.TryGetByPublicIdAsync(interviewPublicId);
        if (interview is null)
            return Result.Fail($"Interview with id: '{interviewPublicId}' not found");

        var question = await unitOfWork.QuestionRepository.TryGetByPublicIdAsync(questionPublicId);
        if (question is null)
            return Result.Fail($"Question with id: '{questionPublicId}' not found");

        var selectedAnswers = question.Answers
            .Where(a => request.AnswerIds.Contains(a.PublicId))
            .ToList();
        if (selectedAnswers.Count != request.AnswerIds.Count)
            return Result.Fail("Some answers are missing or invalid");

        foreach (var selectedAnswer in selectedAnswers)
        {
            var resultEntity = new Domain.Result
            {
                InterviewId = interview.Id,
                QuestionId = selectedAnswer.QuestionId,
                AnswerId = selectedAnswer.Id
            };
            unitOfWork.ResultRepository.AddResult(resultEntity);
        }

        ++interview.CurrentStep;

        using (var transaction = await unitOfWork.BeginTransactionAsync())
        {
            try
            {
                await unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to save question result");
                await transaction.RollbackAsync();
                return Result.Fail("Error while saving the question result.");
            }
        }

        var orderedSurveyQuestions = await unitOfWork.QuestionRepository
            .GetOrderedSurveyQuestionsAsync(interview.SurveyId);
        if (orderedSurveyQuestions.Count == interview.CurrentStep)
            return Result.Ok().WithSuccess("Interview completed.");

        var nextQuestion = orderedSurveyQuestions.ElementAtOrDefault(interview.CurrentStep);
        if (nextQuestion is null)
            return Result.Fail("Next question is not found");

        return new SaveQuestionResultResponseDto(nextQuestion.PublicId);
    }
}