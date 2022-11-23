using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prayug.Infrastructure;
using Prayug.Infrastructure.Enums;
using Prayug.Infrastructure.ResponseFormat;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Prayug.Portal.Controllers.Web.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ImportDataController : ControllerBase
    {
        private IImportRepository _importRepository;
        public ImportDataController(IImportRepository importRepository)
        {
            _importRepository = importRepository;
        }
        [HttpPost("ImportCourse")]
        public async Task<IActionResult> ImportCourse(IFormFile files)
        {
            using (ISingleModelResponse<ImportResponseVm> response = new SingleModelResponse<ImportResponseVm>())
            {
                try
                {
                    //DataTable dt = new DataTable();
                    //Checking file content length and Extension must be .xlsx  
                    if (files != null && files.Length > 0 && System.IO.Path.GetExtension(files.FileName).ToLower() == ".xlsx")
                    {
                        //GlobalSettings.WebRootPath
                        var folderPath = "ImportExcel/" + Path.GetFileNameWithoutExtension(files.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(files.FileName);
                        string filePath = await UploadFiles(files, folderPath);
                        var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
                        ImportResponseVm fileRead = await _importRepository.ImportCourse(stream, filePath);
                        fileRead.file_path = folderPath;
                        //return File(fileRead, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportBrand");
                        response.objResponse = fileRead;
                        response.Status = ResponseMessageEnum.Success;

                        response.Message = fileRead != null ? "Success" : "Failure";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "Excel file does not exit";
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return Ok(response);
                }
            }
        }
        public static async Task<string> UploadFiles(IFormFile file, string folderPath)
        {
            string retPath = string.Empty;
            var uploadFilePath = Path.Combine(GlobalSettings.WebRootPath);
            if (file.Length > 0)
            {

                var filePath = Path.Combine(uploadFilePath, folderPath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                retPath = Convert.ToString(filePath);
            }
            return retPath;
        }
        [HttpPost("ImportSubject")]
        public async Task<IActionResult> ImportSubject(IFormFile files)
        {
            using (ISingleModelResponse<ImportResponseVm> response = new SingleModelResponse<ImportResponseVm>())
            {
                try
                {
                    //DataTable dt = new DataTable();
                    //Checking file content length and Extension must be .xlsx  
                    if (files != null && files.Length > 0 && System.IO.Path.GetExtension(files.FileName).ToLower() == ".xlsx")
                    {
                        //GlobalSettings.WebRootPath
                        var folderPath = "ImportExcel/" + Path.GetFileNameWithoutExtension(files.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(files.FileName);
                        string filePath = await UploadFiles(files, folderPath);
                        var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
                        ImportResponseVm fileRead = await _importRepository.ImportSubject(stream, filePath);
                        fileRead.file_path = folderPath;
                        //return File(fileRead, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportBrand");
                        response.objResponse = fileRead;
                        response.Status = ResponseMessageEnum.Success;

                        response.Message = fileRead != null ? "Success" : "Failure";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "Excel file does not exit";
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return Ok(response);
                }
            }
        }
        [HttpPost("ImportMCQ")]
        public async Task<IActionResult> ImportMCQ(IFormFile files, string unit_id, int lession_id)
        {
            //int lession_id = 0;
            //IFormFile files = entity.files;
            using (ISingleModelResponse<ImportResponseVm> response = new SingleModelResponse<ImportResponseVm>())
            {
                try
                {
                    if (files != null && files.Length > 0 && System.IO.Path.GetExtension(files.FileName).ToLower() == ".xlsx")
                    {
                        //GlobalSettings.WebRootPath
                        var folderPath = "ImportExcel/" + Path.GetFileNameWithoutExtension(files.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(files.FileName);
                        string filePath = await UploadFiles(files, folderPath);
                        var stream = new MemoryStream(System.IO.File.ReadAllBytes(filePath));
                        ImportResponseVm fileRead = await _importRepository.ImportMCQ(stream, filePath, unit_id, lession_id);
                        fileRead.file_path = folderPath;
                        //return File(fileRead, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ImportBrand");
                        response.objResponse = fileRead;
                        response.Status = ResponseMessageEnum.Success;

                        response.Message = fileRead != null ? "Success" : "Failure";
                        return Ok(response);
                    }
                    else
                    {
                        response.Status = ResponseMessageEnum.Failure;
                        response.Message = "Excel file does not exit";
                        return Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Status = ResponseMessageEnum.Exception;
                    response.Message = ex.Message;
                    return Ok(response);
                }
            }
        }

    }
}
