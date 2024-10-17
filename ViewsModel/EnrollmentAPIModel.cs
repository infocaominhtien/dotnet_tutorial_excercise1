using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.ViewsModel;

public class EnrollmentAPIModel
{
    [Column("student_id")]
    public int StudentId { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }
}