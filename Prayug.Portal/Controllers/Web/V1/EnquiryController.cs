using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Module.Core.ViewModels.Web;
using System.Threading.Tasks;
using System;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.ViewModels.Request;

namespace Prayug.Portal.Controllers.Web.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryRepository _enquiryRepository;
        public EnquiryController(IEnquiryRepository enquiryRepository)
        {
            _enquiryRepository = enquiryRepository;
        }
        [HttpPost("SaveEnquiry")]
        public async Task<IActionResult> SaveEnquiry(EnquiryRequestVm entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _enquiryRepository.SaveEnquiry(entity);


                    if (result > 0)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = "Enquiry detail save";
                        response.objResponse = 0;
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "not save";
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
