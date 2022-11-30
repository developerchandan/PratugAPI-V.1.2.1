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
        Task<(IEnumerable<tbl_common_subject>, Int64)> GetCommonSubjectList(IDbConnection conn, QueryParameters query);
        Task<tbl_subject_vm> CheckSubjectExist(IDbConnection conn, int course_id, string subject_code, string subject_name);
        Task<tbl_common_subject> CheckCommonSubjectExist(IDbConnection conn, string subject_code, string subject_name);
        Task<int> CreateSubject(IDbConnection conn, IDbTransaction tran, int subject_id,int course_id, int group_id, string group_name, string subject_code, string subject_name);
        Task<int> CreateCommonSubject(IDbConnection conn, IDbTransaction tran, string subject_code, string subject_name);
        Task<int> EditSubject(IDbConnection conn, IDbTransaction tran, int id, int subject_id, int course_id, int group_id, string group_name, string subject_code, string subject_name);
        Task<int> EditCommonSubject(IDbConnection conn, IDbTransaction tran, int subject_id, string subject_code, string subject_name);
        Task<tbl_common_subject> GetCommonSubjectDetail(IDbConnection conn, int subject_id);
        Task<tbl_subject_vm> GetSubjectDetail(IDbConnection conn, int subject_id, int course_id);
        Task<int> DeleteOneSubject(IDbConnection conn, IDbTransaction tran,int user_id, int subject_id);
        Task<int> GetDeleteSubject(IDbConnection conn, IDbTransaction tran,int subject_id, int course_id);
        Task<int> GetDeleteCommonSubject(IDbConnection conn, IDbTransaction tran,int subject_id);
        Task<int> DeleteSubjectForInsert(IDbConnection conn, IDbTransaction tran,int subject_id, int course_id);
        Task<int> CreateOneSubject(IDbConnection conn, IDbTransaction tran, int user_id, int subject_id, string semester_name, int course_id, int is_permission);
        Task<IEnumerable<tbl_common_subject>> GetAllSubjects(IDbConnection conn);
        Task<IEnumerable<tbl_subject_vm>> GetAllCertificationSubject(IDbConnection conn);
        Task<int> DeleteSubjectItem(IDbConnection conn, IDbTransaction tran, int id);

    }
}
