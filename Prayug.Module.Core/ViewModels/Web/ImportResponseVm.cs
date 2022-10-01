using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class ImportResponseVm
    {
        public int total_count { get; set; }
        public int success_count { get; set; }
        public string file_path { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
    }
}
