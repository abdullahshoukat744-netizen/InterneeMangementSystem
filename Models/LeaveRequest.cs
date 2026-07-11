using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class LeaveRequest
{
    public int LeaveRequestId { get; set; }

    public int? InterneeId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual Internee? Internee { get; set; }
}
