using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.Models;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Infrastructure.SmartTable;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Repositorys.Web;
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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subject;
        public SubjectController(ISubjectRepository subject)
        {
            _subject = subject;
        }
        [HttpPost]
        [Route("GetSubjectList")]
        public async Task<IActionResult> GetSubjectList([FromBody] SmartTableParam<SubjectSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<SubjectListVm>> response = new SingleListResponse<IEnumerable<SubjectListVm>>())
            {
                try
                {
                    (IEnumerable<SubjectListVm>, Int64) objResult = await _subject.GetSubjectList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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
        [HttpPost("CreateSubject")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectVm entity)
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
                            Status = await _subject.CreateSubject(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Subject already exist";
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
        //[HttpPut("EditSubject")]
        //public async Task<IActionResult> EditSubject([FromBody] SubjectVm entity)
        //{
        //    using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
        //    {
        //        int Status = 0;
        //        try
        //        {
        //            TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
        //            if (entity != null && userdetail != null && entity.course_id > 0)
        //            {
        //                if (entity != null)
        //                {
        //                    Status = await _subject.EditSubject(entity, userdetail);
        //                    if (Status == 1)
        //                    {
        //                        response.Status = ResponseMessageEnum.Success;
        //                        response.Message = "Subject Successfully Created";
        //                    }
        //                    else if (Status == 2)
        //                    {
        //                        response.Status = ResponseMessageEnum.Failure;
        //                        response.Message = "Subject already exist";
        //                    }
        //                    else
        //                    {
        //                        response.Status = ResponseMessageEnum.Failure;
        //                        response.Message = "Failure";
        //                    }
        //                }
        //                else
        //                {
        //                    response.Status = ResponseMessageEnum.Failure;
        //                    response.Message = "Empty Request ";
        //                }

        //                return Ok(response);
        //            }
        //            else
        //            {
        //                response.Status = ResponseMessageEnum.Failure;
        //                response.Message = "You are not authorized to perform this operation";
        //                return Unauthorized(response);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Status = ResponseMessageEnum.Exception;
        //            response.Message = ex.Message;
        //            return StatusCode(500, response);
        //        }
        //    }
        //}
        [HttpPut("UpdateSubject")]
        public async Task<IActionResult> UpdateSubject([FromBody] SubjectVm entity)
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
                            Status = await _subject.UpdateSubject(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Successfully Updated";
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
        [HttpGet("GetSubjectDetail")]
        public async Task<IActionResult> GetSubjectDetail(int subject_id, int course_id)
        {
            using (ISingleModelResponse<SubjectVm> response = new SingleModelResponse<SubjectVm>())
            {
                try
                {
                    SubjectVm objView = await _subject.GetSubjectDetail(subject_id, course_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Course";
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
        [HttpDelete("DeleteOneSubject")]
        public async Task<IActionResult> DeleteOneSubject(int user_id, int subject_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _subject.DeleteOneSubject(user_id, subject_id);
                    response.objResponse = result;
                    response.Status = (result > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Deleted";
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
        [HttpPost("CreateOneSubject")]
        public async Task<IActionResult> CreateOneSubject([FromBody] UserSemesterVm entity)
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
                            Status = await _subject.CreateOneSubject(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Created Successfully";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Subject already exist";
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
        [HttpPost("CreateCommonSubject")]
        public async Task<IActionResult> CreateCommonSubject([FromBody] CommonSubjectVm entity)
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
                            Status = await _subject.CreateCommonSubject(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Subject already exist";
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
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            using (ISingleModelResponse<IEnumerable<CommonSubjectVm>> response = new SingleModelResponse<IEnumerable<CommonSubjectVm>>())
            {
                try
                {
                    IEnumerable<CommonSubjectVm> objResult = await _subject.GetAllSubjects();
                    response.objResponse = objResult;
                    response.Status = (objResult != null && objResult.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "subject List";
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
        [HttpPost]
        [Route("GetCommonSubjectList")]
        public async Task<IActionResult> GetCommonSubjectList([FromBody] SmartTableParam<SubjectSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<CommonSubjectVm>> response = new SingleListResponse<IEnumerable<CommonSubjectVm>>())
            {
                try
                {
                    (IEnumerable<CommonSubjectVm>, Int64) objResult = await _subject.GetCommonSubjectList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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
        [HttpGet("GetCommonSubjectDetail")]
        public async Task<IActionResult> GetCommonSubjectDetail(int subject_id)
        {
            using (ISingleModelResponse<CommonSubjectVm> response = new SingleModelResponse<CommonSubjectVm>())
            {
                try
                {
                    CommonSubjectVm objView = await _subject.GetCommonSubjectDetail(subject_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Course";
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
        [HttpPut("EditCommonSubject")]
        public async Task<IActionResult> EditCommonSubject([FromBody] CommonSubjectVm entity)
        {
            using (ISingleStatusResponse<int> response = new SingleStatusResponse<int>())
            {
                int Status = 0;
                try
                {
                    TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
                    if (entity != null && userdetail != null && entity.subject_id > 0)
                    {
                        if (entity != null)
                        {
                            Status = await _subject.EditCommonSubject(entity, userdetail);
                            if (Status == 1)
                            {
                                response.Status = ResponseMessageEnum.Success;
                                response.Message = "Subject Successfully Created";
                            }
                            else if (Status == 2)
                            {
                                response.Status = ResponseMessageEnum.Failure;
                                response.Message = "Subject already exist";
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
        [HttpDelete("GetDeleteSubject")]
        public async Task<IActionResult> GetDeleteSubject(int subject_id, int course_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _subject.GetDeleteSubject(subject_id, course_id);
                    response.objResponse = result;
                    response.Status = (result > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Deleted";
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
        [HttpDelete("GetDeleteCommonSubject")]
        public async Task<IActionResult> GetDeleteCommonSubject(int subject_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _subject.GetDeleteCommonSubject(subject_id);
                    response.objResponse = result;
                    response.Status = (result > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Deleted";
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
        [AllowAnonymous]
        [HttpGet("GetAllCertificationSubject")]
        public async Task<IActionResult> GetAllCertificationSubject()
        {
            using (ISingleModelResponse<IEnumerable<SubjectVm>> response = new SingleModelResponse<IEnumerable<SubjectVm>>())
            {
                try
                {
                    IEnumerable<SubjectVm> objResult = await _subject.GetAllCertificationSubject();
                    response.objResponse = objResult;
                    response.Status = (objResult != null && objResult.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "subject List";
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
