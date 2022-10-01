using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Infrastructure.SmartTable
{
    public class Pagination
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid {0}")]
        public int pageNo { get; set; }
        [Required(ErrorMessage = "The {0} field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid {0}")]
        public int pageSize { get; set; }
        public string sortName { get; set; }
        public string sortType { get; set; }
    }
}
