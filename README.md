# School Management Application

This project is a simple web application for managing teacher information, built using **ASP.NET MVC** and a **MySQL** database.

![School Management](https://img.icons8.com/external-flat-juicy-fish/50/000000/external-school-education-flat-flat-juicy-fish.png)

## Features

The application includes the following core features:

- 📜 **List Teachers**: Displays a list of all teachers with their ID and full name. Includes an optional search functionality to filter teachers by first name, last name, full name, hire date, or salary.
  
- 🧑‍🏫 **Show Teacher Details**: Displays detailed information for a specific teacher, including their ID, employee number, full name, hire date, and salary.
  
- ➕ **Add Teacher**: Adds a new teacher to the database via a **POST** request. Supports both traditional form submission and **AJAX**.
  
- ❌ **Delete Teacher**: Deletes an existing teacher from the database based on their ID via a **POST** request (now supports **AJAX**).

## Technologies Used

The following technologies were used to build this application:

- 🚀 **ASP.NET MVC**: The web framework used for building the application.
- 💻 **C#**: The programming language used for the backend logic.
- 🗃️ **MySQL**: The relational database used to store teacher and class information.
- 🔗 **MySql.Data**: The MySQL connector for .NET used to interact with the database.
- 🎨 **Bootstrap**: For front-end styling.
- 📱 **jQuery + AJAX**: For smooth asynchronous operations.

## Setup Instructions

### Prerequisites

To get started, you'll need the following:

- **Visual Studio 2022+** (or any compatible IDE)
- **.NET Framework (or .NET Core/5+ SDK)**
- **MySQL Server** running locally
- **MySQL Connector/NET** (installed via NuGet)

### Clone the Repository

Clone the repository to your local machine using the command:

```bash
git clone <repository_url>
cd <repository_directory>

### API Endpoints

#### List All Teachers

GET /api/TeacherData/ListTeachers


#### Search Teachers
GET /api/TeacherData/ListTeachers/{SearchKey?}

#### Get Teacher Details by ID
GET /api/TeacherData/FindTeacher/{id}

#### Add a New Teacher
POST /api/TeacherData/AddTeacher

#### Request Body (JSON format):
{
  "TeacherFname": "FirstName",
  "TeacherLname": "Lastname",
  "EmployeeNumber": "T1234",
  "HireDate": "2024-01-15",
  "Salary": 55
}

#### Delete a Teacher by ID
POST /api/TeacherData/DeleteTeacher/{id}

#### Testing the APIs
# List all teachers
curl http://localhost:<your_port>/api/TeacherData/ListTeachers

# Find a teacher by ID
curl http://localhost:<your_port>/api/TeacherData/FindTeacher/1

# Add a new teacher (example with JSON data)
curl -X POST -H "Content-Type: application/json" -d '{
  "TeacherFname": "John",
  "TeacherLname": "Doe",
  "EmployeeNumber": "T1234",
  "HireDate": "2024-01-01",
  "Salary": 65000
}' http://localhost:<your_port>/api/TeacherData/AddTeacher

# Delete teacher with ID 3
curl -X POST http://localhost:<your_port>/api/TeacherData/DeleteTeacher/3















