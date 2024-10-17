using System.Text.Json.Serialization;

namespace WebApplication7.ViewsModel;

public class EnrollmentAPIModel
{
    [JsonPropertyName("student_id")]
    public int StudentId { get; set; }

    [JsonPropertyName("course_id")]
    public int CourseId { get; set; }
}