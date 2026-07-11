using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class Certificate
{
    public int CertificateId { get; set; }

    public int? InterneeId { get; set; }

    public string? CertificateType { get; set; }

    public DateOnly? IssueDate { get; set; }

    public string? Qrcode { get; set; }

    public string? CertificatePath { get; set; }

    public virtual Internee? Internee { get; set; }
}
