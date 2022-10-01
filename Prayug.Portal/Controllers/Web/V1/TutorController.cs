using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.Models;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Infrastructure.SmartTable;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.ViewModels.Request;
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
    public class TutorController : ControllerBase
    {
        private ITutorRepository _tutorRepository;
        public TutorController(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        [HttpPost]
        [Route("GetCourseList")]
        public async Task<IActionResult> GetCourseList([FromBody] SmartTableParam<CourseSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<CourseListVm>> response = new SingleListResponse<IEnumerable<CourseListVm>>())
            {
                try
                {
                    (IEnumerable<CourseListVm>, Int64) objResult = await _tutorRepository.GetCourseList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
                    response.Status = ResponseMessageEnum.Success;
                    response.Message = (objResult.Item2 > 0) ? "Success" : "No data fround";

                    response.pageSize = entity.paging.pageSize;
                    response.TotalRecord = objResult.Item2;
                    response.objResponse = objResult.Item1;
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
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseVm entity)
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
                            Status = await _tutorRepository.CreateCourse(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Course Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Course already exist";
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
        [HttpPut("EditCourse")]
        public async Task<IActionResult> EditCourse([FromBody] CourseVm entity)
        {
            using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
            {
                int Status = 0;
                try
                {
                    TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
                    if (entity != null && userdetail != null && entity.course_id > 0)
                    {
                        if (entity != null)
                        {
                            Status = await _tutorRepository.EditCourse(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Course Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Course already exist";
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
        [HttpDelete("GetCourseDelete")]
        public async Task<IActionResult> GetCourseDelete(int course_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _tutorRepository.GetCourseDelete(course_id);
                    response.objResponse = result;
                    response.Status = (result > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "item";
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
        [HttpPost("CreateCourseStructure")]
        public async Task<IActionResult> CreateCourseStructure([FromBody] CourseStructureVm entity)
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
                            Status = await _tutorRepository.CreateCourseStructure(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Course Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Course already exist";
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
    }
    
}
