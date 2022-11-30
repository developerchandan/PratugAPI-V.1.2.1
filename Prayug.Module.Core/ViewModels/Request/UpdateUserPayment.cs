using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class UpdateUserPayment
    {
        public int user_id { get; set; }
        public string service { get; set; }
        public string mode { get; set; }
        public string is_free { get; set; }
        public int amount { get; set; }
    }
}
