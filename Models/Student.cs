using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace School.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }

        public string StudentFName { get; set; }

        public string StudentLName { get; set; }
     
        public string StudentEnrolDate { get; set; }
    }
}
