namespace QuestionsBank.Models;

/// <summary>
/// Helper class for Unit dropdown
/// </summary>
public static class Units
{
    public static List<(int Id, string Name)> GetAll() => new()
    {
        (1, "الوحدة الأولى"),
        (2, "الوحدة الثانية"),
        (3, "الوحدة الثالثة"),
        (4, "الوحدة الرابعة"),
        (5, "الوحدة الخامسة")
    };
}
