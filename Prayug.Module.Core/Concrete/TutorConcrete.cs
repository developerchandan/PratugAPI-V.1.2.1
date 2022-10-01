﻿using Dapper;
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
    public class TutorConcrete : ITutor
    {
        public async Task<(IEnumerable<tbl_course_vm>, long)> GetCourseList(IDbConnection conn, QueryParameters query)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_sort_expression", query.sort_expression);
            param.Add("p_page_size", query.page_size);
            param.Add("p_offsetCount", query.offsetCount);
            param.Add("p_search_query", query.search_query);

            var multi = await conn.QueryMultipleAsync(@"usp_core_get_course_list", param, null, commandType: CommandType.StoredProcedure);

            return (await multi.ReadAsync<tbl_course_vm>(), await multi.ReadSingleAsync<Int64>());
        }

        public async Task<tbl_course_vm> CheckCourseExist(IDbConnection conn, string course_code, string course_name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_course_code", course_code);
            param.Add("p_course_name", course_name);
            return await conn.QueryFirstOrDefaultAsync<tbl_course_vm>(@"usp_core_check_course_exist", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateCourse(IDbConnection conn, IDbTransaction tran, string course_code, string course_name, string image_path, string category)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_code", course_code);
                param.Add("p_course_name", course_name);
                param.Add("p_image_path", image_path);
                param.Add("p_category", category);
                return await conn.ExecuteAsync("usp_core_create_course", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> EditCourse(IDbConnection conn, IDbTransaction tran, int course_id, string course_code, string course_name, string image_path)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", course_id);
                param.Add("p_course_code", course_code);
                param.Add("p_course_name", course_name);
                param.Add("p_image_path", image_path);
                return await conn.ExecuteAsync("usp_core_edit_course", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetCourseDelete(IDbConnection conn, IDbTransaction tran, int course_id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", course_id);
                return await conn.ExecuteAsync(@"usp_core_delete_course", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
        public async Task<course_structure> CheckCourseStructureExist(IDbConnection conn, int course_id, string item_name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_course_id", course_id);
            param.Add("p_item_name", item_name);
            return await conn.QueryFirstOrDefaultAsync<course_structure>(@"usp_core_check_course_structure_exist", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateCourseStructure(IDbConnection conn, IDbTransaction tran, int course_id, string category_code, string item_name, int is_active, string path)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_course_id", course_id);
                param.Add("p_category_code", category_code);
                param.Add("p_item_name", item_name);
                param.Add("p_is_active", is_active);
                param.Add("p_path", path);
                return await conn.ExecuteAsync("usp_core_create_course_structure", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
    }
}