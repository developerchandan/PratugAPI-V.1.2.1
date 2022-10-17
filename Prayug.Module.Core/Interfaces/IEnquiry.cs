using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface IEnquiry
    {
        Task<int> SaveEnquiry(IDbConnection conn, IDbTransaction tran, string user_name, string email, string mobile, string course, string admission, string message, string enquiry_for);
    }
}
