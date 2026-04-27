namespace QuestionsBank.Models;

public class QuestionOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int Order { get; set; } = 1;
}
