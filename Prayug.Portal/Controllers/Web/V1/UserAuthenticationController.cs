using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.ViewModels.Request;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prayug.Portal.Controllers.Web.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserAuthenticationController : ControllerBase
    {
        IUserAuthenticationRepository _userAuthenticationRepository;

        public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository)
        {
            _userAuthenticationRepository = userAuthenticationRepository;
        }
        [HttpPost]
        [Route("portalregister")]
        public async Task<IActionResult> PortalRegister(PortalRegisterVm entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _userAuthenticationRepository.PortalRegister(entity);


                    if (result == 1)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = ResponseMessageEnum.Success.GetDescription();
                        return Ok(response);
                    }
                    else if (result == 2)
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "User already register";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.UnAuthorized;
                        response.Message = "Invalid login credentials";
                        return Unauthorized(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return StatusCode(500, response);
                }
            }
        }

        [HttpPost("portallogin")]
        public async Task<IActionResult> PortalLogin(PortalLoginVm entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleModelResponse<PortalLoginResponseVm> response = new SingleModelResponse<PortalLoginResponseVm>())
            {
                try
                {
                    PortalLoginResponseVm portalLoginResponseVm = await _userAuthenticationRepository.PortalLogin(entity.Email, entity.Password, entity.isAdmin);


                    if (portalLoginResponseVm != null && !string.IsNullOrEmpty(portalLoginResponseVm.Token))
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = ResponseMessageEnum.Success.GetDescription();
                        response.objResponse = portalLoginResponseVm;
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.UnAuthorized;
                        response.Message = "Invalid login credentials";
                        return Unauthorized(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return StatusCode(500, response);
                }



            }
        }
        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm entity)
        {
            int trnStatus = 0;
            using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
            {
                try
                {
                    trnStatus = await _userAuthenticationRepository.ChangePassword(entity.user_code, entity.current_password, entity.new_password);
                    if (trnStatus == 1)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = ResponseMessageEnum.Success.GetDescription();
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "Password not change Successfully";
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return StatusCode(500, response);
                }



            }
        }
        [HttpPut]
        [Route("UpdatePaymentDetail")]
        public async Task<IActionResult> UpdatePaymentDetail(UpdateUserPayment entity)
        {
            int trnStatus = 0;
            using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
            {
                try
                {
                    trnStatus = await _userAuthenticationRepository.UpdatePaymentDetail(entity.user_id, entity.service, entity.mode, entity.is_free, entity.amount);
                    if (trnStatus == 1)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = ResponseMessageEnum.Success.GetDescription();
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "User Payment Updated Successfully";
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return StatusCode(500, response);
                }



            }
        }
    }
}
