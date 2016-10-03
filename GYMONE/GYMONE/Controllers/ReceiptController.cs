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
    [Authorize(Roles = "SystemUser")]
    public class ReceiptController : Controller
    {

        IPaymentlisting objIPaymentlisting;
        IReceipt objIRecepit;

        public ReceiptController()
        {
            objIPaymentlisting = new Paymentlisting();
            objIRecepit = new Receipt();
        }


        [HttpGet]
        public ActionResult Generatereceipt()
        {
            TempData["Message"] = null;
            return View(new RecepitDTO());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generatereceipt(RecepitDTO obj, string actionType)
        {
            var Mainno = "";

            if (string.Equals(actionType, "Download Receipt"))
            {

                if (!string.IsNullOrEmpty(obj.MemberName))
                {
                    string[] Memno = obj.MemberName.Split('|');
                    Mainno = Memno[3];
                }
                else if (!string.IsNullOrEmpty(obj.MembernoSearch))
                {
                    Mainno = obj.MembernoSearch;
                }

                if (obj.PaymentSearch == "1" && string.IsNullOrEmpty(obj.MembernoSearch))
                {
                    ModelState.AddModelError("Error", "Please enter Member No !");
                }
                else if (obj.PaymentSearch == "2" && string.IsNullOrEmpty(obj.MemberName))
                {
                    ModelState.AddModelError("Error", "Please enter Member Name !");
                }
                else
                {
                    DataSet ds = objIRecepit.GenerateRecepitDataset(Mainno);
                    ds.Tables[0].TableName = "RecepitDataset";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportClass rptH = new ReportClass();
                        rptH.FileName = Server.MapPath("~/Reports/Recepit.rpt");
                        rptH.Load();
                        rptH.SetDataSource(ds.Tables[0]);
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/pdf", "Recepit.pdf");
                    }
                }
            }
            else if (string.Equals(actionType, "Download Candidate Form"))
            {

                if (!string.IsNullOrEmpty(obj.MemberName))
                {
                    string[] Memno = obj.MemberName.Split('|');
                    Mainno = Memno[3];
                }
                else if (!string.IsNullOrEmpty(obj.MembernoSearch))
                {
                    Mainno = obj.MembernoSearch;
                }

                if (obj.PaymentSearch == "1" && string.IsNullOrEmpty(obj.MembernoSearch))
                {
                    ModelState.AddModelError("Error", "Please enter Member No !");
                }
                else if (obj.PaymentSearch == "2" && string.IsNullOrEmpty(obj.MemberName))
                {
                    ModelState.AddModelError("Error", "Please enter Member Name !");
                }
                else
                {

                    DataSet ds = objIRecepit.GenerateDeclarationDataset(Mainno);
                    ds.Tables[0].TableName = "DsDeclaration";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportClass rptH = new ReportClass();
                        rptH.FileName = Server.MapPath("~/Reports/DetailsForm.rpt");
                        rptH.Load();
                        rptH.SetDataSource(ds.Tables[0]);
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/pdf", "Declaration.pdf");
                    }


                }
            }
            return View(obj);
        }


        public JsonResult GetDatabyMemberNo(string term)
        {
            var list = objIPaymentlisting.ListofMemberNo(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDatabyMemberName(string term)
        {
            var list = objIPaymentlisting.ListofMemberName(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
