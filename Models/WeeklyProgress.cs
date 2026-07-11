using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class WeeklyProgress
{
    public int ProgressId { get; set; }

    public int? InterneeId { get; set; }

    public int? WeekNo { get; set; }

    public string? TaskAssigned { get; set; }

    public string? TaskCompleted { get; set; }

    public string? SupervisorRemarks { get; set; }

    public DateOnly? SubmissionDate { get; set; }

    public virtual Internee? Internee { get; set; }
}
