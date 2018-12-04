using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    interface ISchoolDbContext : IDisposable
    {
        DbSet<School> Schools { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        int GetSaveChanges();
    }
}
