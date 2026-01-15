using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;

[Table("Student")]
public class Student
{
    public int StudentId { get; set; }
    public string FullName { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public List<Grade> Grades { get; set; } = new();
}