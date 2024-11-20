using System;
using System.Collections.Generic;

namespace Models;

public partial class Instructor
{
    public int Id { get; set; }

    public int SpecialityId { get; set; }

    public virtual ICollection<RealseCourse> RealseCourses { get; set; } = new List<RealseCourse>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual User? User { get; set; }
}
