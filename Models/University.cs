using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class University
{
    public int UniversityId { get; set; }

    public string? UniversityName { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Internee> Internees { get; set; } = new List<Internee>();
}
