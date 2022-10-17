using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class OrderVm
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string order_number { get; set; }
        public string payment_id { get; set; }
        public string course_code { get; set; }
    }
}
