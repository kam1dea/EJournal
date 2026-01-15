using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;

[Table("Grade")]
public class Grade
{
    public int GradeId { get; set; }
    public int StudentId { get; set; }
    public int LessonDateId { get; set; }
    public int? Value { get; set; } // от 1 до 5, null если нет
    
    public Student Student { get; set; }
    public LessonDate LessonDate { get; set; }
}