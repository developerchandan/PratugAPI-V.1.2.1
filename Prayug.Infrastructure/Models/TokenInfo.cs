using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Infrastructure.Models
{
    public class TokenInfo : BaseClass
    {
        public long id { get; set; }
        public string role { get; set; }
        public string uname { get; set; }
        public string udes { get; set; }
        public int unit { get; set; }
        public string org { get; set; }
        public string mng { get; set; }
        public int atype { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsValidToken
        {
            get
            {
                if (string.IsNullOrEmpty(uname) || id <= 0 || string.IsNullOrWhiteSpace(uname))
                {
                    ErrorMessage = "Unauthorized";
                    return false;
                }
                else
                {
                    ErrorMessage = "Valid";
                    return true;


                }
            }
        }
    }
}
