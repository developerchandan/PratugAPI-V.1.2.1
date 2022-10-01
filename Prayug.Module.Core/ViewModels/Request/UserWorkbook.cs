using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class UserWorkbook
    {
        public int lession_id { get; set; }
        public string unit_id { get; set; }
        public int user_id { get; set; }
        public UserQuestionVm[] questions  { get; set; }
    }
}
