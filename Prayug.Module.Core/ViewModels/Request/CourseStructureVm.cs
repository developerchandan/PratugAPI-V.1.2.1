using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class CourseStructureVm
    {
        public int course_id { get; set; }
        public string category_code { get; set; }
        public string item_name { get; set; }
        public int is_active { get; set; }
        public string path { get; set; }
    }
}
