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
    public interface ILession
    {
        Task<tbl_unit_lession> CheckLessionExist(IDbConnection conn, string lession_name, int subject_id, string unit_id);
        Task<int> CreateLession(IDbConnection conn, IDbTransaction tran, string lession_name, int subject_id, string unit_id);
        Task<IEnumerable<tbl_unit_lession>> getLessionBySubject(IDbConnection conn, int subject_id, string unit_id);
        Task<int> CreateLessionItem(IDbConnection conn, IDbTransaction tran, int lession_id, int subject_id, string unit_id, string item_name, string language_name, string item_path);
        Task<(IEnumerable<lession_item_list>, Int64)> GetLessionItemList(IDbConnection conn, QueryParameters query);
        Task<tbl_lession_item> GetItemDetail(IDbConnection conn, int item_id);
        Task<int> SaveQuestions(IDbConnection conn, IDbTransaction tran, mcq_question question);
        Task<int> SaveAnswers(IDbConnection conn, IDbTransaction tran, string json);
        Task<int> SaveLessionMcq(IDbConnection conn, IDbTransaction tran, int lession_id);
        Task<int> SaveWorkbookQuestions(IDbConnection conn, IDbTransaction tran, workbook_question question);
        Task<int> SaveLessionWorkbook(IDbConnection conn, IDbTransaction tran, int lession_id);
        Task<int> GetItemDelete(IDbConnection conn, IDbTransaction tran, int item_id);
        Task<(IEnumerable<mcq_item_list>, Int64)> GetLessionMcqList(IDbConnection conn, QueryParameters query);
        Task<(IEnumerable<lession_list>, Int64)> GetLessionList(IDbConnection conn, QueryParameters query);
        Task<(IEnumerable<lession_list>, Int64)> GetWorkbookList(IDbConnection conn, QueryParameters query);
        Task<int> DeleteLession(IDbConnection conn, IDbTransaction tran, int lession_id);
    }
}
