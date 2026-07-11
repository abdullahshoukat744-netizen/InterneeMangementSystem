using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class Internee
{
    public int InterneeId { get; set; }

    public string RegistrationNo { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FatherName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public int? UniversityId { get; set; }

    public string? DegreeProgram { get; set; }

    public int? Semester { get; set; }

    public decimal? Cgpa { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public int? InternshipMonths { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? DepartmentId { get; set; }

    public int? SupervisorId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<PerformanceEvaluation> PerformanceEvaluations { get; set; } = new List<PerformanceEvaluation>();

    public virtual User? Supervisor { get; set; }

    public virtual University? University { get; set; }

    public virtual ICollection<WeeklyProgress> WeeklyProgresses { get; set; } = new List<WeeklyProgress>();
}
