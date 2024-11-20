using DAL;
using Data;
using Interfaces;
using Models;
using Interfaces;

namespace Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly SysDbContext _context;

    public UnitOfWork(SysDbContext context)
    {
        _context = context;

        Users = new Repository<User>(_context);
        Instructors = new Repository<Instructor>(_context);
        Courses = new Repository<Course>(_context);
        Specialities = new Repository<Speciality>(_context);
        RealseCourses = new Repository<RealseCourse>(_context);
        Enrollments = new Repository<Enrollment>(_context);
    }

    public IRepository<Instructor> Instructors { get; }
    public IRepository<User> Users { get; }
    public IRepository<Course> Courses { get; }
    
    public IRepository<Speciality> Specialities { get; }
    public Repository<RealseCourse> RealseCourses { get; }
    public Repository<Enrollment> Enrollments { get; }
    IRepository<RealseCourse> IUnitOfWork.RealseCourses { get; set; }

    IRepository<Enrollment> IUnitOfWork.Enrollments => throw new NotImplementedException();

    public void Dispose()
    {
        _context.Dispose();
    }

    public int Save()
    {
       return _context.SaveChanges();
    }
}
