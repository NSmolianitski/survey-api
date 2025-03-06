namespace Domain;

public class Survey
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public ICollection<Question> Questions { get; set; } = [];
}