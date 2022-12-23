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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LearningController : ControllerBase
    {
        private ILearningRepository _learnRepository;
        public LearningController(ILearningRepository learnRepository)
        {
            _learnRepository = learnRepository;
        }
        
        [HttpGet("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse()
        {
            using (ISingleModelResponse<IEnumerable<CourseVm>> response = new SingleModelResponse<IEnumerable<CourseVm>>())
            {
                try
                {
                    IEnumerable<CourseVm> objResult = await _learnRepository.GetAllCourse();
                    response.objResponse = objResult;
                    response.Status = (objResult != null && objResult.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Course List";
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
        [HttpGet("GetAllGroup")]
        public async Task<IActionResult> GetAllGroup(int course_id)
        {
            using (ISingleModelResponse<IEnumerable<GroupVm>> response = new SingleModelResponse<IEnumerable<GroupVm>>())
            {
                try
                {
                    IEnumerable<GroupVm> objGroup = await _learnRepository.GetAllGroup(course_id);
                    response.objResponse = objGroup;
                    response.Status = (objGroup != null && objGroup.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Group List";
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
        [HttpGet("GetAllSubject")]
        public async Task<IActionResult> GetAllSubject(int course_id)
        {
            using (ISingleModelResponse<IEnumerable<SubjectVm>> response = new SingleModelResponse<IEnumerable<SubjectVm>>())
            {
                try
                {
                    IEnumerable<SubjectVm> objSubject = await _learnRepository.GetAllSubject(course_id);
                    response.objResponse = objSubject;
                    response.Status = (objSubject != null && objSubject.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Group List";
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
        [Route("SaveUserSemester")]
        public async Task<IActionResult> SaveUserSemester(UserSemesterRequestVm entity)
        {
            //var tt = ResponseMessageEnum.Exception.GetDescription();
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _learnRepository.SaveUserSemester(entity);


                    if (result  > 0)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = "Semester detail save";
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
        [HttpGet("getUserSemester")]
        public async Task<IActionResult> getUserSemester(int user_id)
        {
            using (ISingleModelResponse<IEnumerable<UserSemesterList>> response = new SingleModelResponse<IEnumerable<UserSemesterList>>())
            {
                try
                {
                    IEnumerable<UserSemesterList> objSubject = await _learnRepository.getUserSemester(user_id);
                    response.objResponse = objSubject;
                    response.Status = (objSubject != null && objSubject.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Group List";
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
        [HttpGet("getSubjectOverview")]
        public async Task<IActionResult> getSubjectOverview(int subject_id)
        {
            using (ISingleModelResponse<IEnumerable<SubjectOverviewVm>> response = new SingleModelResponse<IEnumerable<SubjectOverviewVm>>())
            {
                try
                {
                    IEnumerable<SubjectOverviewVm> objView = await _learnRepository.getSubjectOverview(subject_id);
                    response.objResponse = objView;
                    response.Status = (objView != null && objView.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Overview List";
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
        [HttpGet("getSubjectCurriculum")]
        public async Task<IActionResult> getSubjectCurriculum(int subject_id)
        {
            using (ISingleModelResponse<IEnumerable<SubjectCurriculumVm>> response = new SingleModelResponse<IEnumerable<SubjectCurriculumVm>>())
            {
                try
                {
                    IEnumerable<SubjectCurriculumVm> objView = await _learnRepository.getSubjectCurriculum(subject_id);
                    response.objResponse = objView;
                    response.Status = (objView != null && objView.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Curriculum List";
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

        [HttpGet("getSubjectUnit")]
        public async Task<IActionResult> getSunjectUnit(string unit_name, int subject_id)
        {
            using (ISingleModelResponse<IEnumerable<UnitLessionVm>> response = new SingleModelResponse<IEnumerable<UnitLessionVm>>())
            {
                try
                {
                    IEnumerable<UnitLessionVm> objView = await _learnRepository.getSunjectUnit(unit_name, subject_id);
                    response.objResponse = objView;
                    response.Status = (objView != null && objView.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Curriculum List";
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

        [HttpGet("getLessionItems")]
        public async Task<IActionResult> getLessionItems(int lession_id)
        {
            using (ISingleModelResponse<IEnumerable<LessionItemsVm>> response = new SingleModelResponse<IEnumerable<LessionItemsVm>>())
            {
                try
                {
                    IEnumerable<LessionItemsVm> objView = await _learnRepository.getLessionItems(lession_id);
                    response.objResponse = objView;
                    response.Status = (objView != null && objView.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Lession Item List";
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

        [HttpGet("GetCourseDetail")]
        public async Task<IActionResult> GetCourseDetail(int course_id)
        {
            using (ISingleModelResponse<CourseVm> response = new SingleModelResponse<CourseVm>())
            {
                try
                {
                    CourseVm objView = await _learnRepository.GetCourseDetail(course_id);
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

        [HttpGet("GetQuizByLessionId")]
        public async Task<IActionResult> GetQuizByLessionId(int lession_id)
        {
            using (ISingleModelResponse<QuizDetailVm> response = new SingleModelResponse<QuizDetailVm>())
            {
                try
                {
                    QuizDetailVm objView = await _learnRepository.GetQuizByLessionId(lession_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Quiz";
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

        [HttpGet("GetTopicDetail")]
        public async Task<IActionResult> GetTopicDetail(int item_id)
        {
            using (ISingleModelResponse<LessionItemsVm> response = new SingleModelResponse<LessionItemsVm>())
            {
                try
                {
                    LessionItemsVm objView = await _learnRepository.GetTopicDetail(item_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
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

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(int user_id)
        {
            using (ISingleModelResponse<UserVm> response = new SingleModelResponse<UserVm>())
            {
                try
                {
                    UserVm objView = await _learnRepository.GetUserDetail(user_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
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
        [AllowAnonymous]
        [HttpGet("GetAllCourseByCategory")]
        public async Task<IActionResult> GetAllCourseByCategory(string category_code)
        {
            using (ISingleModelResponse<IEnumerable<CourseVm>> response = new SingleModelResponse<IEnumerable<CourseVm>>())
            {
                try
                {
                    IEnumerable<CourseVm> objResult = await _learnRepository.GetAllCourseByCategory(category_code);
                    response.objResponse = objResult;
                    response.Status = (objResult != null && objResult.Count() > 0) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Course List";
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
        [HttpGet("GetWorkbookByLession")]
        public async Task<IActionResult> GetWorkbookByLession(int lession_id)
        {
            using (ISingleModelResponse<WorkbookDetailVm> response = new SingleModelResponse<WorkbookDetailVm>())
            {
                try
                {
                    WorkbookDetailVm objView = await _learnRepository.GetWorkbookByLession(lession_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
                    response.Message = "Workbook";
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

        [HttpPost("SaveUserWorkbook")]
        public async Task<IActionResult> SaveUserWorkbook(UserWorkbook entity)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _learnRepository.SaveUserWorkbook(entity);


                    if (result > 0)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = "workbook detail save";
                        response.objResponse = 1;
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
        [HttpPost("CreateUserProfile")]
        public async Task<IActionResult> CreateUserProfile(UserProfileVm entity)
        {
            using (ISingleModelResponse<int> response = new SingleModelResponse<int>())
            {
                try
                {
                    int result = await _learnRepository.CreateUserProfile(entity);


                    if (result > 0)
                    {
                        response.Status = ResponseMessageEnum.Success;
                        response.Message = "profile detail save";
                        response.objResponse = 1;
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
        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(int user_id)
        {
            using (ISingleModelResponse<UserProfileVm> response = new SingleModelResponse<UserProfileVm>())
            {
                try
                {
                    UserProfileVm objView = await _learnRepository.GetUserProfile(user_id);
                    response.objResponse = objView;
                    response.Status = (objView != null) ? ResponseMessageEnum.Success : ResponseMessageEnum.Failure;
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
        
    }
}
