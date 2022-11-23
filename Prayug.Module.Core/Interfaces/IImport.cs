using Prayug.Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface IImport
    {
        Task<(IEnumerable<import_course>, import_response)> ImportCourse(IDbConnection conn, string course);
        Task<(IEnumerable<import_course>, import_response)> ImportSubject(IDbConnection conn, string course);
        Task<(IEnumerable<import_mcq>, import_response)> ImportMCQ(IDbConnection conn, IDbTransaction tran, string mcq, string unit_id, int lession_id);
    }
}
