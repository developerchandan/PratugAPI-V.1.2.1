using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.ViewModels.Web
{
    public class QuizDetailVm
    {
        public int lession_id { get; set; }
        public string lession_name { get; set; }
        public string unit_id { get; set; }
        public string unit_name { get; set; }
        public IEnumerable<QuestionVm> questions { get; set; }
    }
}
