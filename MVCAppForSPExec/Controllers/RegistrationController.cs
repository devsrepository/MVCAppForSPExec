using MVCAppForSPExec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVCAppForSPExec.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult List()
        {
            return View();
        }

        public ActionResult ListByJoins()
        {
            return View();
        }
        public ActionResult DetailIndex(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Details(string name=null)
        {
            return View();
        }
        //to get records from db nad show on view. i.e. get request
        public ActionResult GetRegistrationList()
        {
            try
            {
                return Json(new { model = (new RegistrationModel().GetRegistrationList()) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //get list by joining 3 tables.
        public ActionResult GetRegListWithCityState()
        {
            try
            {
                return Json(new { model = (new RegistrationModel().GetRegListWithCityState()) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        //to insert data into db i.e. post request .here this method is for the model where there is no photo.
        //public ActionResult SaveRegistration(RegistrationModel model)
        //{
        //    try
        //    {
        //        return Json(new { model = (new RegistrationModel().SaveRegistration(model)) },
        //        JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult SaveRegistration(RegistrationModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[0];
                  
                }
                return Json(new { model = (new RegistrationModel().SaveRegistration(fb,model)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteRegistrationRow(int id)
        {
            try
            {
                return Json(new { model = (new RegistrationModel().DeleteRegistrationRow(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditRegistrationRecord(int id)
        {
            try
            {
                return Json(new { model = (new RegistrationModel().EditRegistrationRecord(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRegistrationListById(int id)
        {
            try
            {
                return Json(new { model = (new RegistrationModel().GetRegListById(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}