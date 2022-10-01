using Dapper;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Concrete
{
    public class CategoryConcrete : ICategory
    {
        public async Task<tbl_category_vm> CheckCategoryExist(IDbConnection conn, string category_code, string category_name)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_category_code", category_code);
            param.Add("p_category_name", category_name);

            return await conn.QueryFirstOrDefaultAsync<tbl_category_vm>(@"usp_core_check_category_exist", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateCategory(IDbConnection conn, IDbTransaction tran, string category_code, string category_name)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("p_category_code", category_code);
                param.Add("p_category_name", category_name);
                return await conn.ExecuteAsync("usp_core_create_category", param, tran, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<tbl_category_vm>> GetCategoryList(IDbConnection conn)
        {
            try
            {
                //DynamicParameters param = new DynamicParameters();

                return await conn.QueryAsync<tbl_category_vm>(@"usp_core_get_category_all_list", null, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw;
            }
        }
    }
}
