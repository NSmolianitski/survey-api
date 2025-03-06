namespace Domain;

public class Question
{
    public int Id { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid();

    public int SurveyId { get; set; }
    public Survey? Survey { get; set; }

    public int OrderId { get; set; }

    public string Text { get; set; } = string.Empty;

    public ICollection<Answer> Answers { get; set; } = [];
}