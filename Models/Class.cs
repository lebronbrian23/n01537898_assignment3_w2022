using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace School.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassCode { get; set; }

        public int TeacherId { get; set; }

        public string ClassName { get; set; }

        public string ClassFinishDate { get; set; }

        public string ClassStartDate { get; set; }
    }
}
