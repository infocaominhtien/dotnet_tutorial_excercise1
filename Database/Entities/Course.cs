using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Database.Entities;

[Table("course")]
[Index("Name", Name = "course_name_uindex", IsUnique = false)]
public class Course
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] [StringLength(255)] public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("credits")] public int Credits { get; set; }

    [Column("school_id")] public int SchoolId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("SchoolId")] // determine the specific foreign key column and name, ignore to use the default
    public virtual School School { get; set; } = null!;
}