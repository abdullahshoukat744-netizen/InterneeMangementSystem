using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Wing { get; set; }

    public string? Section { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Internee> Internees { get; set; } = new List<Internee>();
}
