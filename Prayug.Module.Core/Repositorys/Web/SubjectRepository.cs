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
    public class SubjectRepository : BaseRepository, ISubjectRepository
    {
        private readonly IMapper _mapper;
        private readonly ISubject _subject;
        public SubjectRepository(IConfiguration config, ISubject subject, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _subject = subject;
        }
        public async Task<(IEnumerable<SubjectListVm>, Int64)> GetSubjectList(int pageNo, int pageSize, string sortName, string sortType, SubjectSearchRequestVm entity)
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
                    query.search_query += " AND S.course_code= '" + entity.course_code + "'";
                }

                if (entity != null && !string.IsNullOrEmpty(entity.subject_code))
                {
                    query.search_query += " AND S.subject_code= '" + entity.subject_code + "'";
                }

                conn.Open();
                try
                {
                    (IEnumerable<tbl_subject_vm>, Int64) objSubject = await _subject.GetSubjectList(conn, query);
                    return (_mapper.Map<IEnumerable<SubjectListVm>>(objSubject.Item1), objSubject.Item2);
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
        public async Task<int> CreateSubject(SubjectVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_subject_vm Subject = await _subject.CheckSubjectExist(conn, entity.course_id, entity.subject_code, entity.subject_name);
                        if (Subject == null)
                        {
                            status = await _subject.CreateSubject(conn, tran, entity.subject_id, entity.course_id, entity.group_id, entity.group_name, entity.subject_code, entity.subject_name);
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
        //public async Task<int> EditSubject(SubjectVm entity, TokenInfo token)
        //{
        //    int status = 0;

        //    using (IDbConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (IDbTransaction tran = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                tbl_subject_vm Subject = await _subject.CheckSubjectExist(conn, entity.course_id, entity.subject_code, entity.subject_name);
        //                if (Subject != null)
        //                {
        //                    status = await _subject.EditSubject(conn, tran, entity.subject_id, entity.course_id, entity.group_id, entity.subject_code, entity.subject_name);
        //                }
        //                else
        //                {
        //                    status = 2;
        //                }
        //                //Rollback if any table not inserted
        //                if (status == 0)
        //                {
        //                    tran.Rollback();
        //                    return 0;
        //                }
        //                else
        //                {

        //                    tran.Commit();
        //                }
        //            }
        //            catch
        //            {
        //                tran.Rollback();
        //                throw;
        //            }
        //            finally
        //            {
        //                conn.Close();
        //            }
        //        }
        //    }
        //    return status;
        //}
        public async Task<int> UpdateSubject(SubjectVm entity, TokenInfo token)
        {
            int status = 0;
            int isUpdate = 0;
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_subject_vm Subject = await _subject.CheckSubjectExist(conn, entity.course_id, entity.subject_code, entity.subject_name);
                        //isDelete = (Subject == null) ? 0 : 1;
                        if (Subject == null)
                        {
                            int isDeleted = await _subject.DeleteSubjectItem(conn, tran, entity.id);
                            status = await _subject.CreateSubject(conn, tran, entity.subject_id, entity.course_id, entity.group_id, entity.group_name, entity.subject_code, entity.subject_name);
                            isUpdate = 1;
                        }
                        else
                        {
                            //int isDeleted = await _subject.DeleteSubjectForInsert(conn, tran, entity.id);
                            //if (isDeleted > 0)
                            //{
                                status = await _subject.EditSubject(conn, tran, entity.id, entity.subject_id, entity.course_id, entity.group_id, entity.group_name, entity.subject_code, entity.subject_name);
                            //}
                            isUpdate = 2;
                        }
                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else if(status > 0 && isUpdate == 1)
                        {
                            tran.Commit();
                            return 1;
                        }
                        else if(status > 0 && isUpdate == 2)
                        {
                            tran.Commit();
                            return 2;
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
        public async Task<int> EditCommonSubject(CommonSubjectVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_common_subject Subject = await _subject.CheckCommonSubjectExist(conn, entity.subject_code, entity.subject_name);
                        if (Subject != null && entity.subject_name != Subject.subject_name)
                        {
                            status = await _subject.EditCommonSubject(conn, tran, entity.subject_id, entity.subject_code, entity.subject_name);
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

        public async Task<SubjectVm> GetSubjectDetail(int subject_id, int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_subject_vm obj = await _subject.GetSubjectDetail(conn, subject_id, course_id);
                    return _mapper.Map<SubjectVm>(obj);
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

        public async Task<int> DeleteOneSubject(int user_id, int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _subject.DeleteOneSubject(conn, tran, user_id, subject_id);
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
        public async Task<int> CreateOneSubject(UserSemesterVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        
                        status = await _subject.CreateOneSubject(conn, tran, entity.user_id, entity.subject_id, entity.semester_name, entity.course_id, entity.is_permission);
                        
                        //Rollback if any table not inserted
                        if (status == 0)
                        {
                            tran.Rollback();
                            return 0;
                        }
                        else if (status == 2)
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
        public async Task<int> CreateCommonSubject(CommonSubjectVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_common_subject Subject = await _subject.CheckCommonSubjectExist(conn, entity.subject_code, entity.subject_name);
                        if (Subject == null)
                        {
                            status = await _subject.CreateCommonSubject(conn, tran, entity.subject_code, entity.subject_name);
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
        public async Task<IEnumerable<CommonSubjectVm>> GetAllSubjects()
        {
            using (IDbConnection conn = Connection)
            {
                //QueryParameters query = new QueryParameters();
                conn.Open();
                try
                {
                    IEnumerable<tbl_common_subject> objCourse = await _subject.GetAllSubjects(conn);
                    return _mapper.Map<IEnumerable<CommonSubjectVm>>(objCourse);
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
        public async Task<(IEnumerable<CommonSubjectVm>, Int64)> GetCommonSubjectList(int pageNo, int pageSize, string sortName, string sortType, SubjectSearchRequestVm entity)
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
                    query.search_query += " AND S.course_code= '" + entity.course_code + "'";
                }

                if (entity != null && !string.IsNullOrEmpty(entity.subject_code))
                {
                    query.search_query += " AND S.subject_code= '" + entity.subject_code + "'";
                }

                conn.Open();
                try
                {
                    (IEnumerable<tbl_common_subject>, Int64) objSubject = await _subject.GetCommonSubjectList(conn, query);
                    return (_mapper.Map<IEnumerable<CommonSubjectVm>>(objSubject.Item1), objSubject.Item2);
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
        public async Task<CommonSubjectVm> GetCommonSubjectDetail(int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_common_subject obj = await _subject.GetCommonSubjectDetail(conn, subject_id);
                    return _mapper.Map<CommonSubjectVm>(obj);
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

        public async Task<int> GetDeleteSubject(int subject_id, int course_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _subject.GetDeleteSubject(conn, tran, subject_id, course_id);
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
        public async Task<int> GetDeleteCommonSubject(int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int obj = await _subject.GetDeleteCommonSubject(conn, tran, subject_id);
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
