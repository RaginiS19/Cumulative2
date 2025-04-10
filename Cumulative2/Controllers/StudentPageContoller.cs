using Microsoft.AspNetCore.Mvc;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View("New", student);
            }

            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO students 
                (studentfname, studentlname, studentnumber, enroldate) 
                VALUES (@fname, @lname, @number, @date)";

            cmd.Parameters.AddWithValue("@fname", student.StudentFName);
            cmd.Parameters.AddWithValue("@lname", student.StudentLName);
            cmd.Parameters.AddWithValue("@number", student.StudentNumber);
            cmd.Parameters.AddWithValue("@date", student.EnrolDate);

            cmd.ExecuteNonQuery();

            ViewBag.SuccessMessage = "Student added successfully!";
            return View("New");
        }

        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Student student = null;

            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students WHERE studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                student = new Student
                {
                    StudentId = reader.GetInt32("studentid"),
                    StudentFName = reader.GetString("studentfname"),
                    StudentLName = reader.GetString("studentlname"),
                    StudentNumber = reader.GetString("studentnumber"),
                    EnrolDate = reader.GetDateTime("enroldate")
                };
            }

            reader.Close();
            if (student == null)
            {
                TempData["NotFoundError"] = "Student not found.";
                return RedirectToAction("New");
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using var conn = _context.AccessDatabase();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM students WHERE studentid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            TempData["DeleteSuccess"] = "Student deleted successfully!";
            return RedirectToAction("New");
        }
    }
}
