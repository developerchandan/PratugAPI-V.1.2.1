using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class LessionItemVm
    {
        public int lession_id { get; set; }
        public string unit_id { get; set; }
        public int subject_id { get; set; }
        public string item_name { get; set; }
        public string language_name { get; set; }
        public string item_path { get; set; }
    }
}
