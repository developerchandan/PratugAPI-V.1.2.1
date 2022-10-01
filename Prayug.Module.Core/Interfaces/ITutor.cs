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
    public interface ITutor
    {
        Task<(IEnumerable<tbl_course_vm>, Int64)> GetCourseList(IDbConnection conn, QueryParameters query);
        Task<tbl_course_vm> CheckCourseExist(IDbConnection conn, string course_code, string course_name);
        Task<int> CreateCourse(IDbConnection conn, IDbTransaction tran, string course_code, string course_name, string image_path, string category);
        Task<int> EditCourse(IDbConnection conn, IDbTransaction tran, int course_id, string course_code, string course_name, string image_path);

        Task<int> GetCourseDelete(IDbConnection conn, IDbTransaction tran, int course_id);
        Task<course_structure> CheckCourseStructureExist(IDbConnection conn, int course_id, string item_name);
        Task<int> CreateCourseStructure(IDbConnection conn, IDbTransaction tran, int course_id, string category_code, string item_name, int is_active, string path);

    }
}
