# خطة إعداد Blazorise وتنفيذ التصميم

## 📋 نظرة عامة

هذه الخطة التفصيلية لإعداد مكتبة **Blazorise** مع **Bootstrap 5** في مشروع **Blazor Server/WASM** وتحويل تصميم `Example-v2.html` إلى مكونات Blazor.

---

## 🔍 الوضع الحالي (تم فحصه)

### ✅ ما تم تثبيته:
- **Blazorise.Bootstrap5** v2.1.1
- **Blazorise.Components** v2.1.1
- **Blazorise.Icons.FontAwesome** v2.1.1
- **Blazorise.Snackbar** v2.1.1

### ✅ ما تم إعداده:
1. **Program.cs**: تم إضافة خدمات Blazorise
2. **App.razor**: تم إضافة روابط CSS و RTL
3. **_Imports.razor**: تم إضافة `@using` statements

### ⚠️ مشاكل محتملة للفحص:
1. Bootstrap JS غير مضاف (مطلوب لبعض المكونات)
2. MainLayout قد يحتاج تعديل لدعم RTL بشكل أفضل
3. لم يتم اختبار عمل المكونات فعلياً

---

## 📦 المرحلة 1: التحقق من صحة الإعداد

### 1.1 فحص الحزم
```bash
cd QuestionsBank\QuestionsBank
dotnet restore
dotnet build
```

### 1.2 التأكد من ملفات CSS في App.razor
```html
<!-- الترتيب المطلوب (مهم جداً): -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.rtl.min.css" rel="stylesheet" />
<link href="_content/Blazorise/blazorise.css" rel="stylesheet" />
<link href="_content/Blazorise.Bootstrap5/blazorise.bootstrap5.css" rel="stylesheet" />
<link href="_content/Blazorise.Snackbar/blazorise.snackbar.css" rel="stylesheet" />
<link href="_content/Blazorise.Icons.FontAwesome/v6/css/all.min.css" rel="stylesheet" />
```

### 1.3 إضافة Bootstrap JS (مطلوب للـ Dropdowns و Modals)
```html
<!-- قبل blazor.web.js -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
```

---

## 🎨 المرحلة 2: إنشاء صفحة اختبار

### 2.1 إنشاء صفحة `Components/Pages/Test.razor`
هذه الصفحة للتأكد من عمل جميع مكونات Blazorise:
- Button (أزرار)
- Card (بطاقات)
- Modal (نوافذ منبثقة)
- Table (جداول)
- Form Controls (عناصر الإدخال)
- Snackbar (إشعارات)
- Icons (أيقونات)

---

## 🏗️ المرحلة 3: هيكل المشروع

### 3.1 هيكل المجلدات
```
Components/
├── Layout/
│   ├── MainLayout.razor        ← تخطيط رئيسي جديد
│   └── MainLayout.razor.css
├── Pages/
│   ├── Lessons/
│   │   ├── Index.razor         ← صفحة القائمة
│   │   └── Form.razor          ← صفحة الإضافة/التعديل
│   └── Test.razor              ← صفحة الاختبار
└── Shared/
    ├── LessonCard.razor        ← مكون البطاقة
    ├── TopicSection.razor      ← مكون الموضوع
    ├── ContentEditor.razor     ← محرر المحتوى
    └── QuestionEditor.razor    ← محرر الأسئلة
```

### 3.2 الملفات المطلوبة
| الملف | الغرض |
|--------|--------|
| `Models/Lesson.cs` | ✅ موجود - نماذج البيانات |
| `Models/LessonService.cs` | ✅ موجود - خدمة البيانات |

---

## 🎯 المرحلة 4: تحويل التصميم

### 4.1 تحليل Example-v2.html

#### الصفحة 1: قائمة الدروس (List Page)
| العنصر HTML | مكون Blazorise |
|-------------|----------------|
| Stats Cards | `<Card>` + `<CardBody>` |
| Search Input | `<TextEdit Placeholder="...">` |
| Select Filter | `<Select>` |
| Data Table | `<Table Striped Hoverable>` |
| Action Buttons | `<Button Color="Color.Primary">` |
| Delete Modal | `<Modal>` + `<ModalContent>` |

#### الصفحة 2: نموذج الدرس (Form Page)
| العنصر HTML | مكون Blazorise |
|-------------|----------------|
| Form Sections | `<Card>` مع ألوان مختلفة |
| Text Input | `<TextEdit>` |
| Textarea | `<MemoEdit Rows="3">` |
| Select | `<Select>` |
| Toggle Switch | `<Switch>` |
| File Upload | `<FilePicker>` |
| Nested Sections | `<Card>` متداخلة |
| Toast | `<SnackbarStack>` (موجود في App.razor) |

---

## 🔧 المرحلة 5: المكونات المخصصة

### 5.1 مكون بطاقة الإحصائيات
```razor
<Card Background="Background.{Color}">
    <CardBody>
        <Div Class="d-flex align-items-center gap-3">
            <Icon Name="..." IconSize="IconSize.Large" />
            <Div>
                <Heading Size="HeadingSize.Is3">@Value</Heading>
                <Text>@Label</Text>
            </Div>
        </Div>
    </CardBody>
</Card>
```

### 5.2 مكون السؤال
```razor
<Card Background="Background.Warning" Class="card-animate">
    <CardHeader>
        <Badge Color="Color.Warning">سؤال @Index</Badge>
        <CloseButton Clicked="OnRemove" />
    </CardHeader>
    <CardBody>
        <Field>
            <FieldLabel>نص السؤال</FieldLabel>
            <TextEdit @bind-Text="Question.Text" />
        </Field>
        <Field>
            <Select @bind-SelectedValue="Question.Type">
                <SelectItem Value="QuestionType.MultipleChoice">اختيار متعدد</SelectItem>
                ...
            </Select>
        </Field>
    </CardBody>
</Card>
```

---

## 📝 المرحلة 6: خطوات التنفيذ

### الخطوة 1: إصلاح الإعداد ✓
- [ ] إضافة Bootstrap JS
- [ ] التحقق من ترتيب CSS
- [ ] Build بدون أخطاء

### الخطوة 2: صفحة الاختبار ✓
- [ ] إنشاء Test.razor
- [ ] اختبار: Button, Card, Modal, Table, Form
- [ ] تشغيل المشروع والتحقق

### الخطوة 3: MainLayout الجديد ✓
- [ ] تصميم RTL
- [ ] Header ثابت
- [ ] تنقل بسيط

### الخطوة 4: صفحة القائمة (Lessons/Index.razor)
- [ ] بطاقات الإحصائيات
- [ ] شريط البحث والفلترة
- [ ] جدول البيانات
- [ ] أزرار الإجراءات
- [ ] Modal الحذف

### الخطوة 5: صفحة النموذج (Lessons/Form.razor)
- [ ] قسم بيانات الدرس
- [ ] قسم المواضيع (ديناميكي)
- [ ] قسم المحتوى (ديناميكي)
- [ ] قسم الأسئلة (ديناميكي)
- [ ] التنقل بين الصفحات

### الخطوة 6: الربط والاختبار
- [ ] ربط مع LessonService
- [ ] حفظ/تحميل البيانات
- [ ] اختبار شامل

---

## 🎨 أنماط CSS المخصصة

### app.css إضافات
```css
/* خط عربي */
* { font-family: 'Tajawal', sans-serif !important; }

/* ألوان مخصصة للأقسام */
.section-lesson { background: linear-gradient(to right, #dbeafe, #eff6ff); }
.section-topic { background: linear-gradient(to right, #dcfce7, #f0fdf4); }
.section-content { background: linear-gradient(to right, #f3e8ff, #faf5ff); }
.section-question { background: linear-gradient(to right, #ffedd5, #fff7ed); }

/* أنيميشن البطاقات */
.card-animate {
    animation: slideIn 0.3s ease;
}

@keyframes slideIn {
    from { opacity: 0; transform: translateY(-10px); }
    to { opacity: 1; transform: translateY(0); }
}

/* Nested Section Style */
.nested-section {
    margin-right: 20px;
    padding-right: 20px;
    border-right: 2px solid #e2e8f0;
    position: relative;
}
```

---

## ✅ معايير النجاح

1. [ ] المشروع يعمل بدون أخطاء
2. [ ] جميع مكونات Blazorise تعمل
3. [ ] RTL يعمل بشكل صحيح
4. [ ] التصميم مطابق لـ Example-v2.html
5. [ ] الـ CRUD يعمل (Create, Read, Update, Delete)
6. [ ] التنقل بين الصفحات يعمل

---

## 🔗 مراجع مهمة

- [Blazorise Documentation](https://blazorise.com/docs)
- [Blazorise Bootstrap5 Components](https://blazorise.com/docs/components/button)
- [Bootstrap 5 RTL](https://getbootstrap.com/docs/5.3/getting-started/rtl/)
