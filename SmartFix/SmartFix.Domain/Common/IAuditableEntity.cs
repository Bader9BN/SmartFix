using System;

namespace SmartFix.Domain.Common
{
    /// <summary>
    /// يُستخدم لتسجل كل Create/Update/Delete على الكائنات لتعزيز الأمان (Audit Trail).
    /// </summary>
    public interface IAuditableEntity
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedAt { get; set; }
    }
}