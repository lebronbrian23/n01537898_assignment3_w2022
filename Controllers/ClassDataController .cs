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
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to connect to the MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Classes table of in the school database.
        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of Classes ( id, classname , classcode , startdate and finishdate)
        /// </returns>
        [HttpGet]
        [Route("api/Classdata/listClass/{searchkey?}")]
        public List<Class> ListClasses(string searchkey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "SELECT* FROM Classes";

            //check if search is not null then find Classes based on the search key
            if(searchkey != null)
            {

                query = query + " where lower(classname) = lower(@value)  or lower(classcode) = lower(@value) or  startdate = @value or finishdate = @value";
                cmd.Parameters.AddWithValue("@value" , searchkey);
                cmd.Prepare();
            }

            cmd.CommandText =  query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Class
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Class NewClass = new Class();
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();
                NewClass.ClassStartDate = ResultSet["startdate"].ToString().Substring(0, 10);
                NewClass.ClassFinishDate = ResultSet["finishdate"].ToString().Substring(0, 10); 
               
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Class names
            return Classes;
        }

        /// <summary>
        /// Returns a single Class in the databasr
        /// </summary>
        /// <example>GET api/ClassData/findClass/{id}</example>
        /// <returns>
        ///Class data ( id, classname , classcode , startdate and finishdate)
        /// </returns>
        [HttpGet]
        [Route("api/Classdata/findClass/{id}")]
        public Class findClass(int id)
        {
            // create a connection to the db 
            MySqlConnection Conn =  School.AccessDatabase();

            //open the connection between webserver and db
            Conn.Open();

            // establish a command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "SELECT * FROM Classes where Classid = " + id;

            //set results of the query in to a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty string for Class
            Class selectedClass = new Class();

            //lopp through each row
            while (ResultSet.Read())
            {
                //access column data from the db
                selectedClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                selectedClass.ClassCode = ResultSet["classcode"].ToString();
                selectedClass.ClassName = ResultSet["classname"].ToString();
                selectedClass.ClassStartDate = ResultSet["startdate"].ToString().Substring(0, 10);
                selectedClass.ClassFinishDate = ResultSet["finishdate"].ToString().Substring(0, 10);

            }
            //close connection
            Conn.Close();

            //return the final list of thee Class names
            return selectedClass;
        }

    }

}
