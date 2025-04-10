using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/CourseAPI")]
public class CourseAPIController : Controller
{
    private readonly SchoolDbContext _context = new SchoolDbContext();

    [HttpPost("AddCourse")]
    public IActionResult AddCourse([FromBody] Course course)
    {
        using var conn = _context.AccessDatabase();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO courses (coursecode, teacherid, startdate, finishdate, coursename) 
                            VALUES (@code, @teacherid, @start, @finish, @name)";
        cmd.Parameters.AddWithValue("@code", course.CourseCode);
        cmd.Parameters.AddWithValue("@teacherid", course.TeacherId);
        cmd.Parameters.AddWithValue("@start", course.StartDate);
        cmd.Parameters.AddWithValue("@finish", course.FinishDate);
        cmd.Parameters.AddWithValue("@name", course.CourseName);
        cmd.ExecuteNonQuery();
        return Ok("Course added successfully.");
    }

    [HttpDelete("DeleteCourse/{id}")]
    public IActionResult DeleteCourse(int id)
    {
        using var conn = _context.AccessDatabase();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM courses WHERE courseid = @id";
        cmd.Parameters.AddWithValue("@id", id);
        int rows = cmd.ExecuteNonQuery();
        if (rows == 0) return NotFound("Course not found.");
        return Ok("Course deleted.");
    }
}
