using Domain;
using System;
using System.Linq;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SchoolDbContext : DbContext, ISchoolDbContext
    {
        public SchoolDbContext() { }
        public SchoolDbContext(DbContextOptions options)
       : base(options)
        { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseGrade> CourseGrades { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .ToTable("tbl_Schools");

            modelBuilder.Entity<Grade>()
                .ToTable("tbl_Grades")
                .HasOne(g => g.School)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SchoolId);

            modelBuilder.Entity<CourseGrade>()
                .ToTable("tbl_CourseGrade")
                .HasKey(cg => new { cg.CourseId, cg.GradeId });

            modelBuilder.Entity<CourseGrade>()
                .HasOne(cg => cg.Course)
                .WithMany(c => c.CourseGrades)
                .HasForeignKey(cg => cg.CourseId);

            modelBuilder.Entity<CourseGrade>()
                .HasOne(cg => cg.Grade)
                .WithMany(g => g.CourseGrades)
                .HasForeignKey(cg => cg.GradeId);

            modelBuilder.Entity<CourseStudent>()
                .ToTable("tbl_CourseStudent")
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<Section>()
                .ToTable("tbl_Sections")
                .HasOne(s => s.Grade)
                .WithMany(g => g.Sections)
                .HasForeignKey(s => s.GradeId);

            modelBuilder.Entity<Course>()
                .ToTable("tbl_Courses");

            modelBuilder.Entity<Student>()
                .ToTable("tbl_Students")
                .HasOne(s => s.Section)
                .WithMany(sc => sc.Students)
                .HasForeignKey(s => s.SectionId);
        }
        public int GetSaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
              .Where(x => x.Entity is BaseModel
                  && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                if (!(entry.Entity is BaseModel entity)) continue;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = Guid.NewGuid();
                }
                else
                {
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedBy = Guid.NewGuid();
                entity.UpdatedDate = now;
            }
            return base.SaveChanges();
        }

        public override void Dispose()
        { }
    }
}
