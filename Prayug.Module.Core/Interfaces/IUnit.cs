using Prayug.Module.Core.Models;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface IUnit
    {
        Task<tbl_subject_curriculum> CheckUnitExist(IDbConnection conn, int course_id, int subject_id, string unit_id);
        Task<int> CreateUnit(IDbConnection conn, IDbTransaction tran, int course_id, int subject_id, string unit_id, int sequensce);
        Task<tbl_course_vm> GetUnitDetail(IDbConnection conn, int subject_id, string unit_id);
        Task<(IEnumerable<unit_list>, Int64)> GetUnitList(IDbConnection conn, QueryParameters query);
        Task<int> DeleteUnit(IDbConnection conn, IDbTransaction tran, int subject_id, string unit_id);

    }
}
