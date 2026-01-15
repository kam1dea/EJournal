using System.ComponentModel.DataAnnotations.Schema;

namespace EJournal.Entities;

[Table("Semester")]
public class Semester
{
    public int SemesterId { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}