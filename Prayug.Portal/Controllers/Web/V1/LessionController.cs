using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.Models;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Infrastructure.SmartTable;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
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
    public class LessionController : ControllerBase
    {
        private readonly ILessionRepository _lession;
        public LessionController(ILessionRepository lession)
        {
            _lession = lession;
        }
        [HttpPost("CreateLession")]
        public async Task<IActionResult> CreateLession([FromBody] LessionVm entity)
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
                            Status = await _lession.CreateLession(entity, userdetail);
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
        [HttpGet("GetLessionBySubject")]
        public async Task<IActionResult> GetLessionBySubject(int subject_id, string unit_id)
        {
            using (ISingleModelResponse<IEnumerable<LessionVm>> response = new SingleModelResponse<IEnumerable<LessionVm>>())
            {
                try
                {
                    IEnumerable<LessionVm> objLession = await _lession.getLessionBySubject(subject_id, unit_id);
                    response.objResponse = objLession;
                    response.Status = (objLession != null && objLession.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Lession List";
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
        [Route("GetLessionItemList")]
        public async Task<IActionResult> GetLessionItemList([FromBody] SmartTableParam<ItemSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<LessionItemListVm>> response = new SingleListResponse<IEnumerable<LessionItemListVm>>())
            {
                try
                {
                    (IEnumerable<LessionItemListVm>, Int64) objResult = await _lession.GetLessionItemList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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
        [HttpPost("CreateLessionItem")]
        public async Task<IActionResult> CreateLessionItem([FromBody] LessionItemVm entity)
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
                            Status = await _lession.CreateLessionItem(entity, userdetail);
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
        [HttpGet("GetItemDetail")]
        public async Task<IActionResult> GetItemDetail(int item_id)
        {
            using (ISingleModelResponse<LessionItemListVm> response = new SingleModelResponse<LessionItemListVm>())
            {
                try
                {
                    LessionItemListVm objView = await _lession.GetItemDetail(item_id);
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
        [HttpPost("SaveQuestions")]
        public async Task<IActionResult> SaveQuestions([FromBody] McqQuestionVm[] entity, int lession_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    if (entity != null)
                    {
                        TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
                        try
                        {
                            int objResult = await _lession.SaveQuestions(entity, lession_id, userdetail);
                            response.Status = ResponseMessageEnum.Success;
                            response.Message = (objResult != 0) ? "Success" : "No data fround";
                            response.objResponse = 1;
                        }
                        catch (Exception ex)
                        {
                            response.Status = ResponseMessageEnum.Exception;
                            response.Message = ex.Message;
                            response.Message = ex.Message;
                            return Ok(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    response.Message = ex.Message;
                }
                return Ok(response);
            }
        }
        [HttpPost("SaveWorkbookQuestions")]
        public async Task<IActionResult> SaveWorkbookQuestions([FromBody] WorkbookQuestionVm[] entity, int lession_id, int course_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    if (entity != null)
                    {
                        TokenInfo userdetail = TokenDetail.GetTokenInfo(HttpContext.User);
                        try
                        {
                            int objResult = await _lession.SaveWorkbookQuestions(entity, lession_id, course_id, userdetail);
                            response.Status = ResponseMessageEnum.Success;
                            response.Message = (objResult != 0) ? "Success" : "No data fround";
                            response.objResponse = 1;
                        }
                        catch (Exception ex)
                        {
                            response.Status = ResponseMessageEnum.Exception;
                            response.Message = ex.Message;
                            response.Message = ex.Message;
                            return Ok(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    response.Message = ex.Message;
                }
                return Ok(response);
            }
        }
        [HttpDelete("GetItemDelete")]
        public async Task<IActionResult> GetItemDelete(int item_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _lession.GetItemDelete(item_id);
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
        [HttpPost]
        [Route("GetLessionMcqList")]
        public async Task<IActionResult> GetLessionMcqList([FromBody] SmartTableParam<LessionSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<McqQuestionListVm>> response = new SingleListResponse<IEnumerable<McqQuestionListVm>>())
            {
                try
                {
                    (IEnumerable<McqQuestionListVm>, Int64) objResult = await _lession.GetLessionMcqList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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
        [HttpPost]
        [Route("GetLessionList")]
        public async Task<IActionResult> GetLessionList([FromBody] SmartTableParam<LessionSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<LessionListVm>> response = new SingleListResponse<IEnumerable<LessionListVm>>())
            {
                try
                {
                    (IEnumerable<LessionListVm>, Int64) objResult = await _lession.GetLessionList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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
        [HttpDelete("DeleteLession")]
        public async Task<IActionResult> DeleteLession(int lession_id)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _lession.DeleteLession(lession_id);
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
        [HttpPost]
        [Route("GetWorkbookList")]
        public async Task<IActionResult> GetWorkbookList([FromBody] SmartTableParam<LessionSearchRequestVm> entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleListResponse<IEnumerable<LessionListVm>> response = new SingleListResponse<IEnumerable<LessionListVm>>())
            {
                try
                {
                    (IEnumerable<LessionListVm>, Int64) objResult = await _lession.GetWorkbookList(entity.paging.pageNo, entity.paging.pageSize, entity.paging.sortName, entity.paging.sortType, entity.Search);
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

    }
}
