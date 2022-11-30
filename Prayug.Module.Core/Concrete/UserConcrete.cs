using Dapper;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Concrete
{
    public class UserConcrete : IUser
    {
        public async Task<int> PortalRegister(IDbConnection conn, IDbTransaction tran, fnd_user entity)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_first_name", entity.first_name);
                param.Add("p_last_name", entity.last_name);
                param.Add("p_email", entity.email);
                param.Add("p_mobile", entity.mobile);
                param.Add("p_city", entity.city);
                param.Add("p_state", entity.state);
                param.Add("p_password", entity.password);
                return await conn.ExecuteAsync(@"usp_core_user_registration", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> ChangePassword(IDbConnection conn, IDbTransaction tran, string user_code, string current_password, string new_password, long last_modified_date_time)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_name", user_code);
                param.Add("p_current_password", current_password);
                param.Add("p_new_password", new_password);
                param.Add("p_last_modified_data_time", last_modified_date_time);
                return await conn.ExecuteAsync(@"usp_core_portal_change_password", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<fnd_user> PortalLogin(IDbConnection conn, string email, string encrypted_user_password, string user_roles, int isAdmin)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_email", email);
                param.Add("p_user_password", encrypted_user_password);
                param.Add("p_isadmin", isAdmin);
                return await conn.QueryFirstOrDefaultAsync<fnd_user>(@"usp_core_portal_user_login", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<fnd_user> IsExistUser(IDbConnection conn, string email_id)
        {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_email", email_id);
                return await conn.QueryFirstOrDefaultAsync<fnd_user>(@"usp_core_get_user_detail_byemail", param, commandType: CommandType.StoredProcedure);
            
        }
        public async Task<int> UpdatePaymentDetail(IDbConnection conn, IDbTransaction tran, int user_id, string service, string mode, string is_free, int amount)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                param.Add("p_service", service);
                param.Add("p_mode", mode);
                param.Add("p_is_free", is_free);
                param.Add("p_amount", amount);
                return await conn.ExecuteAsync(@"usp_core_portal_change_payment_detail", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

    }
}
