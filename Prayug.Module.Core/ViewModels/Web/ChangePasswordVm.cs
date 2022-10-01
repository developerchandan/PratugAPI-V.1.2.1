using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class ChangePasswordVm
    {
        public string current_password { get; set; }
        public string new_password { get; set; }
        public string user_code { get; set; }
    }
}
