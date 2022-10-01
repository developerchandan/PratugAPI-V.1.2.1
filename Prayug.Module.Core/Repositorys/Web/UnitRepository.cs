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
    public class UnitRepository : BaseRepository, IUnitRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnit _unit;
        public UnitRepository(IConfiguration config, IUnit unit, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _unit = unit;
        }

        public async Task<int> CreateUnit(UnitVm entity, TokenInfo token)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        tbl_subject_curriculum Subject = await _unit.CheckUnitExist(conn, entity.course_id, entity.subject_id, entity.unit_id);
                        if (Subject == null)
                        {
                            status = await _unit.CreateUnit(conn, tran, entity.course_id, entity.subject_id, entity.unit_id, entity.sequensce);
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

        public async Task<CourseVm> GetUnitDetail(int subject_id, string unit_id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                try
                {
                    tbl_course_vm obj = await _unit.GetUnitDetail(conn, subject_id, unit_id);
                    return _mapper.Map<CourseVm>(obj);
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
