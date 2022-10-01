using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class ImportCourseList
    {
        public string course_code { get; set; }
        public string course_name { get; set; }
        public string image_path { get; set; }
        public string is_activce { get; set; }
        public int index_no { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }
    }
}
