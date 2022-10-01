using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class WorkbookQuestionVm
    {
        public int question_id { get; set; }
        public int lession_id { get; set; }
        public string question { get; set; }
        public int sequence { get; set; }
        public string answer { get; set; }
    }
}
