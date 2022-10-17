using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class all_user_list
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string city { get; set; }
        public string user_role { get; set; }
        public int is_permission { get; set; }
    }
}
