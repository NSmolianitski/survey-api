namespace Domain;

public class Answer
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    public int QuestionId { get; set; }
    public Question? Question { get; set; }

    public string Value { get; set; } = string.Empty;
}