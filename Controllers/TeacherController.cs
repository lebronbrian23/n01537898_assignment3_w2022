using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //GET /Teacher/new
        [Route("/Teacher/New")]
        public ActionResult New()
        {

            return View();
        }

        //POST: /Teacher/Create
        [Route("/Teacher/Create")]
        public ActionResult Create(string teacherfname,string teacherlname , string employeenumber ,
            string salary, string hiredate)
        {
            if (teacherfname == "" || teacherlname == "" || employeenumber == "" || hiredate == "" || salary == "")
            {
                return RedirectToAction("New");
            }
            

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = teacherfname;
            NewTeacher.TeacherLName = teacherlname;
            NewTeacher.TeacherEmployeeNumber = employeenumber;
            NewTeacher.TeacherHireDate = hiredate;
            NewTeacher.TeacherSalary = salary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            // redirect back to the list page
            return RedirectToAction("List");
        }


        //GET /Teacher/delete/{id}
        [Route("/Teacher/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.findTeacher(id);
            return View(selectedTeacher);
        }

        /// <summary>
        /// This function deletes a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //POST: /Teacher/Delete/{id}
        //[Route("/Teacher/Delete/{id}")]
        [HttpPost]
        public ActionResult Delete(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }

        //GET /Teacher/edit/{id}
        [Route("/Teacher/Edit/{id}")]
        public ActionResult Edit(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.findTeacher(id);
            return View(selectedTeacher);
        }

        /// <summary>
        /// This function update a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherfname"></param>
        /// <param name="teacherlname"></param>
        /// <param name="employeenumber"></param>
        /// <param name="salary"></param>
        /// <param name="hiredate"></param>
        /// <returns></returns>
        //POST: /Teacher/Update/{id}
        //[Route("/Teacher/Update/{id}")]
        [HttpPost]
        public ActionResult Update(int id ,string teacherfname, string teacherlname, string employeenumber,
            string salary, string hiredate)
        {
            if (teacherfname == "" || teacherlname == "" || employeenumber == "" || hiredate == ""  || salary == "")
            {
                return RedirectToAction("/Edit/" + id);
            }

            Teacher UpdateTeacher = new Teacher();
            UpdateTeacher.TeacherFName = teacherfname;
            UpdateTeacher.TeacherLName = teacherlname;
            UpdateTeacher.TeacherEmployeeNumber = employeenumber;
            UpdateTeacher.TeacherHireDate = hiredate;
            UpdateTeacher.TeacherSalary = salary;


            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id , UpdateTeacher);

            // redirect back to the Teacher after an update
            return RedirectToAction("/Show/"+id);

        }


    }
}

