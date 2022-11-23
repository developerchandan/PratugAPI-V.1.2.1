using Dapper;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Models;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Concrete
{
    public class SubjectConcrete : ISubject
    {
        public async Task<tbl_subject_vm> CheckSubjectExist(IDbConnection conn, int course_id, string subject_code, string subject_name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_course_id", course_id);
            param.Add("p_subject_code", subject_code);
            param.Add("p_subject_name", subject_name);
            return await conn.QueryFirstOrDefaultAsync<tbl_subject_vm>(@"usp_core_check_subject_exist", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateSubject(IDbConnection conn, IDbTransaction tran, int subject_id, int course_id, int group_id, string group_name, string subject_code, string subject_name)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                param.Add("p_course_id", course_id);
                param.Add("p_group_id", group_id);
                param.Add("p_group_name", group_name);
                param.Add("p_subject_code", subject_code);
                param.Add("p_subject_name", subject_name);
                return await conn.ExecuteAsync("usp_core_create_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<tbl_common_subject> CheckCommonSubjectExist(IDbConnection conn, string subject_code, string subject_name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_subject_code", subject_code);
            param.Add("p_subject_name", subject_name);
            return await conn.QueryFirstOrDefaultAsync<tbl_common_subject>(@"usp_core_check_common_subject_exist", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateCommonSubject(IDbConnection conn, IDbTransaction tran, string subject_code, string subject_name)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_code", subject_code);
                param.Add("p_subject_name", subject_name);
                return await conn.ExecuteAsync("usp_core_create_common_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> EditSubject(IDbConnection conn, IDbTransaction tran, int id, int subject_id, int course_id, int group_id, string group_name, string subject_code, string subject_name)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_id", id);
                param.Add("p_subject_id", subject_id);
                param.Add("p_course_id", course_id);
                param.Add("p_group_id", group_id);
                param.Add("p_subject_code", subject_code);
                param.Add("p_subject_name", subject_name);
                return await conn.ExecuteAsync("usp_core_edit_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> EditCommonSubject(IDbConnection conn, IDbTransaction tran, int subject_id, string subject_code, string subject_name)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                param.Add("p_subject_code", subject_code);
                param.Add("p_subject_name", subject_name);
                return await conn.ExecuteAsync("usp_core_edit_common_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<(IEnumerable<tbl_subject_vm>, long)> GetSubjectList(IDbConnection conn, QueryParameters query)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_sort_expression", query.sort_expression);
            param.Add("p_page_size", query.page_size);
            param.Add("p_offsetCount", query.offsetCount);
            param.Add("p_search_query", query.search_query);

            var multi = await conn.QueryMultipleAsync(@"usp_core_get_subject_list", param, null, commandType: CommandType.StoredProcedure);

            return (await multi.ReadAsync<tbl_subject_vm>(), await multi.ReadSingleAsync<Int64>());
        }
        public async Task<(IEnumerable<tbl_common_subject>, long)> GetCommonSubjectList(IDbConnection conn, QueryParameters query)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_sort_expression", query.sort_expression);
            param.Add("p_page_size", query.page_size);
            param.Add("p_offsetCount", query.offsetCount);
            param.Add("p_search_query", query.search_query);

            var multi = await conn.QueryMultipleAsync(@"usp_core_get_common_subject_list", param, null, commandType: CommandType.StoredProcedure);

            return (await multi.ReadAsync<tbl_common_subject>(), await multi.ReadSingleAsync<Int64>());
        }

        public async Task<tbl_subject_vm> GetSubjectDetail(IDbConnection conn, int subject_id, int course_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                param.Add("p_course_id", course_id);
                return await conn.QueryFirstOrDefaultAsync<tbl_subject_vm>(@"usp_core_get_subject_detail_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteOneSubject(IDbConnection conn, IDbTransaction tran, int user_id, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                param.Add("p_subject_id", subject_id);
                return await conn.ExecuteAsync(@"usp_core_delete_user_one_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<tbl_common_subject> GetCommonSubjectDetail(IDbConnection conn, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                return await conn.QueryFirstOrDefaultAsync<tbl_common_subject>(@"usp_core_get_common_subject_detail_byid", param, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CreateOneSubject(IDbConnection conn, IDbTransaction tran, int user_id, int subject_id, string semester_name, int course_id, int is_permission)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_user_id", user_id);
                param.Add("p_subject_id", subject_id);
                param.Add("p_semester_name", semester_name);
                param.Add("p_course_id", course_id);
                param.Add("p_is_permission", is_permission);
                return await conn.ExecuteAsync("usp_core_create_one_semester_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<tbl_common_subject>> GetAllSubjects(IDbConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                return await conn.QueryAsync<tbl_common_subject>(@"usp_core_get_all_common_subject_list", null, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteSubjectForInsert(IDbConnection conn, IDbTransaction tran, int subject_id, int course_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", course_id);
                param.Add("p_subject_id", subject_id);
                return await conn.ExecuteAsync(@"usp_core_delete_subject_for_insert_same", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> GetDeleteSubject(IDbConnection conn, IDbTransaction tran, int subject_id, int course_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                param.Add("p_course_id", course_id);
                return await conn.ExecuteAsync(@"usp_core_delete_subject_for_insert_same", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteSubjectItem(IDbConnection conn, IDbTransaction tran, int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_id", id);
                return await conn.ExecuteAsync(@"usp_core_delete_subject_one_Item", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> GetDeleteCommonSubject(IDbConnection conn, IDbTransaction tran, int subject_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_subject_id", subject_id);
                return await conn.ExecuteAsync(@"usp_core_delete_common_subject", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

    }
}
