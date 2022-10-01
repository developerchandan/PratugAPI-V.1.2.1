using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Infrastructure.SmartTable
{
    public class SmartTableParam<T> where T : class
    {
        public T Search { get; set; }
        public Pagination paging { get; set; }
    }
}
