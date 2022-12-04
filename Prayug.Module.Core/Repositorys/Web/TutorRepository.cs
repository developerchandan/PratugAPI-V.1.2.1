using AutoMapper;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Models;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Request;
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
    public class TutorRepository : BaseRepository, ITutorRepository
    {
        private readonly IMapper _mapper;
        private readonly ITutor _tutor;
        public TutorRepository(IConfiguration config, ITutor tutor, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _tutor = tutor;
        }

        public async Task<(IEnumerable<CourseListVm>, Int64)> GetCourseList(int pageNo, int pageSize, string sortName, string sortType, CourseSearchRequestVm entity)
        {
            using (IDbConnection conn = Connection)
            {
                QueryParameters query = new QueryParameters();

                query.page_no = pageNo;
                query.page_size = pageSize;
                query.search_query = string.Empty;
                query.sort_expression = sortName + " " + sortType;

                if (entity != null && !string.IsNullOrEmpty(entity.course_code))
                {
                    query.search_query += " AND course_code= '" + entity.course_code + "'";
                }

                if (entity != null && !string.IsNullOrEmpty(entity.course_name))
                {
                    query.search_query += " AND course_name= '" + entity.course_name + "'";
                }

                conn.Open();
                try
                {
                    (IEnumerable<tbl_course_vm>, Int64) objcourse = await _tutor.GetCourseList(conn, query);
                    return (_mapper.Map<IEnumerable<CourseListVm>>(objcourse.Item1), objcourse.Item2);
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
        public async Task<int> CreateCourse(CourseVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_course_vm course = await _tutor.CheckCourseExist(conn, entity.course_code, entity.course_name);
                        if (course == null)
                        {
                            status = await _tutor.CreateCourse(conn, tran, entity.course_code, entity.course_name, entity.image_path, entity.category, entity.description);
                        }
                        else
                        {
                            status = 2;
                        }
                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
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
        public async Task<int> EditCourse(CourseVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_course_vm course = await _tutor.CheckCourseExist(conn, entity.course_code, entity.course_name);
                        if (course != null)
                        {
                            status = await _tutor.EditCourse(conn, tran, entity.course_id, entity.course_code, entity.course_name, entity.image_path, entity.description);
                        }
                        else
                        {
                            status = 2;
                        }
                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else
                        {

                            tran.Commit();
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

        public async Task<int> GetCourseDelete(int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _tutor.GetCourseDelete(conn, tran, course_id);
                        if (obj == 1)
                        {
                            tran.Commit();
                            return 1;

                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
                        }
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
        public async Task<int> CreateCourseStructure(CourseStructureVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //course_structure course = await _tutor.CheckCourseStructureExist(conn, entity.course_id, entity.item_name);
                        //if (course == null)
                        //{
                        //    status = await _tutor.CreateCourseStructure(conn, tran, entity.course_id, entity.category_code, entity.item_name, entity.is_active, entity.path);
                        //}
                        //else
                        //{
                        //    status = 2;
                        //}

                        status = await _tutor.CreateCourseStructure(conn, tran, entity.course_id, entity.category_code, entity.item_name, entity.is_active, entity.path);

                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
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

        public async Task<IEnumerable<CourseStructureVm>> GetCourseStructure(int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<course_structure> objCourse = await _tutor.GetCourseStructure(conn, course_id);
                    return _mapper.Map<IEnumerable<CourseStructureVm>>(objCourse);
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
        public async Task<int> CreateCourseSkill(CourseSkillVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        course_skill course = await _tutor.CheckCourseSkillExist(conn, entity.course_id, entity.skill_name);
                        if (course == null)
                        {
                            status = await _tutor.CreateCourseSkill(conn, tran, entity.course_id, entity.category_code, entity.skill_name, entity.path);
                        }
                        else
                        {
                            status = 2;
                        }

                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
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

        public async Task<IEnumerable<CourseSkillVm>> GetCourseSkill(int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<course_skill> objCourse = await _tutor.GetCourseSkill(conn, course_id);
                    return _mapper.Map<IEnumerable<CourseSkillVm>>(objCourse);
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
        public async Task<int> CreateOrder(OrderVm entity, TokenInfo token)
        {
            int id = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //course_skill course = await _tutor.CheckOrderExist(conn, entity.user_id. entity.course, entity.skill_name);
                        //if (course == null)
                        //{
                            id = await _tutor.CreateOrder(conn, tran, entity.user_id, entity.user_name, entity.order_number, entity.course_code, entity.payment_id);
                        //}
                        //else
                        //{
                        //    status = 2;
                        //}

                        //Rollback if any table not inserted
                        if (id == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else
                        {
                            tran.Commit();
                            return id;
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
            return id;
        }
        public async Task<(IEnumerable<UserListVm>, Int64)> AllUserList(int pageNo, int pageSize, string sortName, string sortType, UserSearchRequestVm entity)
        {
            using (IDbConnection conn = Connection)
            {
                QueryParameters query = new QueryParameters();

                query.page_no = pageNo;
                query.page_size = pageSize;
                query.search_query = string.Empty;
                query.sort_expression = sortName + " " + sortType;

                if (entity != null && entity.user_id>0)
                {
                    query.search_query += " AND user_id= '" + entity.user_id + "'";
                }

                if (entity != null && !string.IsNullOrEmpty(entity.first_name))
                {
                    query.search_query += " AND first_name= '" + entity.first_name + "'";
                }

                conn.Open();
                try
                {
                    (IEnumerable<all_user_list>, Int64) objcourse = await _tutor.AllUserList(conn, query);
                    return (_mapper.Map<IEnumerable<UserListVm>>(objcourse.Item1), objcourse.Item2);
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
        public async Task<int> GetUserActive(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _tutor.GetUserActive(conn, tran, user_id);
                        if (obj == 1)
                        {
                            tran.Commit();
                            return 1;
                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
                        }
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

        public async Task<int> GetUserDelete(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _tutor.GetUserDelete(conn, tran, user_id);
                        if (obj == 1)
                        {
                            tran.Commit();
                            return 1;

                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
                        }
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
        public async Task<UserListVm> GetUserDetail(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    all_user_list obj = await _tutor.GetUserDetail(conn, user_id);
                    return _mapper.Map<UserListVm>(obj);
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
        public async Task<int> UserPermissionAction(int user_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _tutor.UserPermissionAction(conn, tran, user_id);
                        if (obj == 1)
                        {
                            tran.Commit();
                            return 1;
                        }
                        else
                        {
                            tran.Rollback();
                            return 0;
                        }
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
}
