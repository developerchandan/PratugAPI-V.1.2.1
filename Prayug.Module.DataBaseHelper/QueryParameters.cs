using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Module.DataBaseHelper
{
    public class QueryParameters
    {
        public string org_id { get; set; }
        public string salesman_code { get; set; }
        public string sort_expression { get; set; }
        public string sort_direction { get; set; }
        public int page_no { get; set; } = 1;
        public int page_size { get; set; } = 20;
        public int offsetCount
        {
            get
            {
                return ((page_no - 1) * page_size);
            }
        }
        public string search_query { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public string start_date_II { get; set; }
        public string end_date_II { get; set; }

    }
}
