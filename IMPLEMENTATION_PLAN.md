# خطة تنفيذ نظام إدارة المحتوى التعليمي - Blazor + Blazorise

## 📋 معلومات المشروع

- **اسم المشروع**: QuestionsBank
- **المكتبة**: Blazorise Bootstrap5 v2.1.1
- **البروتوتايب المرجعي**: Example-v2.html
- **اللغة**: العربية (RTL)

---

## ✅ الوضع الحالي (تم إنجازه)

- [x] تثبيت حزم Blazorise
- [x] إعداد Program.cs
- [x] إعداد App.razor مع RTL
- [x] إنشاء صفحة اختبار Test.razor
- [x] التحقق من عمل المكونات الأساسية

---

## 🎯 المرحلة 1: إعداد البنية الأساسية

### 1.1 إصلاح الأيقونات (أولوية عالية)
```html
<!-- في App.razor - استخدام FontAwesome من CDN -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" rel="stylesheet" />
```

### 1.2 تعديل MainLayout للتصميم الجديد
- إزالة Sidebar الافتراضي
- تصميم Header ثابت مشابه للبروتوتايب
- دعم RTL كامل

### 1.3 هيكل المجلدات
```
Components/
├── Layout/
│   ├── AppLayout.razor          ← تخطيط جديد بدون sidebar
│   └── AppLayout.razor.css
├── Pages/
│   └── Lessons/
│       ├── Index.razor          ← صفحة القائمة
│       └── Form.razor           ← صفحة الإضافة/التعديل
├── Shared/
│   ├── StatsCard.razor          ← بطاقة الإحصائيات
│   ├── TopicEditor.razor        ← محرر الموضوع
│   ├── ContentEditor.razor      ← محرر المحتوى
│   ├── QuestionEditor.razor     ← محرر السؤال
│   └── DeleteModal.razor        ← نافذة تأكيد الحذف
└── _Imports.razor
```

---

## 🎯 المرحلة 2: صفحة القائمة (Lessons/Index.razor)

### 2.1 المكونات المطلوبة

| العنصر | مكون Blazorise | الوصف |
|--------|----------------|--------|
| بطاقات الإحصائيات | `<Card>` | 4 بطاقات: إجمالي الدروس، نشطة، مواضيع، أسئلة |
| شريط البحث | `<TextEdit>` + `<Select>` | بحث + فلتر الوحدة + فلتر الحالة |
| جدول البيانات | `<Table Striped Hoverable>` | عرض الدروس |
| أزرار الإجراءات | `<Button>` | تعديل، حذف |
| زر إضافة | `<Button Color="Primary">` | في الـ Header |

### 2.2 الكود الأساسي
```razor
@page "/lessons"
@page "/"

<Container Fluid Class="py-4">
    <!-- Stats Cards -->
    <Row Class="mb-4">
        <Column ColumnSize="ColumnSize.Is3">
            <StatsCard Icon="fa-book" Value="@totalLessons" Label="إجمالي الدروس" Color="#3b82f6" />
        </Column>
        <!-- ... المزيد من البطاقات -->
    </Row>
    
    <!-- Search & Filter -->
    <Card Class="mb-4">
        <CardBody>
            <Row>
                <Column ColumnSize="ColumnSize.Is4">
                    <TextEdit Placeholder="بحث عن درس..." @bind-Text="searchTerm" />
                </Column>
                <Column ColumnSize="ColumnSize.Is4">
                    <Select @bind-SelectedValue="unitFilter">
                        <SelectItem Value="">جميع الوحدات</SelectItem>
                        <!-- ... -->
                    </Select>
                </Column>
            </Row>
        </CardBody>
    </Card>
    
    <!-- Data Table -->
    <Card>
        <CardBody Padding="Padding.Is0">
            <Table Striped Hoverable FullWidth>
                <!-- ... -->
            </Table>
        </CardBody>
    </Card>
</Container>

<!-- Delete Modal -->
<Modal @bind-Visible="deleteModalVisible">
    <!-- ... -->
</Modal>
```

---

## 🎯 المرحلة 3: صفحة النموذج (Lessons/Form.razor)

### 3.1 الأقسام الرئيسية

#### قسم بيانات الدرس (أزرق)
```razor
<Card Class="mb-4">
    <CardHeader Background="Background.Primary" TextColor="TextColor.White">
        <Icon Name="@("fa-book")" Class="me-2" />
        بيانات الدرس
    </CardHeader>
    <CardBody>
        <Row>
            <Column ColumnSize="ColumnSize.Is4">
                <Field>
                    <FieldLabel>اسم الدرس *</FieldLabel>
                    <TextEdit @bind-Text="lesson.Name" />
                </Field>
            </Column>
            <!-- المزيد من الحقول -->
        </Row>
    </CardBody>
</Card>
```

#### قسم المواضيع (أخضر)
```razor
<Card Class="mb-4">
    <CardHeader Background="Background.Success" TextColor="TextColor.White">
        <Div Class="d-flex justify-content-between align-items-center">
            <Span>
                <Icon Name="@("fa-tags")" Class="me-2" />
                المواضيع
            </Span>
            <Button Color="Color.Light" Size="Size.Small" Clicked="AddTopic">
                <Icon Name="@("fa-plus")" /> إضافة موضوع
            </Button>
        </Div>
    </CardHeader>
    <CardBody>
        @foreach (var topic in lesson.Topics)
        {
            <TopicEditor Topic="topic" OnRemove="() => RemoveTopic(topic)" />
        }
    </CardBody>
</Card>
```

#### قسم أسئلة الدرس (برتقالي)
```razor
<Card Class="mb-4">
    <CardHeader Style="background: #f97316;" TextColor="TextColor.White">
        <!-- ... -->
    </CardHeader>
    <CardBody>
        @foreach (var question in lesson.Questions)
        {
            <QuestionEditor Question="question" OnRemove="() => RemoveQuestion(question)" />
        }
    </CardBody>
</Card>
```

### 3.2 المكونات الفرعية

#### TopicEditor.razor
```razor
@typeparam TTopic

<Card Class="mb-3 card-animate" Style="border-right: 3px solid #22c55e;">
    <CardHeader Class="d-flex justify-content-between">
        <Badge Color="Color.Success">موضوع @Index</Badge>
        <Button Color="Color.Danger" Size="Size.Small" Outline Clicked="OnRemove">
            <Icon Name="@("fa-trash")" />
        </Button>
    </CardHeader>
    <CardBody>
        <!-- حقول الموضوع -->
        <!-- قسم المحتوى المتداخل -->
        <!-- قسم الأسئلة المتداخل -->
    </CardBody>
</Card>

@code {
    [Parameter] public TTopic Topic { get; set; }
    [Parameter] public int Index { get; set; }
    [Parameter] public EventCallback OnRemove { get; set; }
}
```

#### QuestionEditor.razor
```razor
<Card Class="mb-3 card-animate" Style="border-right: 3px solid #f97316;">
    <CardHeader>
        <Badge Style="background: #ffedd5; color: #c2410c;">سؤال @Index</Badge>
    </CardHeader>
    <CardBody>
        <Row>
            <Column ColumnSize="ColumnSize.Is8">
                <Field>
                    <FieldLabel>نص السؤال *</FieldLabel>
                    <TextEdit @bind-Text="Question.Text" />
                </Field>
            </Column>
            <Column ColumnSize="ColumnSize.Is4">
                <Field>
                    <FieldLabel>نوع السؤال</FieldLabel>
                    <Select @bind-SelectedValue="Question.Type">
                        <SelectItem Value="QuestionType.MultipleChoice">اختيار متعدد</SelectItem>
                        <SelectItem Value="QuestionType.TrueFalse">صح/خطأ</SelectItem>
                        <SelectItem Value="QuestionType.FillBlank">ملء الفراغ</SelectItem>
                        <SelectItem Value="QuestionType.Essay">مقالي</SelectItem>
                    </Select>
                </Field>
            </Column>
        </Row>
        
        <!-- خيارات الإجابة -->
        @if (Question.Type == QuestionType.MultipleChoice)
        {
            <Div Class="mt-3">
                @foreach (var option in Question.Options)
                {
                    <OptionEditor Option="option" QuestionId="Question.Id" />
                }
            </Div>
        }
    </CardBody>
</Card>
```

---

## 🎯 المرحلة 4: خدمة البيانات (LessonService)

### 4.1 تحديث الخدمة
```csharp
public class LessonService
{
    private List<LessonData> _lessons = new();
    
    public event Action OnChange;

    public List<LessonData> GetAll() => _lessons;
    
    public List<LessonData> Search(string term, string unit, string status)
    {
        return _lessons.Where(l => 
            (string.IsNullOrEmpty(term) || l.Name.Contains(term)) &&
            (string.IsNullOrEmpty(unit) || l.UnitId == unit) &&
            (string.IsNullOrEmpty(status) || (status == "active" ? l.IsActive : !l.IsActive))
        ).ToList();
    }
    
    public LessonData GetById(string id) => _lessons.FirstOrDefault(l => l.Id == id);
    
    public void Add(LessonData lesson)
    {
        lesson.Id = Guid.NewGuid().ToString();
        lesson.CreatedAt = DateTime.Now;
        _lessons.Add(lesson);
        OnChange?.Invoke();
    }
    
    public void Update(LessonData lesson)
    {
        var index = _lessons.FindIndex(l => l.Id == lesson.Id);
        if (index >= 0)
        {
            lesson.UpdatedAt = DateTime.Now;
            _lessons[index] = lesson;
            OnChange?.Invoke();
        }
    }
    
    public void Delete(string id)
    {
        _lessons.RemoveAll(l => l.Id == id);
        OnChange?.Invoke();
    }
}
```

### 4.2 تسجيل الخدمة في Program.cs
```csharp
builder.Services.AddSingleton<LessonService>();
```

---

## 🎯 المرحلة 5: التنقل والـ Routing

### 5.1 Routes
| المسار | الصفحة | الوصف |
|--------|--------|--------|
| `/` أو `/lessons` | Index.razor | قائمة الدروس |
| `/lessons/create` | Form.razor | إضافة درس جديد |
| `/lessons/edit/{id}` | Form.razor | تعديل درس |

### 5.2 التنقل
```razor
<!-- من القائمة -->
<Button Color="Color.Primary" Clicked="@(() => NavigationManager.NavigateTo("/lessons/create"))">
    إضافة درس جديد
</Button>

<!-- من النموذج -->
<Button Color="Color.Secondary" Clicked="@(() => NavigationManager.NavigateTo("/lessons"))">
    العودة للقائمة
</Button>
```

---

## 📅 جدول التنفيذ

| الأسبوع | المهمة | الأولوية |
|---------|--------|----------|
| **اليوم 1** | إنشاء AppLayout + تعديل Header | عالية |
| **اليوم 1** | إنشاء StatsCard component | عالية |
| **اليوم 2** | إنشاء صفحة Index (القائمة) | عالية |
| **اليوم 2** | إنشاء DeleteModal | عالية |
| **اليوم 3** | إنشاء صفحة Form (الأساس) | عالية |
| **اليوم 3** | إنشاء TopicEditor | متوسطة |
| **اليوم 4** | إنشاء ContentEditor | متوسطة |
| **اليوم 4** | إنشاء QuestionEditor | متوسطة |
| **اليوم 5** | تحديث LessonService | عالية |
| **اليوم 5** | ربط الصفحات والاختبار | عالية |

---

## 🔧 ملاحظات تقنية مهمة

### استخدام الأيقونات
استخدم أسماء FontAwesome كنصوص:
```razor
<Icon Name="@("fa-book")" />
<Icon Name="@("fa-plus")" />
<Icon Name="@("fa-trash")" />
<Icon Name="@("fa-edit")" />
```

### الأنماط المخصصة (CSS)
```css
/* ألوان الأقسام */
.section-lesson { border-right: 3px solid #3b82f6; }
.section-topic { border-right: 3px solid #22c55e; }
.section-content { border-right: 3px solid #a855f7; }
.section-question { border-right: 3px solid #f97316; }

/* أنيميشن */
.card-animate { animation: slideIn 0.3s ease; }
```

### مكونات Blazorise الرئيسية
- `Container`, `Row`, `Column` - التخطيط
- `Card`, `CardHeader`, `CardBody` - البطاقات
- `Table`, `TableHeader`, `TableBody`, `TableRow` - الجداول
- `TextEdit`, `Select`, `SelectItem` - عناصر الإدخال
- `Button`, `Buttons` - الأزرار
- `Badge` - الشارات
- `Modal`, `ModalContent`, `ModalHeader`, `ModalBody`, `ModalFooter` - النوافذ المنبثقة
- `Alert` - التنبيهات
- `Icon` - الأيقونات

---

## ✅ معايير النجاح

1. [ ] صفحة القائمة تعرض جميع الدروس
2. [ ] البحث والفلترة يعملان
3. [ ] إضافة درس جديد يعمل
4. [ ] تعديل درس موجود يعمل
5. [ ] حذف درس يعمل مع تأكيد
6. [ ] إضافة/حذف مواضيع ديناميكياً
7. [ ] إضافة/حذف محتوى ديناميكياً
8. [ ] إضافة/حذف أسئلة وخيارات ديناميكياً
9. [ ] التصميم مطابق للبروتوتايب
10. [ ] RTL يعمل بشكل صحيح

---

## 🚀 البدء الآن

### الخطوة التالية: إنشاء AppLayout.razor
هذا سيكون التخطيط الجديد بدون Sidebar، مع Header مشابه للبروتوتايب.
