using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using GYMONE.Filters;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
    [MyExceptionHandler]
    [Authorize(Roles = "SystemUser,Admin")]
    public class DetailsReportController : Controller
    {
        IlReports objIlReports;

        public DetailsReportController()
        {
            objIlReports = new ReportsMaster();
        }

        [HttpGet]
        public ActionResult DetailsMemberReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsMemberReport(FormCollection fc)
        {
            DataSet ds = objIlReports.Generate_AllMemberDetailsReport();
            ds.Tables[0].TableName = "DtMember";

            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportClass rptH = new ReportClass();
                rptH.FileName = Server.MapPath("~/Reports/AllMemberDetailsReport.rpt");
                rptH.Load();
                rptH.SetDataSource(ds.Tables[0]);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "DetailsReport.pdf");
            }

            return View();
        }


    }
}
