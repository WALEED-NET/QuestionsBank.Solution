namespace QuestionsBank.Models;

public class Content
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int Order { get; set; } = 1;
    public string? VideoUrl { get; set; }
    public string? AudioFileName { get; set; }
}
