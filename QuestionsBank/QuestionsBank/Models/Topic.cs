namespace QuestionsBank.Models;

/// <summary>
/// الموضوع - يطابق جدول Topic في الـ ERD
/// </summary>
public class Topic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public Guid? LessonId { get; set; } // FK to Lesson
    public bool IsActive { get; set; } = true;
    public int Order { get; set; } = 1;
    public string? Description { get; set; }
    
    // Navigation
    public List<Content> Contents { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
}
