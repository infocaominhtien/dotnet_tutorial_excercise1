using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("student")]
[Index("LastName", "FirstName", Name = "student_name_index", IsUnique = false)]
[Index("DateOfBirth", IsUnique = false)]
public class Student
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("first_name")]
    [StringLength(20)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(20)]
    public string LastName { get; set; } = null!;

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("school_id")]
    public int SchoolId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("SchoolId")]
    public virtual School School { get; set; } = null!;
}
