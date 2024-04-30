using Abp.Web.Mvc.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       

        public IActionResult ExcelFileReader()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExcelFileReader(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if(file !=null && file.Length > 0)
            {
                var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\UploadExcel";

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath =Path.Combine(uploadDirectory,file.FileName);

                using(var stream = new FileStream(filePath,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var excelData = new List<List<object>>();   
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        
                        do
                        {
                            while (reader.Read())
                            {
                                var rowData = new List<object>();
                                for (int column=0; column < reader.FieldCount; column++)
                                {
                                    rowData.Add(reader.GetValue(column));
                                }
                                excelData.Add(rowData);
                            }
                        } while (reader.NextResult());

                        ViewBag.excelData = excelData;

                    }
                }

            }

            return View();
        }
    }
}
