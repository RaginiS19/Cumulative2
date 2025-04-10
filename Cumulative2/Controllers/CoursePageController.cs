using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View("New", course);
            }

            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO courses 
                (coursecode, teacherid, startdate, finishdate, coursename) 
                VALUES (@code, @teacherid, @start, @finish, @name)";

            cmd.Parameters.AddWithValue("@code", course.CourseCode);
            cmd.Parameters.AddWithValue("@teacherid", course.TeacherId);
            cmd.Parameters.AddWithValue("@start", course.StartDate);
            cmd.Parameters.AddWithValue("@finish", course.FinishDate);
            cmd.Parameters.AddWithValue("@name", course.CourseName);

            cmd.ExecuteNonQuery();

            ViewBag.SuccessMessage = "Course added successfully!";
            return View("New");
        }

        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Course course = null;

            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM courses WHERE courseid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                course = new Course
                {
                    CourseId = reader.GetInt32("courseid"),
                    CourseCode = reader.GetString("coursecode"),
                    TeacherId = reader.IsDBNull(reader.GetOrdinal("teacherid")) ? (int?)null : reader.GetInt32("teacherid"),
                    StartDate = reader.GetDateTime("startdate"),
                    FinishDate = reader.GetDateTime("finishdate"),
                    CourseName = reader.GetString("coursename")
                };
            }

            reader.Close();

            if (course == null)
            {
                TempData["NotFoundError"] = "Course not found.";
                return RedirectToAction("New");
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM courses WHERE courseid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            TempData["DeleteSuccess"] = "Course deleted successfully!";
            return RedirectToAction("New");
        }
    }
}
