using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class CourseListVm
    {
        public int course_id { get; set; }
        public string course_code { get; set; }
        public string course_name { get; set; }
        public string image_path { get; set; }
        public int is_active { get; set; }
    }
}
