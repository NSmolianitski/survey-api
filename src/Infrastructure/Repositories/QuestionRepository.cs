using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class QuestionRepository(AppDbContext context) : IQuestionRepository
{
    public async Task<Question?> TryGetByPublicIdAsync(Guid publicId)
    {
        return await context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.PublicId == publicId);
    }

    public async Task<List<Question>> GetOrderedSurveyQuestionsAsync(int surveyId)
    {
        return await context.Questions
            .Where(q => q.SurveyId == surveyId)
            .OrderBy(q => q.OrderId)
            .ToListAsync();
    }
}