using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class mcq_answer
    {
        public int lession_id { get; set; }
        public int question_id { get; set; }
        public int answer_id { get; set; }
        public int is_answer { get; set; }
        public string answer { get; set; }
        public int sequence { get; set; }
    }
}
