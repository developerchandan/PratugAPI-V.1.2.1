using Prayug.Infrastructure.Models;
using Prayug.Module.Core.ViewModels.Request;
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
        Task<(IEnumerable<CommonSubjectVm>, Int64)> GetCommonSubjectList(int pageNo, int pageSize, string sortName, string sortType, SubjectSearchRequestVm entity);
        Task<int> CreateSubject(SubjectVm entity, TokenInfo token);
        Task<int> CreateCommonSubject(CommonSubjectVm entity, TokenInfo token);
        //Task<int> EditSubject(SubjectVm entity, TokenInfo token);
        Task<int> UpdateSubject(SubjectVm entity, TokenInfo token);
        Task<int> EditCommonSubject(CommonSubjectVm entity, TokenInfo token);
        Task<SubjectVm> GetSubjectDetail(int subject_id, int course_id);
        Task<CommonSubjectVm> GetCommonSubjectDetail(int subject_id);
        Task<int> DeleteOneSubject(int user_id, int subject_id);
        Task<int> GetDeleteSubject(int subject_id, int course_id);
        Task<int> GetDeleteCommonSubject(int subject_id);
        Task<int> CreateOneSubject(UserSemesterVm entity, TokenInfo token);
        Task<IEnumerable<CommonSubjectVm>> GetAllSubjects();
        Task<IEnumerable<SubjectVm>> GetAllCertificationSubject();
    }
}
