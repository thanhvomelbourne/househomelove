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
    public class SubcriptionController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SubcriptionController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        public JsonResult Subcribe(SubcriptionModel model)
        {
            if (string.IsNullOrEmpty(model.EmailSubcribed))
            {
                return Json(new { success = false, error = "Your email cannot be empty!" }, JsonRequestBehavior.AllowGet);
            }

            if (_service.Subcription.IsSubcribed(model.EmailSubcribed))
            {
                return Json(new { success = false, error = "Your email is subcribed already!" }, JsonRequestBehavior.AllowGet);
            }

            _service.Subcription.Insert(model);

            return Json(new { success = true, message = "Your email has been subcribed sucessfully!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? subcriptionIdForDelete)
        {
            try
            {
                if (subcriptionIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = subcriptionIdForDelete.Value;

                _service.Subcription.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The subscription is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the subscription!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}