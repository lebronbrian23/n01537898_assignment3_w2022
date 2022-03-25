using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using School.Models;
using MySql.Data.MySqlClient;

namespace School.Controllers
{
    public class AuthorDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/AuthorData/ListAuthors</example>
        /// <returns>
        /// A list of authors ( id, first names , last names and join date)
        /// </returns>
        [HttpGet]
        [Route("api/authordata/listauthors/{searchkey?}")]
        public List<Author> ListAuthors(string searchkey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from Authors";

            if(searchkey != null)
            {
                query = query + " where lower(authorfName)=lower(@value) or lower(authorlName)=lower(@value) ";
                cmd.Parameters.AddWithValue("@value" , searchkey);
                cmd.Prepare();
            }

            cmd.CommandText =  query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Author Names
            List<Author> Authors = new List<Author> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Author NewAuthor = new Author();
                NewAuthor.AuthorId = Convert.ToInt32(ResultSet["authorId"]);
                NewAuthor.AuthorFName = ResultSet["authorfName"].ToString();
                NewAuthor.AuthorLName = ResultSet["authorlname"].ToString();
                NewAuthor.AuthorDate = ResultSet["authorjoindate"].ToString();
               
                Authors.Add(NewAuthor);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Authors;
        }

        [HttpGet]
        [Route("api/authordata/findauthor/{id}")]
        public Author findAuthor(int id)
        {
            // create a connection to the db 
            MySqlConnection Conn =  School.AccessDatabase();

            //open the connection between webserver and db
            Conn.Open();

            // establish a command for the db
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "select * from authors where authorid = " + id;

            //set results of the query in to a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty string for author
            Author selectedAuthor = new Author();

            //lopp through each row
            while (ResultSet.Read())
            {
                //access column data from the db
                selectedAuthor.AuthorId = Convert.ToInt32(ResultSet["authorId"]);
                selectedAuthor.AuthorFName = ResultSet["authorfname"].ToString();
                selectedAuthor.AuthorLName = ResultSet["authorlname"].ToString();
                selectedAuthor.AuthorDate = ResultSet["authorjoindate"].ToString();
               

            }
            //close connection
            Conn.Close();

            //return the final list of thee author names
            return selectedAuthor;
        }

    }
}
