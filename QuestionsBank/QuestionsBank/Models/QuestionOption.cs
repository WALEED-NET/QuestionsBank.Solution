namespace QuestionsBank.Models;

/// <summary>
/// خيار الإجابة - يطابق جدول Option في الـ ERD
/// </summary>
public class QuestionOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public int Order { get; set; } = 1;
    public Guid? QuestionId { get; set; } // FK to Question (ActivityId في ERD)
    public string? AudioUrl { get; set; }
    public string? ImgUrl { get; set; }
    
    // للإجابة الصحيحة (من جدول Answer في ERD)
    public bool IsCorrect { get; set; }
}
