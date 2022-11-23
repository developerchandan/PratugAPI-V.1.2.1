using Prayug.Module.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces
{
    public interface ICategory
    {
        Task<tbl_category_vm> CheckCategoryExist(IDbConnection conn, string category_code, string category_name);
        Task<int> CreateCategory(IDbConnection conn, IDbTransaction tran, string category_code, string category_name, int course_type, string duration);
        Task<IEnumerable<tbl_category_vm>> GetCategoryList(IDbConnection conn);
    }
}
