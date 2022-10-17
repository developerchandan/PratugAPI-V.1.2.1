using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class CourseSkillVm
    {
        public int course_id { get; set; }
        public string category_code { get; set; }
        public string skill_name { get; set; }
        public string path { get; set; }
    }
}
