using System;
using System.Collections.Generic;

namespace Models;

public partial class RealseCourse
{
    public int CoursePatchId { get; set; }

    public int CourseId { get; set; }

    public int InstructorId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Instructor Instructor { get; set; } = null!;
}
