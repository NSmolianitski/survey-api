using Application.Interfaces.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InterviewRepository(AppDbContext context) : IInterviewRepository
{
    public async Task<Interview?> TryGetByPublicIdAsync(Guid publicId) =>
        await context.Interviews
            .Include(i => i.Survey)
                .ThenInclude(s => s.Questions)
            .FirstOrDefaultAsync(i => i.PublicId == publicId);

    public void AddInterview(Interview interview) => context.Interviews.Add(interview);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}