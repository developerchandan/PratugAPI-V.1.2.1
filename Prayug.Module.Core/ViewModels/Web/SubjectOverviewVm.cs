using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class SubjectOverviewVm
    {
        public int id { get; set; }
        public string header_name { get; set; }
        public string target_tab { get; set; }
        public string body_column1 { get; set; }
        public string body_column2 { get; set; }
        public string body_column3 { get; set; }
        public string body_column4 { get; set; }
        public string body_column5 { get; set; }
        public int course_id { get; set; }
        public int subject_id { get; set; }
        public string subject_name { get; set; }
    }
}
