using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Extensions;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Request;
using Prayug.Module.Core.ViewModels.Web;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Repositorys.Web
{
    public class LearningRepository : BaseRepository, ILearningRepository
    {
        private readonly IMapper _mapper;
        private readonly ILearning _learning;
        public LearningRepository(IConfiguration config, ILearning learning, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _learning = learning;
        }
        public async Task<IEnumerable<CourseVm>> GetAllCourse()
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_course_vm> objCourse = await _learning.GetAllCourse(conn);
                    return _mapper.Map<IEnumerable<CourseVm>>(objCourse);
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

        public async Task<IEnumerable<GroupVm>> GetAllGroup(int id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_group_vm> objGroup = await _learning.GetAllGroup(conn, id);
                    return _mapper.Map<IEnumerable<GroupVm>>(objGroup);
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
        public async Task<IEnumerable<SubjectVm>> GetAllSubject(int id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_subject_vm> objGroup = await _learning.GetAllSubject(conn, id);
                    return _mapper.Map<IEnumerable<SubjectVm>>(objGroup);
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

        public async Task<int> SaveUserSemester(UserSemesterRequestVm request)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    //QueryParameters query = new QueryParameters();
                    try
                    {
                        int result = await _learning.SaveUserSemester(conn, tran, request.user_id, request.course_id, request.semester_name, request.subjects);
                        if(result > 0)
                        {
                            tran.Commit();
                            return result;
                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
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
        }
        public async Task<IEnumerable<UserSemesterList>> getUserSemester(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    List<UserSemesterList> semList = new List<UserSemesterList>();
                    IEnumerable<tbl_user_semester> objGroup = await _learning.getUserSemester(conn, user_id);
                    //var semester_list = objGroup.GroupBy(sem => sem.semester_name).Select(x => x.First()).ToList();
                    var semesterList = objGroup.DistinctBy(sem => new { sem.user_id, sem.semester_name, sem.course_id }).ToList();
                    semesterList.ForEach(sem =>
                    {
                        UserSemesterList userSem = new UserSemesterList();
                        userSem.semester_name = sem.semester_name;
                        userSem.user_id = sem.user_id;
                        userSem.course_id = sem.course_id;
                        userSem.course_name = sem.course_name;
                        List<SemesterSubjectList> subList = new List<SemesterSubjectList>();
                        var subjectList = objGroup.Where(x => x.semester_name == sem.semester_name && x.course_id == sem.course_id).ToList();
                        subjectList.ForEach(x =>
                        {
                            SemesterSubjectList subObj = new SemesterSubjectList();
                            subObj.course_id = x.course_id;
                            subObj.course_name = x.course_name;
                            subObj.subject_id = x.subject_id;
                            subObj.subject_name = x.subject_name;
                            subObj.is_permission = x.is_permission;
                            subList.Add(subObj);
                        });
                        userSem.subject_list=subList;
                        semList.Add(userSem);
                    });
                    return _mapper.Map<IEnumerable<UserSemesterList>>(semList);
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

        public async Task<IEnumerable<SubjectOverviewVm>> getSubjectOverview(int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_subject_overview> objview = await _learning.getSubjectOverview(conn, subject_id);
                    return _mapper.Map<IEnumerable<SubjectOverviewVm>>(objview);
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
        public async Task<IEnumerable<SubjectCurriculumVm>> getSubjectCurriculum(int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_subject_curriculum> objview = await _learning.getSubjectCurriculum(conn, subject_id);
                    return _mapper.Map<IEnumerable<SubjectCurriculumVm>>(objview);
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

        public async Task<IEnumerable<UnitLessionVm>> getSunjectUnit(string unit_name, int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_unit_lession> objview = await _learning.getSunjectUnit(conn, unit_name, subject_id);
                    return _mapper.Map<IEnumerable<UnitLessionVm>>(objview);
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

        public async Task<IEnumerable<LessionItemsVm>> getLessionItems(int lession_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_lession_item> objview = await _learning.getLessionItems(conn, lession_id);
                    return _mapper.Map<IEnumerable<LessionItemsVm>>(objview);
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

        public async Task<CourseVm> GetCourseDetail(int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_course_vm obj = await _learning.GetCourseDetail(conn, course_id);
                    return _mapper.Map<CourseVm> (obj);
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

        public async Task<QuizDetailVm> GetQuizByLessionId(int lession_id)
        {
            QuizDetailVm quiz = new QuizDetailVm();
            List<QuestionVm> quesList = new List<QuestionVm>();
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {

                    tbl_unit_lession detail = await _learning.GetLessionDetail(conn, lession_id);
                    
                    if (detail != null && detail.lession_id > 0)
                    {
                        quiz.lession_id = detail.lession_id;
                        quiz.lession_name = detail.lession_name;
                        quiz.unit_id = detail.unit_id;
                        IEnumerable<tbl_question_mcq> questions = await _learning.GetQuestionList(conn, lession_id);
                        IEnumerable<tbl_answer_option> answers = await _learning.GetAnswerList(conn, lession_id);

                        if (questions.Count() > 0)
                        {
                            foreach (var qtn in questions)
                            {
                                QuestionVm question = new QuestionVm();
                                question.question_id = qtn.question_id;
                                question.question = qtn.question;
                                question.question_type = qtn.question_type;
                                question.lession_id = qtn.lession_id;
                                question.unit_id = qtn.unit_id;

                                var list = answers.Where(x => x.question_id == qtn.question_id).ToList();

                                question.options = _mapper.Map<IEnumerable<OptionVm>>(list);
                                quesList.Add(question);
                            }
                        }
                        quiz.questions = quesList;
                    }
                    return quiz;
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

        public async Task<LessionItemsVm> GetTopicDetail(int item_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_lession_item obj = await _learning.GetTopicDetail(conn, item_id);
                    return _mapper.Map<LessionItemsVm>(obj);
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
        public async Task<UserVm> GetUserDetail(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    fnd_user obj = await _learning.GetUserDetail(conn, user_id);
                    return _mapper.Map<UserVm>(obj);
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

        public async Task<IEnumerable<CourseVm>> GetAllCourseByCategory(string category_code)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_course_vm> objCourse = await _learning.GetAllCourseByCategory(conn, category_code);
                    return _mapper.Map<IEnumerable<CourseVm>>(objCourse);
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
        public async Task<WorkbookDetailVm> GetWorkbookByLession(int lession_id)
        {
            WorkbookDetailVm quiz = new WorkbookDetailVm();
            List<WorkbookQuestionVm> quesList = new List<WorkbookQuestionVm>();
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {

                    tbl_unit_lession detail = await _learning.GetLessionDetail(conn, lession_id);

                    if (detail != null && detail.lession_id > 0)
                    {
                        quiz.lession_id = detail.lession_id;
                        quiz.lession_name = detail.lession_name;
                        quiz.unit_id = detail.unit_id;
                        IEnumerable<workbook_question> questions = await _learning.GetWorkbookByLession(conn, lession_id);

                        if (questions.Count() > 0)
                        {
                            foreach (var qtn in questions)
                            {
                                WorkbookQuestionVm question = new WorkbookQuestionVm();
                                question.question_id = qtn.question_id;
                                question.question = qtn.question;
                                question.lession_id = qtn.lession_id;
                                quesList.Add(question);
                            }
                        }
                        quiz.questions = quesList;
                    }
                    return quiz;
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

        public async Task<int> SaveUserWorkbook(UserWorkbook entity)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    //QueryParameters query = new QueryParameters();
                    try
                    {
                        string questions = entity.questions.ToJsonString<UserQuestionVm[]>(false);
                        int result = await _learning.SaveUserWorkbook(conn, tran, entity.user_id, entity.lession_id, entity.unit_id, questions);
                        if (result > 0)
                        {
                            tran.Commit();
                            return result;
                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
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
            };
        }
    }
}
