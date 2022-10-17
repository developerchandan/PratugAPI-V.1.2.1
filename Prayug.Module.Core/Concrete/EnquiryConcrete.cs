using Dapper;
using Prayug.Module.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Concrete
{
    public class EnquiryConcrete : IEnquiry
    {
        public async Task<int> SaveEnquiry(IDbConnection conn, IDbTransaction tran, string user_name, string email, string mobile, string course, string admission, string message, string enquiry_for)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_name", user_name);
                param.Add("p_email", email);
                param.Add("p_mobile", mobile);
                param.Add("p_course", course);
                param.Add("p_admission", admission);
                param.Add("p_message", message);
                param.Add("p_enquiry_for", enquiry_for);
                return await conn.ExecuteAsync("usp_core_create_user_enquiry_detail", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
    }
}
