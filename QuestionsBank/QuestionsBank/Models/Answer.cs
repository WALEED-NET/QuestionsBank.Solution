namespace QuestionsBank.Models;

/// <summary>
/// الإجابة - يطابق جدول Answer في الـ ERD
/// الإجابات هي الخيارات التي يختار منها الطالب للسؤال الفرعي
/// </summary>
public class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public string? AudioUrl { get; set; }
    public string? ImgUrl { get; set; }
    public int Order { get; set; } = 1;
    public bool IsCorrect { get; set; }
    public Guid OptionId { get; set; } // FK to Option (QuestionOption)
}
