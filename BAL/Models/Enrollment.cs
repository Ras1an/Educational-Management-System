using System;
using System.Collections.Generic;

namespace Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public int CoursePatchId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public virtual RealseCourse CoursePatch { get; set; } = null!;
}
