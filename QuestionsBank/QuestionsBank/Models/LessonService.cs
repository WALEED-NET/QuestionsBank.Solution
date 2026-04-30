namespace QuestionsBank.Models;

public class LessonService
{
    private readonly List<Lesson> _lessons = new();

    public LessonService()
    {
        // بيانات تجريبية
        _lessons.Add(new Lesson
        {
            Name = "مقدمة في البرمجة",
            UnitId = 1,
            UnitName = "الوحدة الأولى",
            Order = 1,
            Goals = "فهم أساسيات البرمجة والمتغيرات",
            IsActive = true,
            Topics = new List<Topic>
            {
                new Topic
                {
                    Name = "المتغيرات",
                    Description = "شرح المتغيرات وأنواعها",
                    Order = 1,
                    Contents = new List<Content>
                    {
                        new Content { Title = "ما هي المتغيرات؟", Body = "المتغير هو مكان في الذاكرة...", Order = 1 }
                    },
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Text = "ما هو المتغير؟",
                            Type = QuestionType.MultipleChoice,
                            Points = 2,
                            Options = new List<QuestionOption>
                            {
                                new() { Text = "مكان في الذاكرة لتخزين البيانات", IsCorrect = true, Order = 1 },
                                new() { Text = "نوع من الدوال", IsCorrect = false, Order = 2 },
                                new() { Text = "لغة برمجة", IsCorrect = false, Order = 3 }
                            }
                        }
                    }
                }
            }
        });

        _lessons.Add(new Lesson
        {
            Name = "الدوال والإجراءات",
            UnitId = 1,
            UnitName = "الوحدة الأولى",
            Order = 2,
            Goals = "تعلم كيفية إنشاء واستخدام الدوال",
            IsActive = true
        });

        _lessons.Add(new Lesson
        {
            Name = "الشروط والتكرار",
            UnitId = 2,
            UnitName = "الوحدة الثانية",
            Order = 1,
            Goals = "فهم جمل الشرط والحلقات التكرارية",
            IsActive = false
        });
    }

    public List<Lesson> GetAll() => _lessons.OrderBy(l => l.UnitId).ThenBy(l => l.Order).ToList();

    public Lesson? GetById(Guid id) => _lessons.FirstOrDefault(l => l.Id == id);

    public void Add(Lesson lesson)
    {
        lesson.Id = Guid.NewGuid();
        lesson.CreationTime = DateTime.Now;
        var unit = Units.GetAll().FirstOrDefault(u => u.Id == lesson.UnitId);
        lesson.UnitName = unit.Name ?? "غير محدد";
        _lessons.Add(lesson);
    }

    public void Update(Lesson lesson)
    {
        var existing = _lessons.FirstOrDefault(l => l.Id == lesson.Id);
        if (existing != null)
        {
            var index = _lessons.IndexOf(existing);
            var unit = Units.GetAll().FirstOrDefault(u => u.Id == lesson.UnitId);
            lesson.UnitName = unit.Name ?? "غير محدد";
            lesson.LastModificationTime = DateTime.Now;
            _lessons[index] = lesson;
        }
    }

    public void Delete(Guid id)
    {
        var lesson = _lessons.FirstOrDefault(l => l.Id == id);
        if (lesson != null)
        {
            _lessons.Remove(lesson);
        }
    }

    public Lesson CreateNew() => new Lesson
    {
        Name = string.Empty,
        UnitId = 1,
        Order = _lessons.Count + 1,
        IsActive = true
    };
}
