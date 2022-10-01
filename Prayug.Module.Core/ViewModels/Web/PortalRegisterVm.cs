using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class PortalRegisterVm
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string user_role { get; set; }
        public string user_type { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
    }
}
