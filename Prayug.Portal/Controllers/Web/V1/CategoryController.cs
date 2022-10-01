using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.Models;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prayug.Portal.Controllers.Web.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _category;
        public CategoryController(ICategoryRepository category)
        {
            _category = category;
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryVm entity)
        {
            using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
            {
                int Status = 0;
                try
                {
                    TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
                    if (entity != null && userdetail != null)
                    {
                        if (entity != null)
                        {
                            Status = await _category.CreateCategory(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Category Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Category already exist";
                            }
                            else
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Failure";
                            }
                        }
                        else
                        {
                            response.Status = ResponseMessageEnum.Failure;
                            response.Message = "Empty Request ";
                        }

                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "You are not authorized to perform this operation";
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
        [HttpGet("GetCategoryList")]
        public async Task<IActionResult> GetCategoryList()
        {
            using (ISingleModelResponse<IEnumerable<CategoryVm>> response = new SingleModelResponse<IEnumerable<CategoryVm>>())
            {
                try
                {
                    IEnumerable<CategoryVm> obj = await _category.GetCategoryList();
                    response.objResponse = obj;
                    response.Status = (obj != null && obj.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Category List";
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = "Exception";
                    response.Message = ex.Message;
                    return Ok(response);
                }
            }
        }
    }
}
