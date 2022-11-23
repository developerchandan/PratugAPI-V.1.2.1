using AutoMapper;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using Prayug.Infrastructure.Extensions;
using Prayug.Module.Core.Interfaces;
using Prayug.Module.Core.Interfaces.RepositoryInterfaces.Web;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Response;
using Prayug.Module.Core.ViewModels.Web;
using Prayug.Module.DataBaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.Repositorys.Web
{
    public class ImportRepository : BaseRepository, IImportRepository
    {
        private readonly IMapper _mapper;
        private readonly IImport _import;
        public ImportRepository(IConfiguration config, IImport import, IMapper mapper) : base(config)
        {
            _mapper = mapper;
            _import = import;
        }

        public async Task<ImportResponseVm> ImportCourse(MemoryStream file_stream, string path)
        {
            //dynamic file_content = "";
            ImportResponseVm succesCount = new ImportResponseVm();
            Int64 lmt = Convert.ToInt64(last_modified_date_time);
            List<import_course> sheet = new List<import_course>();
            using (IDbConnection conn = Connection)
            {
                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(file_stream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    int colCount = workSheet.Range("A1:D1").ColumnsUsed().Count();
                    int rowCount = workSheet.Rows().Count();
                    //Loop through the Worksheet rows.

                    int rowNum = 1;
                    rowNum++;
                    if (rowCount <= 1)
                    {
                        succesCount.message = "Excel is empty";
                        succesCount.status = false;
                    }
                    else if (colCount < 4 || colCount > 4)
                    {
                        succesCount.message = "Invalid file";
                        succesCount.status = false;
                    }
                    else
                    {
                        for (int i = 2; i <= rowCount; i++)
                        {
                            if (!string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 1).Value + "")).Trim()) ||
                                    !string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 2).Value + "")).Trim()))
                            {
                                import_course objloc = new import_course();
                                for (int j = 1; j <= colCount; j++)
                                {
                                    switch (CommonExtensions.ToString(workSheet.Cell(1, j).Value).Trim().ToLower())
                                    {
                                        case "coursecode":
                                            objloc.course_code = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "coursename":
                                            objloc.course_name = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "imagepath":
                                            objloc.image_path = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "isactive":
                                            objloc.is_activce = CommonExtensions.ToInteger(workSheet.Cell(i, j).Value);
                                            break;
                                    }
                                }
                                objloc.index_no = i - 1;
                                sheet.Add(objloc);
                            }
                        }
                        if (sheet != null && sheet.Count > 0)
                        {
                            IEnumerable<ImportCourseList> resultImport = null;
                            (IEnumerable<import_course>, import_response) BrandList = await _import.ImportCourse(conn, sheet.ToJsonString<IEnumerable<import_course>>(false));
                            resultImport = _mapper.Map<IEnumerable<ImportCourseList>>(BrandList.Item1);
                            succesCount = _mapper.Map<ImportResponseVm>(BrandList.Item2);
                            if (resultImport != null)
                            {
                                //orderbysalesman = obj_order.GroupBy(p => p.salesman_code).Select(g => g.First());
                                using (var workbook = new XLWorkbook())
                                {
                                    var worksheet = workbook.Worksheets.Add("SampleCourse");
                                    //--------
                                    worksheet.Rows("1").Style.Fill.BackgroundColor = XLColor.BlueGreen;
                                    worksheet.Rows("1").Style.Font.Bold = true;
                                    worksheet.Columns("1,2,3,4,5,6").Width = 20;
                                    worksheet.Columns("2").Width = 40;
                                    //----------------
                                    var currentRow = 1;
                                    worksheet.Cell(currentRow, 1).Value = "CourseCode";
                                    worksheet.Cell(currentRow, 2).Value = "CourseName";
                                    worksheet.Cell(currentRow, 3).Value = "ImagePath";
                                    worksheet.Cell(currentRow, 4).Value = "IsActive";
                                    worksheet.Cell(currentRow, 5).Value = "status";
                                    worksheet.Cell(currentRow, 6).Value = "description";
                                    foreach (var course in resultImport)
                                    {
                                        currentRow++;
                                        worksheet.Cell(currentRow, 1).Value = course.course_code;
                                        worksheet.Cell(currentRow, 2).Value = course.course_name;
                                        worksheet.Cell(currentRow, 3).Value = course.image_path;
                                        worksheet.Cell(currentRow, 4).Value = course.is_activce;
                                        worksheet.Cell(currentRow, 5).Value = course.status;
                                        worksheet.Cell(currentRow, 6).Value = course.status_message;
                                    }
                                    workbook.SaveAs(path);
                                    succesCount.file_path = path;
                                    succesCount.status = true;
                                    succesCount.message = "Success";
                                }
                            }
                            else
                            {
                                succesCount.status = false;
                                succesCount.message = "Failure";
                                return succesCount;
                            }
                        }
                    }
                    return succesCount;
                }
            }
        }
        public async Task<ImportResponseVm> ImportSubject(MemoryStream file_stream, string path)
        {
            //dynamic file_content = "";
            ImportResponseVm succesCount = new ImportResponseVm();
            Int64 lmt = Convert.ToInt64(last_modified_date_time);
            List<import_course> sheet = new List<import_course>();
            using (IDbConnection conn = Connection)
            {
                //Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(file_stream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    int colCount = workSheet.Range("A1:D1").ColumnsUsed().Count();
                    int rowCount = workSheet.Rows().Count();
                    //Loop through the Worksheet rows.

                    int rowNum = 1;
                    rowNum++;
                    if (rowCount <= 1)
                    {
                        succesCount.message = "Excel is empty";
                        succesCount.status = false;
                    }
                    else if (colCount < 4 || colCount > 4)
                    {
                        succesCount.message = "Invalid file";
                        succesCount.status = false;
                    }
                    else
                    {
                        for (int i = 2; i <= rowCount; i++)
                        {
                            if (!string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 1).Value + "")).Trim()) ||
                                    !string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 2).Value + "")).Trim()))
                            {
                                import_course objloc = new import_course();
                                for (int j = 1; j <= colCount; j++)
                                {
                                    switch (CommonExtensions.ToString(workSheet.Cell(1, j).Value).Trim().ToLower())
                                    {
                                        case "coursecode":
                                            objloc.course_code = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "coursename":
                                            objloc.course_name = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "imagepath":
                                            objloc.image_path = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                            break;
                                        case "isactive":
                                            objloc.is_activce = CommonExtensions.ToInteger(workSheet.Cell(i, j).Value);
                                            break;
                                    }
                                }
                                objloc.index_no = i - 1;
                                sheet.Add(objloc);
                            }
                        }
                        if (sheet != null && sheet.Count > 0)
                        {
                            IEnumerable<ImportCourseList> resultImport = null;
                            (IEnumerable<import_course>, import_response) BrandList = await _import.ImportCourse(conn, sheet.ToJsonString<IEnumerable<import_course>>(false));
                            resultImport = _mapper.Map<IEnumerable<ImportCourseList>>(BrandList.Item1);
                            succesCount = _mapper.Map<ImportResponseVm>(BrandList.Item2);
                            if (resultImport != null)
                            {
                                //orderbysalesman = obj_order.GroupBy(p => p.salesman_code).Select(g => g.First());
                                using (var workbook = new XLWorkbook())
                                {
                                    var worksheet = workbook.Worksheets.Add("SampleCourse");
                                    //--------
                                    worksheet.Rows("1").Style.Fill.BackgroundColor = XLColor.BlueGreen;
                                    worksheet.Rows("1").Style.Font.Bold = true;
                                    worksheet.Columns("1,2,3,4,5,6").Width = 20;
                                    worksheet.Columns("2").Width = 40;
                                    //----------------
                                    var currentRow = 1;
                                    worksheet.Cell(currentRow, 1).Value = "CourseCode";
                                    worksheet.Cell(currentRow, 2).Value = "CourseName";
                                    worksheet.Cell(currentRow, 3).Value = "ImagePath";
                                    worksheet.Cell(currentRow, 4).Value = "IsActive";
                                    worksheet.Cell(currentRow, 5).Value = "status";
                                    worksheet.Cell(currentRow, 6).Value = "description";
                                    foreach (var course in resultImport)
                                    {
                                        currentRow++;
                                        worksheet.Cell(currentRow, 1).Value = course.course_code;
                                        worksheet.Cell(currentRow, 2).Value = course.course_name;
                                        worksheet.Cell(currentRow, 3).Value = course.image_path;
                                        worksheet.Cell(currentRow, 4).Value = course.is_activce;
                                        worksheet.Cell(currentRow, 5).Value = course.status;
                                        worksheet.Cell(currentRow, 6).Value = course.status_message;
                                    }
                                    workbook.SaveAs(path);
                                    succesCount.file_path = path;
                                    succesCount.status = true;
                                    succesCount.message = "Success";
                                }
                            }
                            else
                            {
                                succesCount.status = false;
                                succesCount.message = "Failure";
                                return succesCount;
                            }
                        }
                    }
                    return succesCount;
                }
            }
        }
        public async Task<ImportResponseVm> ImportMCQ(MemoryStream file_stream, string path, string unit_id, int lession_id)
        {
            //dynamic file_content = "";
            ImportResponseVm succesCount = new ImportResponseVm();
            Int64 lmt = Convert.ToInt64(last_modified_date_time);
            List<import_mcq> sheet = new List<import_mcq>();
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                using (IDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        //Open the Excel file using ClosedXML.
                        using (XLWorkbook workBook = new XLWorkbook(file_stream))
                        {
                            //Read the first Sheet from Excel file.
                            IXLWorksheet workSheet = workBook.Worksheet(1);
                            int colCount = workSheet.Range("A1:D1").ColumnsUsed().Count();
                            int rowCount = workSheet.Rows().Count();
                            //Loop through the Worksheet rows.

                            int rowNum = 1;
                            rowNum++;
                            if (rowCount <= 1)
                            {
                                succesCount.message = "Excel is empty";
                                succesCount.status = false;
                            }
                            else if (colCount < 4 || colCount > 4)
                            {
                                succesCount.message = "Invalid file";
                                succesCount.status = false;
                            }
                            else
                            {
                                for (int i = 2; i <= rowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 1).Value + "")).Trim()) ||
                                            !string.IsNullOrEmpty((CommonExtensions.ToString(workSheet.Cell(i, 2).Value + "")).Trim()))
                                    {
                                        import_mcq objloc = new import_mcq();
                                        for (int j = 1; j <= colCount; j++)
                                        {
                                            switch (CommonExtensions.ToString(workSheet.Cell(1, j).Value).Trim().ToLower())
                                            {
                                                case "question":
                                                    objloc.question = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                                    objloc.lession_id = lession_id;
                                                    break;
                                                case "option":
                                                    objloc.option = CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim();
                                                    break;
                                                case "isanswer":
                                                    objloc.is_answer = (CommonExtensions.ToString(workSheet.Cell(i, j).Value + "").Trim().ToLower() == "true") ? 1 : 0;
                                                    break;
                                                case "sequence":
                                                    objloc.sequence = CommonExtensions.ToInteger(workSheet.Cell(i, j).Value);
                                                    break;
                                            }
                                        }
                                        objloc.index_no = i - 1;
                                        sheet.Add(objloc);
                                    }
                                }
                                if (sheet != null && sheet.Count > 0)
                                {
                                    IEnumerable<ImportMcqList> resultImport = null;
                                    (IEnumerable<import_mcq>, import_response) BrandList = await _import.ImportMCQ(conn, tran, sheet.ToJsonString<IEnumerable<import_mcq>>(false), unit_id, lession_id);
                                    resultImport = _mapper.Map<IEnumerable<ImportMcqList>>(BrandList.Item1);
                                    succesCount = _mapper.Map<ImportResponseVm>(BrandList.Item2);
                                    if (resultImport != null)
                                    {
                                        //orderbysalesman = obj_order.GroupBy(p => p.salesman_code).Select(g => g.First());
                                        using (var workbook = new XLWorkbook())
                                        {
                                            var worksheet = workbook.Worksheets.Add("SampleCourse");
                                            //--------
                                            worksheet.Rows("1").Style.Fill.BackgroundColor = XLColor.BlueGreen;
                                            worksheet.Rows("1").Style.Font.Bold = true;
                                            worksheet.Columns("1,2,3,4,5,6").Width = 20;
                                            worksheet.Columns("2").Width = 40;
                                            //----------------
                                            var currentRow = 1;
                                            worksheet.Cell(currentRow, 1).Value = "Question";
                                            worksheet.Cell(currentRow, 2).Value = "Option";
                                            worksheet.Cell(currentRow, 3).Value = "IsAnswer";
                                            worksheet.Cell(currentRow, 4).Value = "Sequence";
                                            worksheet.Cell(currentRow, 5).Value = "status";
                                            worksheet.Cell(currentRow, 6).Value = "description";
                                            foreach (var course in resultImport)
                                            {
                                                currentRow++;
                                                worksheet.Cell(currentRow, 1).Value = course.question;
                                                worksheet.Cell(currentRow, 2).Value = course.option;
                                                worksheet.Cell(currentRow, 3).Value = course.is_answer;
                                                worksheet.Cell(currentRow, 4).Value = course.sequence;
                                                worksheet.Cell(currentRow, 5).Value = course.status;
                                                worksheet.Cell(currentRow, 6).Value = course.status_message;
                                            }
                                            workbook.SaveAs(path);
                                            succesCount.file_path = path;
                                            succesCount.status = true;
                                            succesCount.message = "Success";
                                            tran.Commit();
                                            conn.Close();
                                        }
                                    }
                                    else
                                    {
                                        succesCount.status = false;
                                        succesCount.message = "Failure";
                                        tran.Rollback();
                                        return succesCount;
                                    }
                                }
                            }
                            return succesCount;
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
        }
    }
}
