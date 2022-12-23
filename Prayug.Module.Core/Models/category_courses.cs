using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class category_courses
    {
        public int category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public string course_code { get; set; }
        public string course_name { get; set; }
        public int course_id { get; set; }
        public string image_path { get; set; }
        public int course_count { get; set; }
    }
}
