using Prayug.Infrastructure.Models;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface ISubjectRepository
    {
        Task<(IEnumerable<SubjectListVm>, Int64)> GetSubjectList(int pageNo, int pageSize, string sortName, string sortType, SubjectSearchRequestVm entity);
        Task<int> CreateSubject(SubjectVm entity, TokenInfo token);
        Task<int> EditSubject(SubjectVm entity, TokenInfo token);
        Task<SubjectVm> GetSubjectDetail(int subject_id);
    }
}
