using Prayug.Module.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface ILearning
    {
        Task<IEnumerable<tbl_course_vm>> GetAllCourse(IDbConnection conn);
        Task<IEnumerable<tbl_group_vm>> GetAllGroup(IDbConnection conn, int id);
        Task<IEnumerable<tbl_subject_vm>> GetAllSubject(IDbConnection conn, int id);
        Task<int> SaveUserSemester(IDbConnection conn, IDbTransaction tran, int user_id, int course_id, string semester_name, string subjects);
        Task<IEnumerable<tbl_user_semester>> getUserSemester(IDbConnection conn, int user_id);
        Task<IEnumerable<tbl_subject_overview>> getSubjectOverview(IDbConnection conn, int subject_id);
        Task<IEnumerable<tbl_subject_curriculum>> getSubjectCurriculum(IDbConnection conn, int subject_id);
        Task<IEnumerable<tbl_unit_lession>> getSunjectUnit(IDbConnection conn, string unit_name, int subject_id);
        Task<IEnumerable<tbl_lession_item>> getLessionItems(IDbConnection conn, int lession_id);
        Task<tbl_course_vm> GetCourseDetail(IDbConnection conn, int course_id);
        Task<tbl_unit_lession> GetLessionDetail(IDbConnection conn, int course_id);
        Task<IEnumerable<tbl_question_mcq>> GetQuestionList(IDbConnection conn, int lession_id);
        Task<IEnumerable<tbl_answer_option>> GetAnswerList(IDbConnection conn, int lession_id);
        Task<tbl_lession_item> GetTopicDetail(IDbConnection conn, int item_id);
        Task<fnd_user> GetUserDetail(IDbConnection conn, int user_id);
        Task<IEnumerable<tbl_course_vm>> GetAllCourseByCategory(IDbConnection conn, string category_code);
        Task<IEnumerable<workbook_question>> GetWorkbookByLession(IDbConnection conn, int lession_id); 
        Task<int> SaveUserWorkbook(IDbConnection conn, IDbTransaction tran, int user_id, int lession_id, string unit_id, string questions);

    }
}
