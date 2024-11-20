using System;
using System.Collections.Generic;

namespace Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CourseSpecialityId { get; set; }

    public virtual Speciality CourseSpeciality { get; set; } = null!;

    public virtual ICollection<RealseCourse> RealseCourses { get; set; } = new List<RealseCourse>();
}
