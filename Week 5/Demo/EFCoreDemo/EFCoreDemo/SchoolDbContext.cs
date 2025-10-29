using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Teacher> Teachers => Set<Teacher>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            options.UseSqlite("Data Source = D:\\Nisharg\\RKIT_TRAINING\\Week 5\\Demo\\EFCoreDemo\\EFCoreDemo\\school.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}
