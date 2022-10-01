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
    }
}
