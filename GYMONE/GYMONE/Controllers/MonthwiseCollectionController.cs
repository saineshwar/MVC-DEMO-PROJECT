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
    public class MonthwiseCollectionController : Controller
    {
        IlReports objIlReports;

        public MonthwiseCollectionController()
        {
            objIlReports = new ReportsMaster();
        }

        [HttpGet]
        public ActionResult MonthwiseReport()
        {
            MonthwiseModel objMWlist = new MonthwiseModel();
            objMWlist.MonthNameList = BindMonth();
            return View(objMWlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MonthwiseReport(FormCollection FC ,MonthwiseModel objMW)
        {
            MonthwiseModel objMWlist = new MonthwiseModel();
            objMWlist.MonthNameList = BindMonth();

            if (objMW.MonthID == "0")
            {
                ModelState.AddModelError("Message", "Please select Month");
            }
            else
            {

               
                objMWlist.MonthID = objMW.MonthID;

                DataSet ds = objIlReports.Get_MonthwisePayment_details(objMW.MonthID);
                ds.Tables[0].TableName = "RecepitDataset";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportClass rptH = new ReportClass();
                    rptH.FileName = Server.MapPath("~/Reports/Monthwisecollection.rpt");
                    rptH.Load();
                    rptH.SetDataSource(ds.Tables[0]);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "MonthwiseCollection.pdf");
                }

            }
        
            return View(objMWlist);
        }

        [NonAction]
        public List<SelectListItem> BindMonth()
        {
            List<SelectListItem> listofMonth = new List<SelectListItem>() { 
            new SelectListItem {Text ="Select", Value ="0" , Selected =true},
            new SelectListItem {Text ="January", Value ="1" , Selected =false},
            new SelectListItem {Text ="Febuary", Value ="2" , Selected =false},
            new SelectListItem {Text ="March", Value ="3" , Selected =false},
            new SelectListItem {Text ="April", Value ="4" , Selected =false},
            new SelectListItem {Text ="May", Value ="5" , Selected =false},
            new SelectListItem {Text ="June", Value ="6" , Selected =false},
            new SelectListItem {Text ="July", Value ="7" , Selected =false},
            new SelectListItem {Text ="August", Value ="8" , Selected =false},
            new SelectListItem {Text ="September", Value ="9" , Selected =false},
            new SelectListItem {Text ="October", Value ="10" , Selected =false},
            new SelectListItem {Text ="November", Value ="11" , Selected =false},
            new SelectListItem {Text ="December", Value ="12" , Selected =false},
            };

            return listofMonth;
        }

      
    }
}
