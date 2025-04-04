using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // GET: /Teacher/New
        /// <summary>
        /// Display form to create a new teacher.
        /// </summary>
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        // POST: /Teacher/Create
        /// <summary>
        /// Handle form submission to add a new teacher to the database.
        /// </summary>
        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View("New", teacher);
            }

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO teachers 
                (teacherfname, teacherlname, employeenumber, hiredate, salary) 
                VALUES (@fname, @lname, @empnum, @hiredate, @salary)";

            cmd.Parameters.AddWithValue("@fname", teacher.TeacherFName);
            cmd.Parameters.AddWithValue("@lname", teacher.TeacherLName);
            cmd.Parameters.AddWithValue("@empnum", teacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", teacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", teacher.Salary);

            cmd.ExecuteNonQuery();
            conn.Close();

            ViewBag.SuccessMessage = "Teacher added successfully!";
            return View("New", teacher);
        }

        // GET: /Teacher/DeleteConfirm/{id}
        /// <summary>
        /// Show confirmation page before deleting a teacher.
        /// </summary>
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher teacher = null;

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                teacher = new Teacher
                {
                    TeacherId = reader.GetInt32("teacherid"),
                    TeacherFName = reader.GetString("teacherfname"),
                    TeacherLName = reader.GetString("teacherlname"),
                    EmployeeNumber = reader.GetString("employeenumber"),
                    HireDate = reader.GetDateTime("hiredate"),
                    Salary = reader.GetDecimal("salary")
                };
            }

            reader.Close();
            conn.Close();

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: /Teacher/Delete/{id}
        /// <summary>
        /// Confirm deletion of the teacher.
        /// </summary>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            conn.Close();

            TempData["DeleteSuccess"] = "Teacher deleted successfully!";
            return RedirectToAction("New");
        }
    }
}
