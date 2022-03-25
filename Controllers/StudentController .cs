using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class StudentController : Controller
    {
        //GET: Student
        public ActionResult Index()
        {
           
            return View();
        }

        // Get: Student/List
        // showing a page of all students
        [Route("/Student/List/{searchkey}")]
        public ActionResult List(string searchkey)
        {
            //connect to data access layer
            StudentDataController controller = new StudentDataController();
            //get the list of students
            IEnumerable<Student> Students = controller.ListStudents(searchkey);
            
            // pass the list of students to Students/List.cshtml
            return View(Students);

        }

        //GET: /Student/Show/{id}
        [Route("/Student/Show/{id}")]
        public ActionResult Show(int id)
        {
            //connect to data access layer
            StudentDataController controller = new StudentDataController();
            //get the list of students
            Student selectedStudent = controller.findStudent(id);
            // pass the list of students to Students/Show.cshtml
            return View(selectedStudent);

        }

    }
}

