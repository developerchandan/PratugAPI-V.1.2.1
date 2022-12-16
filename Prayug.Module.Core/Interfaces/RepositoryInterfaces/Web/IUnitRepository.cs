using Prayug.Infrastructure.Models;
using Prayug.Module.Core.ViewModels.Request;
using Prayug.Module.Core.ViewModels.Response;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface IUnitRepository
    {
        Task<int> CreateUnit(UnitVm entity, TokenInfo token);
        Task<CourseVm> GetUnitDetail(int subject_id, string unit_id);
        Task<(IEnumerable<UnitListVm>, Int64)> GetUnitList(int pageNo, int pageSize, string sortName, string sortType, UnitSearchRequestVm entity);
        Task<int> DeleteUnit(int subject_id, string unit_id);
    }
}
