using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class PerformanceEvaluation
{
    public int EvaluationId { get; set; }

    public int? InterneeId { get; set; }

    public int? Discipline { get; set; }

    public int? Attendance { get; set; }

    public int? Communication { get; set; }

    public int? TechnicalSkills { get; set; }

    public int? Teamwork { get; set; }

    public int? TotalMarks { get; set; }

    public string? Grade { get; set; }

    public DateOnly? EvaluationDate { get; set; }

    public virtual Internee? Internee { get; set; }
}
