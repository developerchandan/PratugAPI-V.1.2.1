using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class tbl_answer_option
    {
        public int id { get; set; }
        public string option { get; set; }
        public bool is_answer { get; set; }
        public int question_id { get; set; }
        public int lession_id { get; set; }
    }
}
