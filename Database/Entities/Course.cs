using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("course")]
[Index("SchoolId", Name = "course_school_id_fk")]
public class Course
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] [StringLength(255)] public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("credits")] public int Credits { get; set; }

    [Column("schoolId")] public int SchoolId { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("SchoolId")]
    [InverseProperty("Courses")]
    public virtual School School { get; set; } = null!;
}