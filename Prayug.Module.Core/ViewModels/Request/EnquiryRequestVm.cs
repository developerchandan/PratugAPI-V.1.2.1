using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class EnquiryRequestVm
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string course { get; set; }
        public string admission { get; set; }
        public string message { get; set; }
        public string enquiry_for { get; set; }
    }
}
