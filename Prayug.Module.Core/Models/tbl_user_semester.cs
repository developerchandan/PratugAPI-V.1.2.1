﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class tbl_user_semester
    {
        public int user_id { get; set; }
        public int subject_id { get; set; }
        public string subject_name { get; set; }
        public string semester_name { get; set; }
        public int course_id { get; set; }
        public string course_name { get; set; }
        public int is_permission { get; set; }
    }
}
