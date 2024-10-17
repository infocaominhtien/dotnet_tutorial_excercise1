using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("school")]
public class School
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Name { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [InverseProperty("School")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [InverseProperty("School")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
