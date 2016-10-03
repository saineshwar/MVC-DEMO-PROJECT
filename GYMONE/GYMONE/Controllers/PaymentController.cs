using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMONE.Filters;
using GYMONE.Models;
using GYMONE.Repository;
using PagedList;
namespace GYMONE.Controllers
{
    [HandleError()]
    [MyExceptionHandler]
    [Authorize(Roles = "SystemUser,Admin")]
    public class PaymentController : Controller
    {
        const int RecordsPerPage = 10;

        IPaymentlisting objIPaymentlisting;

        public PaymentController()
        {
            objIPaymentlisting = new Paymentlisting();
        }


        public ActionResult PaymentDetails(PaymentlistingDTOVM model, string SearchButton)
        {
            PaymentlistingDTOVM dto = new PaymentlistingDTOVM();

            if (model.PaymentSearch == null)
            {
                ModelState.AddModelError("Message", "Please select type to search ( Member No / Member Name).");
            }
            else if (model.PaymentSearch == "1" && model.Memberno == null)
            {
                ModelState.AddModelError("Message", "Please Enter Member No.");
            }
            else if (model.PaymentSearch == "2" && model.MemberName == null)
            {
                ModelState.AddModelError("Message", "Please Enter Member Name.");
            }
            else
            {
                if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
                {
                    if (model.Memberno != null)
                    {
                        var Listpay = objIPaymentlisting.AllPaymentDetails(Convert.ToString(model.Memberno));

                        var results = Listpay.Where(p => (p.MemberID == model.Memberno));

                        var pageIndex = model.Page ?? 1;

                        model.SearchResults = results.ToPagedList(pageIndex, RecordsPerPage);
                    }
                    else if (model.MemberName != null)
                    {
                        string[] Memno = model.MemberName.Split('|');

                        var Listpay = objIPaymentlisting.AllPaymentDetails(Convert.ToString(Memno[3]));

                        var results = Listpay.Where(p => (p.MemberID == Convert.ToInt32(Memno[3])));

                        var pageIndex = model.Page ?? 1;

                        model.SearchResults = results.ToPagedList(pageIndex, RecordsPerPage);
                    }
                }
            }

            return View(model);
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

        public ActionResult AllMemberList()
        {
            PaymentlistingDTO dto = new PaymentlistingDTO();
            dto.PaymentList = objIPaymentlisting.AllPaymentDetails();

            return View("AllMemberList", dto);
        }
    }
}
