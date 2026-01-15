using System.Data.Common;
using System.Reflection;
using EJournal.Entities;
using Microsoft.EntityFrameworkCore;

namespace EJournal.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<LessonDate> LessonDates { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<GroupSubject> GroupSubjects => Set<GroupSubject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupSubject>()
            .HasKey(gs => new { gs.GroupId, gs.SubjectId });

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Group)
            .WithMany(g => g.GroupSubjects)
            .HasForeignKey(gs => gs.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Subject)
            .WithMany(s => s.GroupSubjects)
            .HasForeignKey(gs => gs.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Group>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Group>()
            .HasMany(g => g.LessonDates)
            .WithOne(ld => ld.Group)
            .HasForeignKey(ld => ld.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LessonDate>()
            .HasMany(ld => ld.Grades)
            .WithOne(g => g.LessonDate)
            .HasForeignKey(g => g.LessonDateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Grades)
            .WithOne(g => g.Student)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}