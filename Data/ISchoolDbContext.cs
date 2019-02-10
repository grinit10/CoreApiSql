using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public interface ISchoolDbContext : IDisposable
    {
        DbSet<School> Schools { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<CourseGrade> CourseGrades { get; set; }
        DbSet<CourseStudent> CourseStudents { get; set; }
        int GetSaveChanges();
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
