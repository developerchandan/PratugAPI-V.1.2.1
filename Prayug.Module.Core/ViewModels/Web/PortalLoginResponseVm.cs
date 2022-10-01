using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class PortalLoginResponseVm
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string user_role { get; set; }
        public string web_role_access { get; set; }
        public string Token { get; set; }
        public int user_id { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime ServerDate
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
