using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("enrollment")]
public class Enrollment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("enrollment_date")]
    public DateOnly EnrollmentDate { get; set; }

    [ForeignKey("CourseId")] // determine the specific foreign key column and name, ignore to use the default
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("StudentId")] // determine the specific foreign key column and name, ignore to use the default
    public virtual Student Student { get; set; } = null!;
}
