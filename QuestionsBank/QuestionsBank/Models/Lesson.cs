namespace QuestionsBank.Models;

/// <summary>
/// الدرس - يطابق جدول Lesson في الـ ERD
/// </summary>
public class Lesson
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public int Order { get; set; } = 1;
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty; // للعرض فقط
    public bool IsActive { get; set; } = true;
    public string? Introduction { get; set; } // مقدمة الدرس
    public string? Goals { get; set; } // أهداف الدرس
    
    // Audit fields
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public Guid? CreatorId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public Guid? LastModifierId { get; set; }
    
    // Navigation
    public List<Topic> Topics { get; set; } = new();
    public List<Question> Questions { get; set; } = new(); // أسئلة مرتبطة بالدرس مباشرة
}

