# 🛠️ SmartFix - Smart Device Maintenance Management System

> **نظام متكامل لإدارة ورش ومراكز صيانة الأجهزة الذكية**  
> تم تطوير المشروع باستخدام أحدث تقنيات Microsoft .NET مع قاعدة بيانات Oracle وتطبيق جوال باستخدام Flutter، بتطبيق أفضل الممارسات والمعايير البرمجية (Clean Architecture & SOLID Principles).

---

## 🚀 عن المشروع (About The Project)

نظام **SmartFix** يقدم حلاً تكنولوجياً شاملاً لمراكز صيانة الهواتف والأجهزة الذكية، حيث يربط بين:
- **العملاء:** لمتابعة حالة إصلاح أجهزتهم وإنشاء طلبات الصيانة بسهولة عبر تطبيق الجوال.
- **الفنيين:** لإدارة طلبات الصيانة وتحديث حالة الأجهزة وتسجيل القطع المستهلكة.
- **المدراء:** لمتابعة أداء الورشة، إدارة المخزون، وتوليد الفواتير وتقارير الصيانة.

---

## ✨ المميزات الرئيسية (Key Features)

- 📱 **تطبيق جوال للعملاء (Flutter):** متابعة حالة التذكرة لحظة بلحظة برقم التذكرة الفريد.
- 💻 **لوحة تحكم إدارية (ASP.NET Core MVC):** شاشة تفاعلية باللغة العربية لدعم الاتجاه من اليمين لليار (RTL).
- 🔗 **RESTful Web API:** نقاط اتصال برمجية فائقة السرعة والأمان للربط مع تطبيقات الجوال والتطبيقات الخارجية.
- 🗄️ **سجل تاريخي للتغييرات (Audit Trail):** تتبع دقيق لكل تغيير في حالة التذكرة مع تسجيل وقت التعديل واسم المستخدم.
- 📦 **إدارة المخزون وقطع الغيار:** ربط التذاكر بقطع الغيار المستخدمة مع حفظ سعر القطعة وقت التثبيت للحفاظ على دقة الفواتير.
- 🧾 **نظام الفواتير:** توليد الفواتير وتتبع حالة الدفع (Unpaid, Paid, Refunded).

---

## 🏗️ الهيكلية المعمارية (System Architecture)

تم تصميم المشروع وفق نمط **N-Tier Architecture (Clean Architecture)** لفصل المسؤوليات وسهولة التوسع والاختبار:

```
SmartFix/
├── SmartFix.Domain/          # الكيانات الأساسية (Entities) والعلاقات
├── SmartFix.Application/     # خدمات منطق الأعمال (Business Logic & Interfaces)
├── SmartFix.Infrastructur/   # سياق قاعدة البيانات (EF Core DbContext) والاتصال بأوراكل
├── SmartFix.API/             # واجهات البرمجة لتطبيق الجوال (REST APIs)
├── SmartFix.MVC/             # لوحة التحكم والإدارة عبر المتصفح
├── SmartFix.Tests/           # اختبارات البرمجيات (Unit Tests)
├── db/                       # مخطط قاعدة بيانات أوراكل (Oracle DDL SQL)
└── smartfix_flutter/         # كود تطبيق الجوال بـ Flutter (Dart)
```

---

## 🛠️ التقنيات المستخدمة (Tech Stack)

### **Backend & Frameworks**
- **ASP.NET Core 8.0** (MVC & Web API)
- **C# 12**
- **Entity Framework Core 8.0**
- **Oracle Database (SQL)**

### **Frontend & Mobile**
- **Bootstrap 5** (مع دعم اللغة العربية RTL)
- **Razor Views**
- **Flutter / Dart**

### **Testing & Quality Assurance**
- **xUnit**
- **EF Core In-Memory Database**

---

## 📊 مخطط قاعدة البيانات (Database Schema Overview)

تتكون قاعدة البيانات في **Oracle** من الجداول التالية:
1. `USERS` - المستخدمين والصلاحيات (Customer, Technician, Admin).
2. `DEVICES` - الأجهزة المملوكة للعملاء.
3. `MAINTENANCETICKETS` - تذاكر الصيانة الأساسية.
4. `TICKETLOGS` - تتبع وتاريخ تغير حالات الصيانة.
5. `INVENTORYPARTS` - قطع الغيار والمخزون.
6. `TICKETPARTS` - القطع المستخدمة في كل تذكرة صيانة.
7. `INVOICES` - الفواتير وتفاصيل الدفع.

---

## ⚙️ طريقة التشغيل (Setup & Running)

### **1. إعداد قاعدة البيانات (Oracle SQL)**
قم بتنفيذ ملف السكريبت الموجود في المجلد الخاص بقواعد البيانات:
```bash
SmartFix/db/smartfix_oracle_schema.sql
```

### **2. تشغيل الـ Backend**
```bash
# تشغيل الـ Web API
dotnet run --project SmartFix/SmartFix.API

# تشغيل لوحة تحكم الـ MVC
dotnet run --project SmartFix/SmartFix.MVC
```

### **3. تشغيل الاختبارات البرمجية**
```bash
dotnet test SmartFix/SmartFix.Tests
```

---

## 👨‍💻 المطور (Author)

تم تطوير وتصميم هذا المشروع بواسطة **Developer** كنموذج عمل احترافي يوضح مهارات تطوير الأنظمة المتكاملة وشاملة الـ Full-Stack.
