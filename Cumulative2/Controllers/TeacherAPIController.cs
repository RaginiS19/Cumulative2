using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
// API controller for managing teacher data
    [ApiController]
    [Route("api/TeacherAPI")]
    public class TeacherAPIController : Controller
    {
    // Initializes the SchoolDbContext to access the database
    // Adds a new teacher to the database
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // POST: api/TeacherAPI/AddTeacher
        [HttpPost("AddTeacher")]
        public IActionResult AddTeacher([FromBody] Teacher teacher)
        {
            //// Basic validation
            //if (string.IsNullOrWhiteSpace(teacher.TeacherFName) || string.IsNullOrWhiteSpace(teacher.TeacherLName))
            //    return BadRequest("First and Last name are required.");

            //if (teacher.HireDate > DateTime.Now)
            //    return BadRequest("Hire date cannot be in the future.");

            //if (string.IsNullOrWhiteSpace(teacher.EmployeeNumber) || !teacher.EmployeeNumber.StartsWith("T"))
            //    return BadRequest("Employee number must start with 'T'.");

            using var conn = _context.AccessDatabase();
            conn.Open();

            //// Check if EmployeeNumber already exists
            //var checkCmd = conn.CreateCommand();
            //checkCmd.CommandText = "SELECT COUNT(*) FROM teachers WHERE employeenumber = @empnum";
            //checkCmd.Parameters.AddWithValue("@empnum", teacher.EmployeeNumber);
            //int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
            //if (exists > 0)
            //    return Conflict("Employee number already exists.");

            // Insert the teacher
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO teachers 
                (teacherfname, teacherlname, employeenumber, hiredate, salary) 
                VALUES (@fname, @lname, @empnum, @hiredate, @salary)";

            cmd.Parameters.AddWithValue("@fname", teacher.TeacherFName);
            cmd.Parameters.AddWithValue("@lname", teacher.TeacherLName);
            cmd.Parameters.AddWithValue("@empnum", teacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", teacher.Salary);

            cmd.ExecuteNonQuery();

            return Ok("Teacher added successfully.");
        }

        // DELETE: api/TeacherAPI/DeleteTeacher/5
        [HttpDelete("DeleteTeacher/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
        // Establishes a database connection to delete the teacher
            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
                return NotFound("Teacher not found.");

            return Ok("Teacher deleted successfully.");
        }


    }
}
