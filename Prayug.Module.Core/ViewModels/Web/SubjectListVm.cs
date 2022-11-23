using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class SubjectListVm
    {
        public int id { get; set; }
        public int subject_id { get; set; }
        public int course_id { get; set; }
        public string course_code { get; set; }
        public string group_id { get; set; }
        public string group_name { get; set; }
        public string subject_code { get; set; }
        public string subject_name { get; set; }
    }
}
