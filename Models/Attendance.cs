using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? InterneeId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual Internee? Internee { get; set; }
}
