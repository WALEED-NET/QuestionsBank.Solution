namespace QuestionsBank.Models;

/// <summary>
/// المحتوى - يطابق جدول Content في الـ ERD
/// </summary>
public class Content
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int Order { get; set; } = 1;
    public string Body { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string? AudioUrl { get; set; }
    public Guid? TopicId { get; set; } // FK to Topic
}
