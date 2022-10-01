using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Request
{
    public class UserQuestionVm
    {
        public int question_id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
