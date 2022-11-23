using Prayug.Infrastructure.Models;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface ILessionRepository
    {
        Task<int> CreateLession(LessionVm entity, TokenInfo token);
        Task<LessionVm> GetLessionDetail(int subject_id, string unit_id);
        Task<IEnumerable<LessionVm>> getLessionBySubject(int subject_id, string unit_id);
        Task<int> CreateLessionItem(LessionItemVm entity, TokenInfo token);
        Task<(IEnumerable<LessionItemListVm>, Int64)> GetLessionItemList(int pageNo, int pageSize, string sortName, string sortType, ItemSearchRequestVm entity);
        Task<LessionItemListVm> GetItemDetail(int item_id);
        Task<int> SaveQuestions(McqQuestionVm[] entity, int lession_id, TokenInfo userdetail);
        Task<int> SaveWorkbookQuestions(WorkbookQuestionVm[] entity, int lession_id, TokenInfo userdetail);
        Task<int> GetItemDelete(int item_id);
    }
}
