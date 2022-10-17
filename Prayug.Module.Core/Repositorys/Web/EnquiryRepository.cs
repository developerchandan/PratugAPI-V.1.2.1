using AutoMapper;
using Microsoft.Extensions.Configuration;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prayug.Module.Core.ViewModels.Request;
using Prayug.Module.Core.Models;
using System.Data;

namespace Prayug.Module.Core.Repositorys.Web
{
    public class EnquiryRepository : BaseRepository, IEnquiryRepository
    {
        private readonly IMapper _mapper;
        private readonly IEnquiry _enquiry;
        public EnquiryRepository(IConfiguration config, IEnquiry enquiry, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _enquiry = enquiry;
        }

        public async Task<int> SaveEnquiry(EnquiryRequestVm entity)
        {
            int status = 0;

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        
                        status = await _enquiry.SaveEnquiry(conn, tran, entity.user_name, entity.email, entity.mobile, entity.course, entity.admission, entity.message, entity.enquiry_for);
                        
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
    }
}
