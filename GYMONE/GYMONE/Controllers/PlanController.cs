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
    [Authorize(Roles = "Admin")]
    public class PlanController : Controller
    {
        IPlanMaster objIPlanMaster;
        ISchemeMaster objscheme;


        public PlanController()
        {
            objIPlanMaster = new PlanMaster();
            objscheme = new SchemeMaster();
        }

        public ActionResult Index()
        {
            return View(objIPlanMaster.GetPlan());
        }

        //
        // GET: /Plan/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Plan/Create

        [HttpGet]
        public ActionResult Create()
        {
            PlanMasterDTO objPM = new PlanMasterDTO();

            List<SchemeMasterDTO> listscheme = new List<SchemeMasterDTO>();
            listscheme = new List<SchemeMasterDTO>()
            {
            new SchemeMasterDTO { SchemeID = 0, SchemeName ="Select"}
            };



            foreach (var item in objscheme.GetSchemes())
            {
                SchemeMasterDTO sm = new SchemeMasterDTO();
                sm.SchemeID = item.SchemeID;
                sm.SchemeName = item.SchemeName;

                listscheme.Add(sm);
            }


            objPM.ListScheme = listscheme;

            objPM.ListofPeriod = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Select" , Selected =true, Value ="0"  },
                new SelectListItem { Text="3 Month" , Selected =false, Value ="3"  },
                new SelectListItem { Text="6 Month" , Selected =false, Value ="6"  },
                new SelectListItem { Text="1 Year" , Selected =false, Value ="12"  },
            };


            return View(objPM);
        }

        //
        // POST: /Plan/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanMasterDTO objplan, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objplan.SchemeID == 0)
                    {
                        ModelState.AddModelError("SchemeMessage", "Please select Scheme Name");
                        Method(objplan);
                        return View(objplan);
                    }
                    else if (objplan.PeriodID == 0)
                    {
                        ModelState.AddModelError("PeriodMessage", "Please select Period Name");
                        Method(objplan);
                        return View(objplan);
                    }
                    else
                    {


                        objplan.PlanID = 0;
                        objplan.CreateUserID = Convert.ToInt32(Session["UserID"]);

                        DateTime dt = new DateTime();
                        dt = DateTime.Now;
                        string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                        int year = Convert.ToInt32(date[0]);
                        int month = Convert.ToInt32(date[1]);
                        int day = Convert.ToInt32(date[2]);

                        DateTime Newdt = new DateTime(year, month, day);
                        objplan.CreateDate = Newdt;
                        objplan.ModifyDate = Newdt;
                        objplan.RecStatus = "A";
                        TempData["notice"] = "Plan Created Successfully";
                        objIPlanMaster.InsertPlan(objplan);
                        return RedirectToAction("Create");

                    }
                }
                else
                {
                    Method(objplan);
                    return View(objplan);
                }
            }
            catch
            {
                Method(objplan);
                return View(objplan);
            }
        }

        //
        // GET: /Plan/Edit/5

        public ActionResult Edit(int id)
        {
            var Model = objIPlanMaster.GetPlanByID(Convert.ToString(id));
            EDITMethod(Model);

            return View(Model);
        }

        //
        // POST: /Plan/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanMasterDTO objplan, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objplan.SchemeID == 0)
                        {
                            ModelState.AddModelError("SchemeMessage", "Please select Scheme Name");
                            Method(objplan);
                            return View(objplan);
                        }
                        else if (objplan.PeriodID == 0)
                        {
                            ModelState.AddModelError("PeriodMessage", "Please select Period Name");
                            Method(objplan);
                            return View(objplan);
                        }
                        else
                        {
                            objplan.CreateUserID = Convert.ToInt32(Session["UserID"]);

                            DateTime dt = new DateTime();
                            dt = DateTime.Now;
                            string[] date = dt.ToString("yyyy/MM/dd").Split('/');
                            int year = Convert.ToInt32(date[0]);
                            int month = Convert.ToInt32(date[1]);
                            int day = Convert.ToInt32(date[2]);

                            DateTime Newdt = new DateTime(year, month, day);
                            objplan.CreateDate = Newdt;
                            objplan.ModifyDate = Newdt;
                            objplan.RecStatus = "A";

                            objIPlanMaster.UpdatePlan(objplan);
                            TempData["notice"] = "Plan Updated Successfully";

                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        Method(objplan);
                        return View(objplan);
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Plan/Delete/5

        public ActionResult Delete(int id)
        {
            objIPlanMaster.DeletePlan(Convert.ToString(id));
            return RedirectToAction("Index");
        }

        //
        // POST: /Plan/Delete/5



        public ActionResult PlannameExists(string Planname)
        {
            var result = objIPlanMaster.PlannameExists(Planname);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }


        public void Method(PlanMasterDTO objplan)
        {
            ModelState.Remove("ListScheme");
            ModelState.Remove("ListofPeriod");
            List<SchemeMasterDTO> listscheme = new List<SchemeMasterDTO>();
            listscheme = new List<SchemeMasterDTO>() { new SchemeMasterDTO { SchemeID = 0, SchemeName = "Select" } };

            foreach (var item in objscheme.GetSchemes())
            {
                SchemeMasterDTO sm = new SchemeMasterDTO();
                sm.SchemeID = item.SchemeID;
                sm.SchemeName = item.SchemeName;
                listscheme.Add(sm);
            }


            objplan.ListScheme = listscheme;

            objplan.ListofPeriod = new List<SelectListItem>()
                {
                new SelectListItem{ Text="Select" , Selected =true, Value ="0"  },
                new SelectListItem { Text="3 Month" , Selected =false, Value ="3"  },
                new SelectListItem { Text="6 Month" , Selected =false, Value ="6"  },
                new SelectListItem { Text="1 Year" , Selected =false, Value ="12"  },
                };
        }

        public void EDITMethod(PlanMasterDTO objplan)
        {
            ModelState.Remove("ListScheme");
            ModelState.Remove("ListofPeriod");
            List<SchemeMasterDTO> listscheme = new List<SchemeMasterDTO>();
            listscheme = new List<SchemeMasterDTO>() { new SchemeMasterDTO { SchemeID = 0, SchemeName = "Select" } };

            foreach (var item in objscheme.GetSchemes())
            {
                SchemeMasterDTO sm = new SchemeMasterDTO();
                sm.SchemeID = item.SchemeID;
                sm.SchemeName = item.SchemeName;
                listscheme.Add(sm);
            }


            objplan.ListScheme = listscheme;
            objplan.SchemeID = objplan.SchemeID;

            objplan.ListofPeriod = new List<SelectListItem>()
                {
                new SelectListItem{ Text="Select" , Selected =true, Value ="0"  },
                new SelectListItem { Text="3 Month" , Selected =false, Value ="3"  },
                new SelectListItem { Text="6 Month" , Selected =false, Value ="6"  },
                new SelectListItem { Text="1 Year" , Selected =false, Value ="12"  },
                };

            objplan.PeriodID = objplan.PeriodID;
        }
    }
}
