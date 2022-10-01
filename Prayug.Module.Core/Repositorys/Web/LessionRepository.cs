using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Extensions;
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
    public class LessionRepository : BaseRepository, ILessionRepository
    {
        private readonly IMapper _mapper;
        private readonly ILession _lession;
        public LessionRepository(IConfiguration config, ILession lession, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _lession = lession;
        }
        public async Task<int> CreateLession(LessionVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_unit_lession Subject = await _lession.CheckLessionExist(conn, entity.lession_name, entity.subject_id, entity.unit_id);
                        if (Subject == null)
                        {
                            status = await _lession.CreateLession(conn, tran, entity.lession_name, entity.subject_id, entity.unit_id);
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

        public async Task<LessionVm> GetLessionDetail(int subject_id, string unit_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_course_vm obj = null;// await _lession.GetUnitDetail(conn, subject_id, unit_id);
                    return _mapper.Map<LessionVm>(obj);
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

        public async Task<IEnumerable<LessionVm>> getLessionBySubject(int subject_id, string unit_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    IEnumerable<tbl_unit_lession> obj = await _lession.getLessionBySubject(conn, subject_id, unit_id);
                    return _mapper.Map<IEnumerable<LessionVm>>(obj);
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

        public async Task<int> CreateLessionItem(LessionItemVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        status = await _lession.CreateLessionItem(conn, tran, entity.lession_id, entity.subject_id, entity.unit_id, entity.item_name, entity.item_path);
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
        public async Task<(IEnumerable<LessionItemListVm>, Int64)> GetLessionItemList(int pageNo, int pageSize, string sortName, string sortType, ItemSearchRequestVm entity)
        {
            using (IDbConnection conn = Connection)
            {
                QueryParameters query = new QueryParameters();

                query.page_no = pageNo;
                query.page_size = pageSize;
                query.search_query = string.Empty;
                query.sort_expression = sortName + " " + sortType;

                if (entity != null && !string.IsNullOrEmpty(entity.subject_code))
                {
                    query.search_query += " AND S.subject_code= '" + entity.subject_code + "'";
                }

                conn.Open();
                try
                {
                    (IEnumerable<lession_item_list>, Int64) objSubject = await _lession.GetLessionItemList(conn, query);
                    return (_mapper.Map<IEnumerable<LessionItemListVm>>(objSubject.Item1), objSubject.Item2);
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
        public async Task<LessionItemListVm> GetItemDetail(int item_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_lession_item obj = await _lession.GetItemDetail(conn, item_id);
                    return _mapper.Map<LessionItemListVm>(obj);
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
        public async Task<int> SaveQuestions(McqQuestionVm[] entity, int lession_id, TokenInfo userdetail)
        {
            List<mcq_answer> answerList;
            using (IDbConnection conn = Connection)
            {
                int isMcq = 0;
                int ques_id=0, ans_id = 0;
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        Int64 last_modified = Convert.ToInt64(last_modified_date_time);
                        //Upadte data in table inv_trip
                        if (entity.Length > 0)
                        {
                            foreach (var qtn in entity)
                            {
                                mcq_question question = new mcq_question();
                                question.lession_id = lession_id;
                                question.question_id = qtn.question_id;
                                question.question = qtn.question;
                                question.type_of_question = qtn.type_of_question;
                                question.sequence = qtn.sequence;
                                ques_id = await _lession.SaveQuestions(conn, tran, question);
                                answerList = new List<mcq_answer>();
                                foreach (var ans in qtn.answers)
                                {
                                    mcq_answer answer = new mcq_answer();
                                    answer.lession_id = lession_id;
                                    answer.question_id = ques_id;
                                    answer.answer_id = ans.answer_id;
                                    answer.answer = ans.answer;
                                    answer.is_answer = ans.is_answer;
                                    answer.sequence = ans.sequence;
                                    answerList.Add(answer);
                                }
                                ans_id = await _lession.SaveAnswers(conn, tran, (answerList.Count > 0) ? answerList.ToJsonString<List<mcq_answer>>(false) : "");
                                
                            }
                            if (ques_id > 0 && ans_id >0)
                            {
                                isMcq = await _lession.SaveLessionMcq(conn, tran, lession_id);
                            }
                            if (isMcq == 1 || isMcq == 2)
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
                        else
                        {
                            tran.Rollback();
                            return 0;
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
            return 1;
        }
        public async Task<int> SaveWorkbookQuestions(WorkbookQuestionVm[] entity, int lession_id, TokenInfo userdetail)
        {
            List<mcq_answer> answerList;
            using (IDbConnection conn = Connection)
            {
                int isMcq = 0;
                int ques_id = 0, ans_id = 0;
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        Int64 last_modified = Convert.ToInt64(last_modified_date_time);
                        //Upadte data in table inv_trip
                        if (entity.Length > 0)
                        {
                            foreach (var qtn in entity)
                            {
                                workbook_question question = new workbook_question();
                                question.lession_id = lession_id;
                                question.question_id = qtn.question_id;
                                question.question = qtn.question;
                                question.sequence = qtn.sequence;
                                ques_id = await _lession.SaveWorkbookQuestions(conn, tran, question);
                            }
                            if (ques_id > 0)
                            {
                                isMcq = await _lession.SaveLessionWorkbook(conn, tran, lession_id);
                            }
                            if (isMcq == 1 || isMcq == 2)
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
                        else
                        {
                            tran.Rollback();
                            return 0;
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
            return 1;
        }
    }
}
