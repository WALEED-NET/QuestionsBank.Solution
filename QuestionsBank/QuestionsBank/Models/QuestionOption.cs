namespace QuestionsBank.Models;

/// <summary>
/// السؤال الفرعي - يطابق جدول Option في الـ ERD
/// مثال: "أ- الاسم:" هذا هو السؤال الفرعي
/// </summary>
public class QuestionOption
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty; // نص السؤال الفرعي
    public int Order { get; set; } = 1;
    public Guid? ActivityId { get; set; } // FK to Question (QuestionBank في ERD)
    public string? AudioUrl { get; set; }
    public string? ImgUrl { get; set; }
    
    // Navigation - الإجابات المتاحة لهذا السؤال
    public List<Answer> Answers { get; set; } = new();
    
    // Helper للحصول على الإجابة الصحيحة
    public Answer? CorrectAnswer => Answers.FirstOrDefault(a => a.IsCorrect);
}
