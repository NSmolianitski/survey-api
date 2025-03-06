using Domain;

namespace Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task<Question?> TryGetByPublicIdAsync(Guid publicId);
    Task<List<Question>> GetOrderedSurveyQuestionsAsync(int surveyId);
}