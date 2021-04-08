using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class EVoucherController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EVoucherController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult CheckBalance(string serialNo, string activationCode)
        {
            if (string.IsNullOrEmpty(serialNo) || string.IsNullOrEmpty(activationCode))
            {
                return Json(new { success = false, message = "Cannot check your evoucher's balance! Please enter your correct evoucher information before checking balance!" }, JsonRequestBehavior.AllowGet);
            }

            var data = _service.EVoucher.CheckBalance(serialNo, activationCode);

            if(data == null)
            {
                return Json(new { success = false, message = "Cannot check your evoucher's balance! Please enter your correct evoucher information before checking balance!" }, JsonRequestBehavior.AllowGet);
            }

            return !data.IsLive ? Json(new { success = false, message = "Your evoucher is already expired!" }, JsonRequestBehavior.AllowGet) : Json(new { success = true, data = data.Balance }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Details(int? evoucherId)
        {
            if (evoucherId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            var id = evoucherId.Value;

            var data = _service.EVoucher.Details(id);

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(EVoucherModel model)
        {
            try
            {
                model.IsLive = (Request.Form["IsLive"] == "on") ? true : false;
                model.IsGift = (Request.Form["IsGift"] == "on") ? true : false;

                if (model.Id == 0)
                {
                    _service.EVoucher.Insert(model);
                    TempData["message"] = "The evoucher is created successfully!";
                }
                else
                {
                    _service.EVoucher.Update(model);
                    TempData["message"] = "The evoucher is updated successfully!";
                }

                TempData["messageClass"] = "alert-success";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot create/update the evoucher!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? evoucherIdForDelete)
        {
            try
            {
                if (evoucherIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int id = evoucherIdForDelete.Value;

                _service.EVoucher.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The evoucher is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the evoucher!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}