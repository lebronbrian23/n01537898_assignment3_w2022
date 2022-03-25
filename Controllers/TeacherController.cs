using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class TeacherController : Controller
    {
        //GET: Teacher
        public ActionResult Index()
        {
           
            return View();
        }

        // Get: Teacher/List
        // showing a page of all teachers
        [Route("/Teacher/List/{searchkey}")]
        public ActionResult List(string searchkey)
        {
            //connect to data access layer
            //get the list of teachers
            // pass the list of teachers to Teacher/List.cshtml

            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(searchkey);
            return View(Teachers);

        }

        //GET: /Teacher/Show/{id}
        [Route("/Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher= controller.findTeacher(id);
            return View(selectedTeacher);

        }

    }
}

