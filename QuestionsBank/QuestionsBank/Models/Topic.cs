namespace QuestionsBank.Models;

public class Topic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Order { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    
    public List<Content> Contents { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
}
