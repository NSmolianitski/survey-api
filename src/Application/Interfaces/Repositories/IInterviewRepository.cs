using Domain;

namespace Application.Interfaces.Repositories;

public interface IInterviewRepository
{
    Task<Interview?> TryGetByPublicIdAsync(Guid publicId);
    void AddInterview(Interview interview);
    Task SaveChangesAsync();
}