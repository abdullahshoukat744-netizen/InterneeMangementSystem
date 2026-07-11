using System;
using System.Collections.Generic;

namespace InterneManagementSystem.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int InterneeId { get; set; }

    public string? DocumentType { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadDate { get; set; }

    public virtual Internee Internee { get; set; } = null!;
}
