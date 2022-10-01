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
                        tbl_subject_vm Subject = await _subject.CheckSubjectExist(conn, entity.subject_code, entity.subject_name);
                        if (Subject == null)
                        {
                            status = await _subject.CreateSubject(conn, tran, entity.course_id, entity.group_id, entity.group_name, entity.subject_code, entity.subject_name);
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
        public async Task<int> EditSubject(SubjectVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_subject_vm Subject = await _subject.CheckSubjectExist(conn, entity.subject_code, entity.subject_name);
                        if (Subject != null)
                        {
                            status = await _subject.EditSubject(conn, tran, entity.subject_id, entity.course_id, entity.group_id, entity.subject_code, entity.subject_name);
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

        public async Task<SubjectVm> GetSubjectDetail(int subject_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_subject_vm obj = await _subject.GetSubjectDetail(conn, subject_id);
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
    }
}
