using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class UserVm
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string user_role { get; set; }
        public string user_type { get; set; }
        public string web_role_access { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string user_image { get; set; }
    }
}
