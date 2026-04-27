namespace QuestionsBank.Models;

public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public string? Body { get; set; } // شرح إضافي
    public QuestionType Type { get; set; } = QuestionType.MultipleChoice;
    public bool IsActive { get; set; } = true;
    public bool IsRequired { get; set; } = true;
    public int Points { get; set; } = 1; // نقاط السؤال
    public string? ImageFileName { get; set; } // صورة للسؤال
    public string? CorrectFeedback { get; set; } // رسالة عند الإجابة الصحيحة
    public string? WrongFeedback { get; set; } // رسالة عند الإجابة الخاطئة
    
    public List<QuestionOption> Options { get; set; } = new();
}
