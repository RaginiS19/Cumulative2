using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/StudentAPI")]
public class StudentAPIController : Controller
{
    private readonly SchoolDbContext _context = new SchoolDbContext();

    [HttpPost("AddStudent")]
    public IActionResult AddStudent([FromBody] Student student)
    {
        using var conn = _context.AccessDatabase();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO students (studentfname, studentlname, studentnumber, enroldate) 
                            VALUES (@fname, @lname, @number, @enroldate)";
        cmd.Parameters.AddWithValue("@fname", student.StudentFName);
        cmd.Parameters.AddWithValue("@lname", student.StudentLName);
        cmd.Parameters.AddWithValue("@number", student.StudentNumber);
        cmd.Parameters.AddWithValue("@enroldate", student.EnrolDate);
        cmd.ExecuteNonQuery();
        return Ok("Student added successfully.");
    }

    [HttpDelete("DeleteStudent/{id}")]
    public IActionResult DeleteStudent(int id)
    {
        using var conn = _context.AccessDatabase();
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM students WHERE studentid = @id";
        cmd.Parameters.AddWithValue("@id", id);
        int rows = cmd.ExecuteNonQuery();
        if (rows == 0) return NotFound("Student not found.");
        return Ok("Student deleted.");
    }
}
