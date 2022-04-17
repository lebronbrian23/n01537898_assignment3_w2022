using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using School.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;
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
        
        /// <summary>
        /// function to find a teacher by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            //cmd.CommandText = "select * from teachers where teacherid = @id";
            cmd.CommandText = "SELECT classes.teacherid ,teachers.teacherfname,teachers.teacherlname, teachers.hiredate,classes.classcode ,teachers.employeenumber , teachers.salary ,classes.classname FROM classes JOIN teachers on teachers.teacherid = classes.teacherid where classes.teacherid =  @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

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
                selectedTeacher.ClassName = ResultSet["classname"].ToString();
                selectedTeacher.ClassCode = ResultSet["classcode"].ToString();
                

            }
            //close connection
            Conn.Close();

            //return the final list of thee Teacher names
            return selectedTeacher;
        }

        /// <summary>
        /// function to add a new teacher to the school database
        /// </summary>
        /// <param name="NewTeacher"></param>
       [HttpPost]
       [Route("api/Teacherdata/AddTeacher")]
       [EnableCors(origins:"*" , methods:"*" , headers:"*")]
       public void AddTeacher(Teacher NewTeacher)
        {

            //create a connection to the db
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between web server and database
            Conn.Open();

            //create a new query for the db
            MySqlCommand cmd = Conn.CreateCommand();
           
            //sql query
            cmd.CommandText = "insert into teachers (teacherfname , teacherlname , employeenumber , hiredate , salary)" +
                "values (@teacherfname ,@teacherlname , @employeenumber ,@hiredate , @salary)";
            cmd.Parameters.AddWithValue("@teacherfname" ,NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname" , NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@employeenumber" ,NewTeacher.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.TeacherSalary);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.TeacherHireDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }


        /// <summary>
        /// method to delete a teacher from the db
        /// </summary>
        /// <param name="id">The primary  key</param>
        [HttpPost]
        [Route ("api/delete/teacher/{id}")]
        [EnableCors (origins:"*" , methods:"*" , headers:"*")]
        public void DeleteTeacher(int id)
        {
            //create connection instance
            MySqlConnection Conn = School.AccessDatabase();

            //open a connection between database server and web server
            Conn.Open ();

            //create a new command for the db
            MySqlCommand cmd = Conn.CreateCommand ();

            cmd.CommandText = "UPDATE classes SET teacherid = NULL WHERE teacherid = @id";
            
            cmd.Parameters.AddWithValue("@id" ,id);

            cmd.Prepare ();

            cmd.ExecuteNonQuery();

            //create a new delete teacher command for the db
            MySqlCommand deleteCmd = Conn.CreateCommand();

            deleteCmd.CommandText = "delete from teachers where teacherid = @id";

            deleteCmd.Parameters.AddWithValue("@id", id);

            deleteCmd.Prepare();

            deleteCmd.ExecuteNonQuery();

            Conn.Close ();


        }

        /// <summary>
        /// function to update a  teacher to the school database
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <param name="TeacherInfo"></param>

        [HttpPost]
        [Route("api/Teacherdata/UpdateTeacher")]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher([FromUri]int TeacherId, [FromBody]Teacher TeacherInfo)
        {

            //create a connection to the db
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between web server and database
            Conn.Open();

            //create a new query for the db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "update teachers set teacherfname=@teacherfname , employeenumber=@employeenumber , teacherlname=@teacherlname , hiredate=@hiredate , salary=@salary where teacherid=@teacherid";

            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.TeacherSalary);
            cmd.Parameters.AddWithValue("@hiredate", TeacherInfo.TeacherHireDate);
            cmd.Parameters.AddWithValue("@teacherid", TeacherId);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.TeacherEmployeeNumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }

}
