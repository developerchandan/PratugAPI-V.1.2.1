using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class UserProfileVm
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string headline { get; set; }
        public string mobile { get; set; }
        public string collage { get; set; }
        public string university { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string youtube { get; set; }
    }
}
