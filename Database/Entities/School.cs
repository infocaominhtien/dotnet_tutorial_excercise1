using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("school")]
[Index("PhoneNumber", IsUnique = true)]
[Index("Name", Name = "school_name_uindex", IsUnique = true)]
public class School
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("phone_number", TypeName = "char(15)")]
    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
