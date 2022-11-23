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
    public interface ITutorRepository
    {
        Task<(IEnumerable<CourseListVm>, Int64)> GetCourseList(int pageNo, int pageSize, string sortName, string sortType, CourseSearchRequestVm entity);
        Task<int> CreateCourse(CourseVm entity, TokenInfo token);
        Task<int> EditCourse(CourseVm entity, TokenInfo token);
        Task<int> GetCourseDelete(int course_id); 
        Task<int> CreateCourseStructure(CourseStructureVm entity, TokenInfo token);
        Task<IEnumerable<CourseStructureVm>> GetCourseStructure(int course_id);
        Task<int> CreateCourseSkill(CourseSkillVm entity, TokenInfo token);
        Task<IEnumerable<CourseSkillVm>> GetCourseSkill(int course_id);
        Task<int> CreateOrder(OrderVm entity, TokenInfo token); 
        Task<(IEnumerable<UserListVm>, Int64)> AllUserList(int pageNo, int pageSize, string sortName, string sortType, UserSearchRequestVm entity);
        Task<int> GetUserActive(int user_id);
        Task<int> UserPermissionAction(int user_id);
        Task<int> GetUserDelete(int user_id);
        Task<UserListVm> GetUserDetail(int user_id);

    }
}
