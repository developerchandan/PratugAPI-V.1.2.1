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
    public class ImportConcrete : IImport
    {
        public async Task<(IEnumerable<import_course>, import_response)> ImportCourse(IDbConnection conn, string course)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_json", course);
            //return await conn.QueryAsync<org_country>(@"usp_core_import_country", param, commandType: CommandType.StoredProcedure);
            var multi = await conn.QueryMultipleAsync(@"usp_core_import_course", param, null, commandType: CommandType.StoredProcedure);

            return (await multi.ReadAsync<import_course>(), await multi.ReadSingleAsync<import_response>());
        }

        public Task<(IEnumerable<import_course>, import_response)> ImportSubject(IDbConnection conn, string course)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<import_mcq>, import_response)> ImportMCQ(IDbConnection conn, IDbTransaction tran, string mcq, string unit_id, int lession_id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("p_mcq", mcq);
            param.Add("p_unit_id", unit_id);
            //return await conn.QueryAsync<org_country>(@"usp_core_import_country", param, commandType: CommandType.StoredProcedure);
            var multi = await conn.QueryMultipleAsync(@"usp_core_import_mcq", param, tran, commandType: CommandType.StoredProcedure);

            return (await multi.ReadAsync<import_mcq>(), await multi.ReadSingleAsync<import_response>());
        }
    }
}
