
using Models;

namespace  Interfaces;

public interface IUnitOfWork:IDisposable
{ 
    public IRepository<User> Users { get; }
    public IRepository<Instructor> Instructors { get; }
    public IRepository<Course> Courses { get; }

    public IRepository<Speciality> Specialities { get; }

    public IRepository<RealseCourse> RealseCourses { get; set;}

    public IRepository<Enrollment> Enrollments { get;}


    int Save();
}
