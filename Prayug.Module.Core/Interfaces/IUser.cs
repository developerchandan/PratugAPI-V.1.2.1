using Prayug.Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface IUser
    {
        Task<fnd_user> IsExistUser(IDbConnection conn, string email_id);
        Task<int> PortalRegister(IDbConnection conn, IDbTransaction tran, fnd_user entity);
        Task<fnd_user> PortalLogin(IDbConnection conn, string email, string encrypted_user_password, string user_roles, int isAdmin);
        Task<int> ChangePassword(IDbConnection conn, IDbTransaction tran, string user_code, string current_password, string new_password, Int64 last_modified_date_time);
        Task<int> UpdatePaymentDetail(IDbConnection conn, IDbTransaction tran, int user_id, string service, string mode, string is_free, int amount);
    }
}
