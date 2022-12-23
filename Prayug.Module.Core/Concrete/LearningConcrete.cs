using Dapper;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Concrete
{
    public class LearningConcrete : ILearning
    {
        public async Task<IEnumerable<tbl_course_vm>> GetAllCourse(IDbConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                return await conn.QueryAsync<tbl_course_vm>(@"usp_core_get_all_course_list", null, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_group_vm>> GetAllGroup(IDbConnection conn, int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters(); 
                param.Add("p_course_id", id);
                return await conn.QueryAsync<tbl_group_vm>(@"usp_core_get_all_group_list_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<tbl_subject_vm>> GetAllSubject(IDbConnection conn, int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", id);
                return await conn.QueryAsync<tbl_subject_vm>(@"usp_core_get_all_subject_list_by_courseid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SaveUserSemester(IDbConnection conn, IDbTransaction tran, int user_id, int course_id, string semester_name, string subjects)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                param.Add("p_course_id", course_id);
                param.Add("p_semester_name", semester_name);
                param.Add("p_subjects", subjects);
                return await conn.ExecuteAsync(@"usp_core_user_save_semester", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_user_semester>> getUserSemester(IDbConnection conn, int user_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                return await conn.QueryAsync<tbl_user_semester>(@"usp_core_get_user_semester_subject_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_subject_overview>> getSubjectOverview(IDbConnection conn, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                return await conn.QueryAsync<tbl_subject_overview>(@"usp_core_get_user_subject_overview_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<tbl_subject_curriculum>> getSubjectCurriculum(IDbConnection conn, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                return await conn.QueryAsync<tbl_subject_curriculum>(@"usp_core_get_user_subject_curriculumn_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_unit_lession>> getSunjectUnit(IDbConnection conn, string unit_name, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_unit_name", unit_name);
                param.Add("p_subject_id", subject_id);
                return await conn.QueryAsync<tbl_unit_lession>(@"usp_core_get_subject_unit_lession_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_lession_item>> getLessionItems(IDbConnection conn, int lession_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_lession_id", lession_id);
                return await conn.QueryAsync<tbl_lession_item>(@"usp_core_get_subject_lession_items_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<tbl_course_vm> GetCourseDetail(IDbConnection conn, int course_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", course_id);
                return await conn.QueryFirstOrDefaultAsync<tbl_course_vm>(@"usp_core_get_course_detail_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<tbl_unit_lession> GetLessionDetail(IDbConnection conn, int lession_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_lession_id", lession_id);
                return await conn.QueryFirstOrDefaultAsync<tbl_unit_lession>(@"usp_core_get_lession_detail_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<tbl_question_mcq>> GetQuestionList(IDbConnection conn, int lession_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_lession_id", lession_id);
                return await conn.QueryAsync<tbl_question_mcq>(@"usp_core_get_subject_lession_question_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_answer_option>> GetAnswerList(IDbConnection conn, int lession_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_lession_id", lession_id);
                return await conn.QueryAsync<tbl_answer_option>(@"usp_core_get_subject_lession_option_list", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<tbl_lession_item> GetTopicDetail(IDbConnection conn, int item_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_item_id", item_id);
                return await conn.QueryFirstOrDefaultAsync<tbl_lession_item>(@"usp_core_get_lession_topic_detail_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<fnd_user> GetUserDetail(IDbConnection conn, int user_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                return await conn.QueryFirstOrDefaultAsync<fnd_user>(@"usp_core_get_user_detail_by_userid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<user_profile_vm> GetUserProfile(IDbConnection conn, int user_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                return await conn.QueryFirstOrDefaultAsync<user_profile_vm>(@"usp_core_get_user_profile_detail_by_userid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_course_vm>> GetAllCourseByCategory(IDbConnection conn, string category_code)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_category_code", category_code);
                return await conn.QueryAsync<tbl_course_vm>(@"usp_core_get_course_list_by_category", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<workbook_question>> GetWorkbookByLession(IDbConnection conn, int lession_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_lession_id", lession_id);
                return await conn.QueryAsync<workbook_question>(@"usp_core_get_subject_lession_workbook_questions", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SaveUserWorkbook(IDbConnection conn, IDbTransaction tran, int user_id, int lession_id, string unit_id, string questions)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                param.Add("p_lession_id", lession_id);
                param.Add("p_unit_id", unit_id);
                param.Add("p_questions", questions);
                return await conn.ExecuteAsync(@"usp_core_save_user_workbook", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> CreateUserProfile(IDbConnection conn, IDbTransaction tran, user_profile_vm profile)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", profile.user_id);
                param.Add("p_first_name", profile.first_name);
                param.Add("p_last_name", profile.last_name);
                param.Add("p_email", profile.email);
                param.Add("p_headline", profile.headline);
                param.Add("p_mobile", profile.mobile);
                param.Add("p_collage", profile.collage);
                param.Add("p_university", profile.university);
                param.Add("p_facebook", profile.facebook);
                param.Add("p_twitter", profile.twitter);
                param.Add("p_youtube", profile.youtube);
                return await conn.ExecuteAsync(@"usp_core_save_update_user_profile", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
    }
}
