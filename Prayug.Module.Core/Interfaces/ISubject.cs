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
    public interface ISubject
    {
        Task<(IEnumerable<tbl_subject_vm>, Int64)> GetSubjectList(IDbConnection conn, QueryParameters query);
        Task<tbl_subject_vm> CheckSubjectExist(IDbConnection conn, string subject_code, string subject_name);
        Task<int> CreateSubject(IDbConnection conn, IDbTransaction tran,int course_id, int group_id, string group_name, string subject_code, string subject_name);
        Task<int> EditSubject(IDbConnection conn, IDbTransaction tran, int subject_id, int course_id, int group_id, string subject_code, string subject_name);
        Task<tbl_subject_vm> GetSubjectDetail(IDbConnection conn, int subject_id);
    }
}
