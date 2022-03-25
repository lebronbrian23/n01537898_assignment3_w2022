using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace School.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherEmployeeNumber { get; set; }

        public string TeacherFName { get; set; }

        public string TeacherLName { get; set; }

        public string TeacherSalary { get; set; }

        public string TeacherHireDate { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
    }
}
