using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class import_mcq
    {
        public int lession_id { get; set; }
        public string question { get; set; }
        public string option { get; set; }
        public int is_answer { get; set; }
        public int sequence { get; set; }
        public int index_no { get; set; }
        public string status { get; set; }
        public string status_message { get; set; }
    }
}
