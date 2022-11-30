using AutoMapper;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.Extensions;
using Prayug.Module.Core.Extensions;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Web;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Repositorys.Web
{
    public class UserAuthenticationRepository : BaseRepository, IUserAuthenticationRepository
    {
        private readonly IMapper _mapper;
        public readonly IUser _user;
        public UserAuthenticationRepository(IConfiguration config, IUser user, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _user = user;
        }

        public async Task<int> PortalRegister(PortalRegisterVm entity)
        {
            int userstatus = 0;
            //int customaerDeletestatus = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        fnd_user isExistUser = await _user.IsExistUser(conn, entity.email);
                        if (isExistUser == null)
                        {
                            userstatus = await _user.PortalRegister(conn, tran
                            , new fnd_user(0, entity.first_name, entity.last_name, entity.user_name, entity.user_role, entity.user_type, 
                            entity.email, entity.mobile, entity.city, entity.state, entity.password));
                        }
                        else
                        {
                            tran.Rollback();
                            return 2;
                        }
                        //Rollback if any table not inserted
                        if (userstatus == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else
                        {

                            tran.Commit();
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 1;
        }
        public async Task<PortalLoginResponseVm> PortalLogin(string email, string encrypted_user_password, int isAdmin)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                try
                {
                    string user_roles = RoleEnum.Admin + "," + RoleEnum.RegisteredUser + "," + RoleEnum.DemoUser;
                    fnd_user objuser = await _user.PortalLogin(conn, email, encrypted_user_password, user_roles, isAdmin);
                    PortalLoginResponseVm loginvm = new PortalLoginResponseVm();
                    if (objuser != null && objuser.user_id > 0)
                    {
                        var authClaims = new[]
                       {
                    new Claim("user_id", objuser.user_id.ToGetStringValue(),ClaimValueTypes.Integer64),
                    //new Claim("role", objuser.user_role,ClaimValueTypes.String),
                    new Claim(ClaimTypes.Role, objuser.user_role.ToGetStringValue(),ClaimValueTypes.String),
                    new Claim("uname", objuser.user_name.ToGetStringValue(),ClaimValueTypes.String),
                    new Claim("fname", objuser.first_name.ToGetStringValue(),ClaimValueTypes.String),
                };
                        (string, DateTime) objtoken = authClaims.CreateToken();

                        loginvm.user_name = objuser.user_name;
                        loginvm.user_role = objuser.user_role;
                        loginvm.user_id = objuser.user_id;
                        loginvm.first_name = objuser.first_name;
                        loginvm.last_name = objuser.last_name;
                        loginvm.web_role_access = objuser.web_role_access;
                        loginvm.Token = objtoken.Item1;
                        loginvm.Expiration = objtoken.Item2;
                        return loginvm;
                    }
                    else
                    {
                        loginvm.Token = string.Empty;
                        loginvm.Expiration = DateTime.Now;
                        return loginvm;
                    }

                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        //----- change passwoir
        public async Task<int> ChangePassword(string usre_name, string current_password, string new_password)
        {
            int userstatus = 0;
            //int customaerDeletestatus = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        userstatus = await _user.ChangePassword(conn, tran, usre_name, current_password, new_password, Convert.ToInt64(last_modified_date_time));

                        //Rollback if any table not inserted
                        if (userstatus == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else
                        {

                            tran.Commit();
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 1;
        }

        //---------
        public async Task<int> UpdatePaymentDetail(int user_id, string service, string mode, string is_free, int amount)
        {
            int userstatus = 0;
            //int customaerDeletestatus = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        userstatus = await _user.UpdatePaymentDetail(conn, tran, user_id, service, mode, is_free, amount);

                        //Rollback if any table not inserted
                        if (userstatus == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else
                        {

                            tran.Commit();
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 1;
        }

    }
}
