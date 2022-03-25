using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using School.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace School.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to connect to the MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Students table of in the school database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students ( id, first names , last names , enroldate and student number)
        /// </returns>
        [HttpGet]
        [Route("api/Studentdata/listStudent/{searchkey?}")]
        public List<Student> ListStudents(string searchkey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "SELECT* FROM Students";

            //check if search is not null then find Students based on the search key
            if(searchkey != null)
            {

                query = query + " where lower(studentfName) = lower(@value)  or lower(studentlName) = lower(@value) or  enroldate = @value or studentnumber = @value";
                cmd.Parameters.AddWithValue("@value" , searchkey);
                cmd.Prepare();
            }

            cmd.CommandText =  query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Student
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Student NewStudent = new Student();
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.StudentFName = ResultSet["studentfName"].ToString();
                NewStudent.StudentLName = ResultSet["studentlname"].ToString();
                NewStudent.StudentEnrolDate = ResultSet["enroldate"].ToString().Substring(0, 10); 
               
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Student names
            return Students;
        }

        [HttpGet]
        [Route("api/Studentdata/findStudent/{id}")]
        public Student findStudent(int id)
        {
            // create a connection to the db 
            MySqlConnection Conn =  School.AccessDatabase();

            //open the connection between webserver and db
            Conn.Open();

            // establish a command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "SELECT * FROM students where studentid = " + id;

            //set results of the query in to a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty string for Student
            Student selectedStudent = new Student();

            //lopp through each row
            while (ResultSet.Read())
            {
                //access column data from the db
                selectedStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                selectedStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                selectedStudent.StudentFName = ResultSet["studentfname"].ToString();
                selectedStudent.StudentLName = ResultSet["studentlname"].ToString();
                selectedStudent.StudentEnrolDate = ResultSet["enroldate"].ToString().Substring(0, 10);


            }
            //close connection
            Conn.Close();

            //return the final list of thee Student names
            return selectedStudent;
        }

    }

}
