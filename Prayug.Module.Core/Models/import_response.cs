using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Models
{
    public class import_response
    {
        public int total_count { get; set; }
        public int success_count { get; set; }
        public string file_path { get; set; }
    }
}
