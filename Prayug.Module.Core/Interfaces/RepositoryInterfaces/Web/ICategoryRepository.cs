using Prayug.Infrastructure.Models;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface ICategoryRepository
    {
        Task<int> CreateCategory(CategoryVm entity, TokenInfo token); 
        Task<IEnumerable<CategoryVm>> GetCategoryList();
        Task<IEnumerable<CategoryCourses>> GetCategoryCourses();
        Task<IEnumerable<CategoryCourses>> GetUserTextSearch(string user_search);
    }
}
