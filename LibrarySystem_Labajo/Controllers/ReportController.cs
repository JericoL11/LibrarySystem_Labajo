using LibrarySystem_Labajo.Data;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem_Labajo.DataWrapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem_Labajo.Controllers
{
    public class ReportController : Controller
    {
        private readonly LibrarySystem_LabajoContext _context;

        public ReportController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetReport(DateTime dateFrom,DateTime dateTo)
        {
            var param1Value = new SqlParameter("@dateFrom", dateFrom);
            var param2Value = new SqlParameter("@dateTo", dateTo);

            List<ReportWR> result = _context.Set<ReportWR>().FromSqlRaw
                ("SP_GraphReturn @dateFrom, @dateTo", param1Value,param2Value).ToList();

            return Json(new { dateResult = result });
        }
    }
}
