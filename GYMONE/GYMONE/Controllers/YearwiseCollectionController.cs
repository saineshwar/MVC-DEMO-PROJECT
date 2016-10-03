using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using GYMONE.Filters;
using GYMONE.Models;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
    
    [MyExceptionHandler]
    [Authorize(Roles = "SystemUser,Admin")]
    public class YearwiseCollectionController : Controller
    {
        IlReports objIlReports;

        public YearwiseCollectionController()
        {
            objIlReports = new ReportsMaster();
        }

        [HttpGet]
        public ActionResult YearwiseReport()
        {
            YearwiseModel objMWlist = new YearwiseModel();
            objMWlist.YearNameList = BindYear();
            return View(objMWlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YearwiseReport(FormCollection FC, YearwiseModel objMW)
        {
            YearwiseModel objMWlist = new YearwiseModel();
            objMWlist.YearNameList = BindYear();

            if (objMW.YearID == "0")
            {
                ModelState.AddModelError("Message", "Please select Year");
            }
            else
            {
                objMWlist.YearID = objMW.YearID;

                DataSet ds = objIlReports.Get_YearwisePayment_details(objMW.YearID);
                ds.Tables[0].TableName = "DSYear";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportClass rptH = new ReportClass();
                    rptH.FileName = Server.MapPath("~/Reports/YearwiseCollectionreport.rpt");
                    rptH.Load();
                    rptH.SetDataSource(ds.Tables[0]);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "YearwiseMaintenanceReport.pdf");
                }

            }

            return View(objMWlist);
        }

        [NonAction]
        public List<SelectListItem> BindYear()
        {
            List<SelectListItem> listofMonth = new List<SelectListItem>() { 
            new SelectListItem {Text ="Select", Value ="0" , Selected =true},
            new SelectListItem {Text ="2014", Value ="2015" , Selected =false},
            new SelectListItem {Text ="2015", Value ="2016" , Selected =false},
            new SelectListItem {Text ="2016", Value ="2017" , Selected =false},
            new SelectListItem {Text ="2017", Value ="2018" , Selected =false},
            new SelectListItem {Text ="2018", Value ="2019" , Selected =false},
            new SelectListItem {Text ="2019", Value ="2020" , Selected =false},
            new SelectListItem {Text ="2020", Value ="2021" , Selected =false},
            new SelectListItem {Text ="2021", Value ="2022" , Selected =false},
            new SelectListItem {Text ="2022", Value ="2023" , Selected =false},
            new SelectListItem {Text ="2023", Value ="2024" , Selected =false},
            new SelectListItem {Text ="2024", Value ="2025" , Selected =false},
            new SelectListItem {Text ="2025", Value ="2026" , Selected =false},
            };

            return listofMonth;
        }

    }
}
