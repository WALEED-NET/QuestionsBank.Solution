# نظرة عامة على المشروع - Questions Bank

## 📊 العلاقات في قاعدة البيانات (Backend ERD)

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                              Lesson (الدرس)                                  │
│  Id, Name, AudioUrl, [Order], UnitId, IsActive, Introduction, Goals         │
│  ExtraProperties, ConcurrencyStamp, CreationTime, CreatorId                 │
│  LastModificationTime, LastModifierId                                       │
└─────────────────┬───────────────────────────────────────┬───────────────────┘
                  │ 1:N                                   │ 1:N
                  ▼                                       ▼
┌─────────────────────────────────────┐   ┌─────────────────────────────────────┐
│           Topic (الموضوع)            │   │     QuestionBank (بنك الأسئلة)       │
│  Id, Name, AudioUrl, LessonId       │   │  Id, Question, AudioUrl, VideoUrl   │
│  IsActive, [Order], Description     │   │  QuestionType, [Order], LessonId    │
└─────────────────┬───────────────────┘   │  IsActive, Body, Discriminator      │
                  │ 1:N                   │  TopicId                            │
                  ▼                       └─────────────────┬───────────────────┘
┌─────────────────────────────────────┐                     │ 1:N
│         Content (المحتوى)           │                     ▼
│  Id, Title, IsActive, [Order]       │   ┌─────────────────────────────────────┐
│  Body, VideoUrl, AudioUrl, TopicId  │   │          Option (الخيار)            │
└─────────────────────────────────────┘   │  Id, Text, [Order], ActivityId      │
                                          │  AudioUrl, ImgUrl                   │
                                          └─────────────────┬───────────────────┘
                                                            │ 1:N
                                                            ▼
                                          ┌─────────────────────────────────────┐
                                          │          Answer (الإجابة)            │
                                          │  Id, Text, AudioUrl, ImgUrl         │
                                          │  [Order], IsCorrect, OptionId       │
                                          └─────────────────────────────────────┘
```

### العلاقات:
| العلاقة | النوع | الوصف |
|---------|-------|-------|
| Lesson → Topic | 1:N | درس واحد يحتوي على عدة مواضيع |
| Topic → Content | 1:N | موضوع واحد يحتوي على عدة محتويات |
| Lesson → QuestionBank | 1:N | درس واحد له عدة أسئلة مباشرة |
| Topic → QuestionBank | 1:N | موضوع واحد له عدة أسئلة |
| QuestionBank → Option | 1:N | سؤال واحد له عدة خيارات |
| Option → Answer | 1:N | خيار واحد له عدة إجابات |

---

## 🖥️ الموديلات الحالية في Frontend

### Lesson.cs
```csharp
public class Lesson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int Order { get; set; }
    public string? Goals { get; set; }
    public bool IsActive { get; set; }
    public string? AudioFileName { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<Topic> Topics { get; set; }      // Nested
    public List<Question> Questions { get; set; } // Nested
}
```

### Topic.cs
```csharp
public class Topic
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; }
    
    public List<Content> Contents { get; set; }   // Nested
    public List<Question> Questions { get; set; } // Nested
}
```

### Question.cs
```csharp
public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string? Body { get; set; }
    public QuestionType Type { get; set; }
    public bool IsActive { get; set; }
    public bool IsRequired { get; set; }
    public int Points { get; set; }
    public string? ImageFileName { get; set; }
    public string? CorrectFeedback { get; set; }
    public string? WrongFeedback { get; set; }
    
    public List<QuestionOption> Options { get; set; } // Nested
}
```

---

## 🔄 الفرق بين Backend و Frontend

| الجانب | Backend (ERD) | Frontend (حالياً) |
|--------|---------------|-------------------|
| نوع العلاقات | Foreign Keys (LessonId, TopicId) | Nested Objects (Lists) |
| الإجابات | جدول Answer منفصل + جدول Option | QuestionOption مع IsCorrect |
| التخزين | SQL Database | In-Memory Storage |
| الهدف | تخزين دائم مع علاقات | عرض وتحرير مؤقت |

### لماذا الاختلاف مقبول؟
```
┌──────────────────────────────────────────────────────────────────┐
│  Frontend Model (للعرض والتحرير)                                  │
│  ════════════════════════════════                                │
│  • Nested Objects = أسهل للـ Binding في Blazor                   │
│  • لا حاجة لـ Foreign Keys = أبسط للتعامل                        │
│  • يمكن تحويلها لـ DTOs عند الإرسال للـ API                       │
└──────────────────────────────────────────────────────────────────┘
                              ▼
                     [API Mapping/DTO]
                              ▼
┌──────────────────────────────────────────────────────────────────┐
│  Backend Model (للتخزين)                                         │
│  ═══════════════════════                                        │
│  • Foreign Keys = علاقات قاعدة البيانات                         │
│  • Normalized = لا تكرار للبيانات                                │
│  • Efficient Queries                                            │
└──────────────────────────────────────────────────────────────────┘
```

---

## ✅ ما تم إنجازه

### 1. هيكل المشروع
- [x] إعداد Blazor Server مع .NET 10
- [x] تثبيت Blazorise للـ UI Components
- [x] إعداد RTL والعربية
- [x] تصميم Layout احترافي

### 2. الموديلات
- [x] Lesson, Topic, Content, Question, QuestionOption
- [x] QuestionType enum (MultipleChoice, TrueFalse, FillBlank, Essay)
- [x] LessonService للـ In-Memory CRUD

### 3. صفحات الدروس
- [x] قائمة الدروس (Index) مع فلترة وبحث
- [x] إضافة/تعديل درس (Form)
- [x] إدارة المواضيع داخل الدرس
- [x] إدارة المحتوى داخل الموضوع
- [x] إدارة الأسئلة (للدرس والموضوع)

### 4. تحسينات UX/UI
- [x] جداول للعرض بدلاً من حقول مفتوحة دائماً
- [x] حقول الإضافة تظهر عند الطلب وتختفي بعد الحفظ
- [x] Collapse للأقسام لتوفير المساحة
- [x] أزرار تعديل/حذف في كل صف

---

## 🚀 الخطوات القادمة

### المرحلة 1: تحسينات UI/UX
- [ ] إصلاح مشكلة حقل الحالة (تم جزئياً)
- [ ] تحسين تصميم QuestionEditor
- [ ] إضافة Validation messages
- [ ] إضافة Toast notifications للعمليات
- [ ] تحسين الـ Mobile responsiveness

### المرحلة 2: الحقول الناقصة
- [ ] إضافة `Introduction` للـ Lesson
- [ ] إضافة `AudioUrl` للـ Topic
- [ ] إضافة `IsActive` للـ Content
- [ ] إضافة `VideoUrl`, `AudioUrl` للـ Question
- [ ] إضافة `ImgUrl`, `AudioUrl` للـ QuestionOption

### المرحلة 3: ميزات إضافية
- [ ] Import/Export JSON
- [ ] Drag & Drop لإعادة الترتيب
- [ ] نسخ الأسئلة بين الدروس
- [ ] معاينة الدرس/السؤال
- [ ] إحصائيات متقدمة

### المرحلة 4: التكامل مع Backend
- [ ] إنشاء DTOs للـ API
- [ ] HttpClient Service للـ API calls
- [ ] Mapping بين Frontend Models و DTOs
- [ ] Error handling للـ API

---

## 📁 هيكل الملفات الحالي

```
QuestionsBank/
├── Components/
│   ├── Layout/
│   │   ├── AppLayout.razor       # التخطيط الرئيسي
│   │   └── MainLayout.razor
│   ├── Pages/
│   │   ├── Lessons/
│   │   │   ├── Index.razor       # قائمة الدروس
│   │   │   └── Form.razor        # إضافة/تعديل درس
│   │   └── Home.razor
│   └── Shared/
│       ├── QuestionEditor.razor  # محرر الأسئلة
│       ├── StatsCard.razor       # بطاقة الإحصائيات
│       └── DeleteModal.razor     # مودال الحذف
├── Models/
│   ├── Lesson.cs
│   ├── Topic.cs
│   ├── Content.cs
│   ├── Question.cs
│   ├── QuestionOption.cs
│   ├── QuestionType.cs
│   ├── Units.cs
│   └── LessonService.cs
└── wwwroot/
    └── app.css                   # الأنماط المخصصة
```

---

## 💡 ملاحظات للتطوير

1. **عند التكامل مع API**: سنحتاج DTOs منفصلة تطابق الـ ERD
2. **Validation**: إضافة FluentValidation أو DataAnnotations
3. **State Management**: قد نحتاج Fluxor إذا كبر المشروع
4. **Testing**: إضافة Unit Tests للـ Services
