using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface IUserAuthenticationRepository
    {
        Task<int> PortalRegister(PortalRegisterVm register);
        Task<PortalLoginResponseVm> PortalLogin(string email, string encrypted_user_password, int isAdmin);
        Task<int> ChangePassword(string usre_name, string current_password, string new_password);
        Task<int> UpdatePaymentDetail(int user_id, string service, string mode, string is_free, int amount);
    }
}
