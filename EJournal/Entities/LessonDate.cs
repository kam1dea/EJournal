using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;

[Table("LessonDate")]
public class LessonDate
{
    public int LessonDateId { get; set; }
    public DateTime Date  {get; set;}
    public int SubjectId { get; set; }
    public int SemesterId  { get; set; }
    public int GroupId  { get; set; }
    
    public Group Group { get; set; }
    public Semester Semestr { get; set; }
    public Subject Subject { get; set; }

    public List<Grade> Grades { get; set; } = new();
}