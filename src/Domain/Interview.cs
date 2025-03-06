namespace Domain;

public class Interview
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    public int SurveyId { get; set; }
    public Survey? Survey { get; set; }
    
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public int CurrentStep { get; set; } = 0;

    public ICollection<Result> Results { get; set; } = [];
}