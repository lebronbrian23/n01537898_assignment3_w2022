using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class AuthorController : Controller
    {
        //GET: Author
        public ActionResult Index()
        {
           
            return View();
        }

        // Get: Author/List
        // showing a page of all authors
        [Route("/Author/List/{searchkey}")]
        public ActionResult List(string searchkey)
        {
            //connect to data access layer
            //get the authors
            // pass the list of authors to Author/List.cshtml

            AuthorDataController controller = new AuthorDataController();
            IEnumerable<Author> Authors = controller.ListAuthors(searchkey);
            return View(Authors);

        }

    }
}
