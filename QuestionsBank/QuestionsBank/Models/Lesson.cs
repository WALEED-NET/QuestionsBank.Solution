namespace QuestionsBank.Models;

public class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public int Order { get; set; } = 1;
    public string? Goals { get; set; }
    public bool IsActive { get; set; } = true;
    public string? AudioFileName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public List<Topic> Topics { get; set; } = new();
    public List<Question> Questions { get; set; } = new(); // أسئلة مرتبطة بالدرس مباشرة
}

