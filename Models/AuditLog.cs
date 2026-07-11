using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class AuditLog
{
    public int AuditLogId { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public DateTime? ActionDate { get; set; }

    public string? Ipaddress { get; set; }

    public virtual User? User { get; set; }
}
