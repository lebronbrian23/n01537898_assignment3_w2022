using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class ClassController : Controller
    {
        //GET: Class
        public ActionResult Index()
        {
           
            return View();
        }

        // Get: Class/List
        // showing a page of all Classs
        [Route("/Class/List/{searchkey}")]
        public ActionResult List(string searchkey)
        {
            //connect to data access layer
            ClassDataController controller = new ClassDataController();
            //get the list of Classs
            IEnumerable<Class> Classs = controller.ListClasses(searchkey);
            
            // pass the list of Classs to Classs/List.cshtml
            return View(Classs);

        }

        //GET: /Class/Show/{id}
        [Route("/Class/Show/{id}")]
        public ActionResult Show(int id)
        {
            //connect to data access layer
            ClassDataController controller = new ClassDataController();
            //get the list of Classs
            Class selectedClass = controller.findClass(id);
            // pass the list of Classs to Classs/Show.cshtml
            return View(selectedClass);

        }

    }
}

