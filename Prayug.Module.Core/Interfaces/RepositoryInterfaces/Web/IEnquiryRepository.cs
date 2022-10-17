using Prayug.Module.Core.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface IEnquiryRepository
    {
        Task<int> SaveEnquiry(EnquiryRequestVm entity);
    }
}
