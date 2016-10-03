using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMONE.Filters;
using GYMONE.Models;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
  
    [MyExceptionHandler]
    [Authorize(Roles = "SystemUser")]
    public class RenewalController : Controller
    {
        ISchemeMaster objscheme;
        IPlanMaster objPlan;
        IRegisterMember objRegMem;
        IPaymentDetails objpay;
        IPaymentlisting objIPaymentlisting;
        IRenewal objRenewal;

        public RenewalController()
        {
            objscheme = new SchemeMaster();
            objPlan = new PlanMaster();
            objRegMem = new RegisterMember();
            objpay = new PaymentDetails();
            objIPaymentlisting = new Paymentlisting();
            objRenewal = new Renewal();
        }

        [HttpGet]
        public ActionResult RenewalDetails()
        {
            TempData["Message"] = null;
            return View(new RenewalDTO());
        }

        public List<SelectListItem> BindRenewalType()
        {
            List<SelectListItem> ListBindRenewal = new List<SelectListItem>()
                {
                    new SelectListItem{ Text="Select" ,Value="0" ,Selected =true },
                    new SelectListItem{ Text="Continues" ,Value="1" ,Selected =true },
                    new SelectListItem{ Text="Gap" ,Value="2" ,Selected =true },
                };

            return ListBindRenewal;

        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<SchemeMasterDTO> BindListScheme()
        {
            List<SchemeMasterDTO> listscheme = new List<SchemeMasterDTO>() 
            { new 
            SchemeMasterDTO 
            { SchemeID = 0,
                SchemeName = "Select" } };

            foreach (var item in objscheme.GetSchemes())
            {
                SchemeMasterDTO sm = new SchemeMasterDTO();
                sm.SchemeID = item.SchemeID;
                sm.SchemeName = item.SchemeName;
                listscheme.Add(sm);
            }
            return listscheme;

        }

        [NonAction]
        public List<PlanMasterDTO> BindListPlan()
        {
            List<PlanMasterDTO>
            ListPlan = new List<PlanMasterDTO>() { new PlanMasterDTO { PlanID = 0, PlanName = "Select" } };

            foreach (var item in objPlan.GetPlan())
            {
                PlanMasterDTO pm = new PlanMasterDTO();
                pm.PlanID = item.PlanID;
                pm.PlanName = item.PlanName;
                ListPlan.Add(pm);
            }


            return ListPlan;
        }

        public JsonResult GetPlan(string WorkTypeID)
        {
            var plandata = objPlan.GetPlanByWorkTypeID(WorkTypeID);
            return Json(plandata, JsonRequestBehavior.AllowGet);
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

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult RenewalDetails(RenewalDTO obj, RenewalDATA dto, string actionType)
        {
            RenewalDTO objRM = new RenewalDTO();
            RenewalDATA d = new RenewalDATA();

            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string[] date = dt.ToString("yyyy/MM/dd").Split('/');
            int month = Convert.ToInt32(date[1]);
            int day = Convert.ToInt32(date[2]);
            int year = Convert.ToInt32(date[0]);
            DateTime Newdt = new DateTime(year, month, day);

            if (actionType == "Cancel")
            {
                RedirectToAction("RenewalDetails", "Renewal");
            }
            else if (actionType == "Search")
            {

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
                    var Mainno = "";


                    if (!string.IsNullOrEmpty(obj.MemberName))
                    {
                        string[] Memno = obj.MemberName.Split('|');
                        Mainno = Memno[3];
                    }
                    else if (!string.IsNullOrEmpty(obj.MembernoSearch))
                    {
                        Mainno = obj.MembernoSearch;
                    }

                    var list = objRenewal.GetDataofMember(Mainno);
                    d.ListRenewaltype = BindRenewalType();
                    d.ListScheme = BindListScheme();
                    d.ListPlan = null;
                    d.RenwalDate = list.RenwalDate;
                    d.WorkouttypeID = list.WorkouttypeID;
                    d.PlantypeID = list.PlantypeID.ToString();
                    d.PaymentAmount = list.PaymentAmount;
                    d.MemberNo = list.MemberNo;
                    d.MainMemberID = list.MainMemberID;
                    d.Name = list.Name;
                    objRM.PaymentSearch = obj.PaymentSearch;
                    objRM.MemberName = obj.MemberName;
                    objRM.MembernoSearch = obj.MembernoSearch;
                    d.MemberID = list.MemberID;
                    d.PaymentID = list.PaymentID;

                    objRM.RenewalDATA = d;
                    ViewData["SelectedPlan"] = d.PlantypeID;

                    return View(objRM);

                }
            }
            else
            {

                if (!string.IsNullOrEmpty(dto.DrpRenewal) && dto.DrpRenewal == "1")
                {
                    RenewalDTO objRM1 = new RenewalDTO();
                    Pay(dto);
                    ModelState.Clear();
                    return View(new RenewalDTO());
                }
                if (!string.IsNullOrEmpty(dto.DrpRenewal) && dto.DrpRenewal == "2")
                {
                    Pay_Gap(dto);
                    ModelState.Clear();
                    return View(new RenewalDTO());
                }
            }




            return View(obj);
        }

        [NonAction]
        public int Pay(RenewalDATA obj)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);
                int year = Convert.ToInt32(date[0]);
                DateTime Newdt = new DateTime(year, month, day);

                DateTime joiningDT = DateTime.Now;
                PaymentDetailsDTO PayPD = new PaymentDetailsDTO();
                PayPD.PaymentID = 0;
                PayPD.PlanID = Convert.ToInt32(obj.PlantypeID);
                PayPD.WorkouttypeID = Convert.ToInt32(obj.WorkouttypeID);
                PayPD.Paymenttype = "Cash";

                string[] PFFromdate = obj.ContinuesDate.Split('-');
                int yearPD = Convert.ToInt32(PFFromdate[0]);
                int monthPD = Convert.ToInt32(PFFromdate[1]);
                int dayPD = Convert.ToInt32(PFFromdate[2]);
                joiningDT = new DateTime(yearPD, monthPD, dayPD);
                PayPD.PaymentFromdt = joiningDT;
                int PDID = Convert.ToInt32(objRenewal.Get_PeriodID_byPlan(obj.PlantypeID));

                PayPD.PaymentTodt = joiningDT.AddMonths(PDID).AddDays(-1);
                PayPD.PaymentAmount = Convert.ToDecimal(obj.PaymentAmount);
                PayPD.NextRenwalDate = joiningDT.AddMonths(PDID).AddDays(-1);
                PayPD.CreateDate = Newdt;
                PayPD.CreateUserID = Convert.ToInt32(Session["UserID"]);
                PayPD.ModifyUserID = 0;
                PayPD.ModifyDate = Newdt;

                PayPD.RecStatus = "A";
                PayPD.MemberID = Convert.ToInt32(obj.MemberID);
                PayPD.MemberNo = obj.MemberNo;

                int payresult = objpay.InsertPaymentDetails(PayPD);


                TempData["Message"] = "Renewal Done Successfully";

                return payresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [NonAction]
        public int Pay_Gap(RenewalDATA obj)
        {
            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);
                int year = Convert.ToInt32(date[0]);
                DateTime Newdt = new DateTime(year, month, day);

                DateTime joiningDT = DateTime.Now;
                PaymentDetailsDTO PayPD = new PaymentDetailsDTO();
                PayPD.PaymentID = 0;
                PayPD.PlanID = Convert.ToInt32(obj.PlantypeID);
                PayPD.WorkouttypeID = Convert.ToInt32(obj.WorkouttypeID);
                PayPD.Paymenttype = "Cash";

                string[] PFFromdate = obj.NewDate.Split('-');
                int yearPD = Convert.ToInt32(PFFromdate[0]);
                int monthPD = Convert.ToInt32(PFFromdate[1]);
                int dayPD = Convert.ToInt32(PFFromdate[2]);
                joiningDT = new DateTime(yearPD, monthPD, dayPD);
                PayPD.PaymentFromdt = joiningDT;
                int PDID = Convert.ToInt32(objRenewal.Get_PeriodID_byPlan(obj.PlantypeID));

                PayPD.PaymentTodt = joiningDT.AddMonths(PDID).AddDays(-1);
                PayPD.PaymentAmount = Convert.ToDecimal(obj.PaymentAmount);
                PayPD.NextRenwalDate = joiningDT.AddMonths(PDID).AddDays(-1);
                PayPD.CreateDate = Newdt;
                PayPD.CreateUserID = Convert.ToInt32(Session["UserID"]);
                PayPD.ModifyUserID = 0;
                PayPD.ModifyDate = Newdt;

                PayPD.RecStatus = "A";
                PayPD.MemberID = Convert.ToInt32(obj.MemberID);
                PayPD.MemberNo = obj.MemberNo;

                int payresult = objpay.InsertPaymentDetails(PayPD);


                TempData["Message"] = "Renewal Done Successfully";

                return payresult;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult GetAmount(string PlanID, string WorkTypeID)
        {
            var amount = objRegMem.GetAmount(PlanID, WorkTypeID);
            return Json(amount, JsonRequestBehavior.AllowGet);
        }
    }
}
