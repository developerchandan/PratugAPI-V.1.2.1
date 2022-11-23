using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web
{
    public interface IImportRepository
    {
        Task<ImportResponseVm> ImportCourse(MemoryStream file_stream, string path);
        Task<ImportResponseVm> ImportSubject(MemoryStream file_stream, string path);
        Task<ImportResponseVm> ImportMCQ(MemoryStream file_stream, string path,string unit_id, int lession_id);
    }
}
