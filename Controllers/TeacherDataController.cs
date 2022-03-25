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
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to connect to the MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of in the school database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers ( id, first names , last names , hire date and salary)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteacher/{searchkey?}")]
        public List<Teacher> ListTeachers(string searchkey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = " SELECT* FROM teachers";

            //check if search is not null then find teachers based on the search key
            if(searchkey != null)
            {

                query = query + " where lower(teacherfName) = lower(@value)  or lower(teacherlName) = lower(@value) or  hiredate = @value or salary = @value";
                cmd.Parameters.AddWithValue("@value" , searchkey);
                cmd.Prepare();
            }

            cmd.CommandText =  query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teacher
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.TeacherFName = ResultSet["teacherfName"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.TeacherHireDate = ResultSet["hiredate"].ToString().Substring(0, 10); 
                NewTeacher.TeacherSalary = ResultSet["salary"].ToString();
               
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Teachers;
        }

        [HttpGet]
        [Route("api/Teacherdata/findTeacher/{id}")]
        public Teacher findTeacher(int id)
        {
            // create a connection to the db 
            MySqlConnection Conn =  School.AccessDatabase();

            //open the connection between webserver and db
            Conn.Open();

            // establish a command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "select * from teachers where teacherid = " + id;

            //set results of the query in to a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty string for Teacher
            Teacher selectedTeacher = new Teacher();

            //lopp through each row
            while (ResultSet.Read())
            {
                //access column data from the db
                selectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                selectedTeacher.TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                selectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                selectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                selectedTeacher.TeacherHireDate = ResultSet["hiredate"].ToString().Substring(0, 10);
                selectedTeacher.TeacherSalary = ResultSet["salary"].ToString();


            }
            //close connection
            Conn.Close();

            //return the final list of thee Teacher names
            return selectedTeacher;
        }

    }

}
