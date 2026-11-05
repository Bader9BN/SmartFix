const { Document, Packer, Paragraph, TextRun, HeadingLevel, AlignmentType, BorderStyle } = require('docx');
const fs = require('fs');

const doc = new Document({
    sections: [{
        properties: {},
        children: [
            new Paragraph({ text: 'BADER AL HARBI', heading: HeadingLevel.HEADING_1, alignment: AlignmentType.CENTER }),
            new Paragraph({ text: 'Full Stack .NET Developer | ASP.NET Core | C# | Oracle SQL | Flutter', alignment: AlignmentType.CENTER }),
            new Paragraph({ text: 'Riyadh, Saudi Arabia | +966 551485711 | baderalharbibn@gmail.com', alignment: AlignmentType.CENTER }),
            new Paragraph({ text: 'linkedin.com/in/bader-al-harbi-855794372', alignment: AlignmentType.CENTER }),
            
            new Paragraph({ text: 'SUMMARY', heading: HeadingLevel.HEADING_2, border: { bottom: { style: BorderStyle.SINGLE, size: 6, color: '000000' } } }),
            new Paragraph({ text: 'Ambitious Full Stack .NET Developer with hands-on experience building enterprise-grade applications. Recently developed a comprehensive Smart Device Maintenance Management System (SmartFix) showcasing expertise in Clean Architecture, ASP.NET Core MVC/API, Oracle SQL, and mobile integration with Flutter. Eager to contribute technical skills to impactful projects.' }),
            
            new Paragraph({ text: 'TECHNICAL SKILLS', heading: HeadingLevel.HEADING_2, border: { bottom: { style: BorderStyle.SINGLE, size: 6, color: '000000' } } }),
            new Paragraph({ text: 'Backend: C#, ASP.NET Core MVC, ASP.NET Core Web API, EF Core' }),
            new Paragraph({ text: 'Frontend/Mobile: Flutter, Dart, HTML, CSS, JavaScript, Bootstrap' }),
            new Paragraph({ text: 'Database: Oracle Database SQL, Entity Framework Core (InMemory)' }),
            new Paragraph({ text: 'Concepts: Clean Architecture, RESTful API, SDLC, State Management, Git/GitHub' }),
            
            new Paragraph({ text: 'KEY PROJECTS', heading: HeadingLevel.HEADING_2, border: { bottom: { style: BorderStyle.SINGLE, size: 6, color: '000000' } } }),
            new Paragraph({ text: 'SmartFix – Smart Device Maintenance Management System', bold: true }),
            new Paragraph({ text: '• Developed a comprehensive enterprise solution for managing device maintenance operations.' }),
            new Paragraph({ text: '• Architected using Clean Architecture principles separating Domain, Application, and Infrastructure layers.' }),
            new Paragraph({ text: '• Implemented RESTful APIs with ASP.NET Core and built a dynamic administrative dashboard using MVC.' }),
            new Paragraph({ text: '• Designed complex Oracle SQL schema with optimized relationships for maintenance logs, inventory, and invoices.' }),
            new Paragraph({ text: '• Integrated Flutter mobile app for customer request tracking and technician status updates.' }),
            
            new Paragraph({ text: 'EDUCATION', heading: HeadingLevel.HEADING_2, border: { bottom: { style: BorderStyle.SINGLE, size: 6, color: '000000' } } }),
            new Paragraph({ text: 'Diploma in Smart Devices Development | Majmaah University | GPA: 3.54/5 | Graduated: 2025' }),

            new Paragraph({ text: 'CERTIFICATIONS & TRAINING', heading: HeadingLevel.HEADING_2, border: { bottom: { style: BorderStyle.SINGLE, size: 6, color: '000000' } } }),
            new Paragraph({ text: '• ASP.NET Core MVC, C#, Oracle SQL, Flutter | Abad Network for Training (TVTC)' }),
            new Paragraph({ text: '• Foundations of User Experience (UX) Design | Google (Coursera)' })
        ],
    }],
});

Packer.toBuffer(doc).then((buffer) => {
    fs.writeFileSync('Bader_AlHarbi_Professional_CV.docx', buffer);
    console.log('CV Created');
});
