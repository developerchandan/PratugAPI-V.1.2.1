using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class CategoryVm
    {
        public int category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public int course_type { get; set; }
        public string duration { get; set; }
    }
}
