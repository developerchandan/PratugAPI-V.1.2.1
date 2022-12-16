using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class workbook_question
    {
        public int lession_id { get; set; }
        public int course_id { get; set; }
        public int question_id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int sequence { get; set; }
    }
}
