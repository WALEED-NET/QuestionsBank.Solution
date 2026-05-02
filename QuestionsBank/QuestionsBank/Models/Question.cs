namespace QuestionsBank.Models;

/// <summary>
/// النشاط/التمرين - يطابق جدول QuestionBank في الـ ERD
/// مثال: "الاختيار من متعدد" هذا هو النشاط الرئيسي
/// </summary>
public class Question
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty; // Question في ERD - عنوان النشاط
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public QuestionType Type { get; set; } = QuestionType.MultipleChoice; // QuestionType
    public int Order { get; set; } = 1;
    public Guid? LessonId { get; set; } // FK to Lesson
    public bool IsActive { get; set; } = true;
    public string? Body { get; set; } // شرح إضافي
    public Guid? TopicId { get; set; } // FK to Topic
    
    // حقول إضافية للـ Frontend
    public bool IsRequired { get; set; } = true;
    public int Points { get; set; } = 1;
    public string? ImageUrl { get; set; }
    public string? CorrectFeedback { get; set; }
    public string? WrongFeedback { get; set; }
    
    // Navigation - الأسئلة الفرعية (Options في ERD)
    public List<QuestionOption> Options { get; set; } = new();
}
