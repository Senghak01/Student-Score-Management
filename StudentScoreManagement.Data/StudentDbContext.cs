using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentScoreManagement.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = "server=localhost;port=3306;database=StudentDB;user=root;password=77778888;";
            optionsBuilder.UseMySql(conStr, ServerVersion.AutoDetect(conStr));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Student>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Subject>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Subject>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Subjects)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentID)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
