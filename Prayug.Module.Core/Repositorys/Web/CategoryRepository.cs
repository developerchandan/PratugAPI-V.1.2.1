using AutoMapper;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Models;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Web;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Repositorys.Web
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ICategory _category;
        public CategoryRepository(IConfiguration config, ICategory category, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _category = category;
        }

        public async Task<int> CreateCategory(CategoryVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_category_vm Subject = await _category.CheckCategoryExist(conn, entity.category_code, entity.category_name);
                        if (Subject == null)
                        {
                            status = await _category.CreateCategory(conn, tran, entity.category_code, entity.category_name, entity.course_type, entity.duration);
                        }
                        else
                        {
                            status = 0;
                        }
                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 2;
                        }
                        else
                        {

                            tran.Commit();
                            return 1;
                        }
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return status;
        }

        public async Task<IEnumerable<CategoryVm>> GetCategoryList()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    IEnumerable<tbl_category_vm> obj = await _category.GetCategoryList(conn);
                    return _mapper.Map<IEnumerable<CategoryVm>>(obj);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public async Task<IEnumerable<CategoryCourses>> GetCategoryCourses()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    IEnumerable<category_courses> obj = await _category.GetCategoryCourses(conn);
                    return _mapper.Map<IEnumerable<CategoryCourses>>(obj);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public async Task<IEnumerable<CategoryCourses>> GetUserTextSearch(string user_search)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    IEnumerable<category_courses> obj = await _category.GetUserTextSearch(conn, user_search);
                    return _mapper.Map<IEnumerable<CategoryCourses>>(obj);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
