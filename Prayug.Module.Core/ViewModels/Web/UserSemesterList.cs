using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class UserSemesterList
    {
        public int user_id { get; set; }
        public string semester_name { get; set; }
        public int course_id { get; set; }
        public string course_name { get; set; }
        public IEnumerable<SemesterSubjectList> subject_list { get; set; }
    }
}
