namespace Databaser_Labb3_V2.Models;

public class CourseInformation
{
    public string CourseName { get; set; }
    public Grade AverageGrade { get; set; }
    public Grade LowestGrade { get; set; }
    public Grade HighestGrade { get; set; }
}