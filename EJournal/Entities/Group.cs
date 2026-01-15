using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;


[Table("Group")]
public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; } = new();
    public List<LessonDate> LessonDates { get; set; } = new();
}