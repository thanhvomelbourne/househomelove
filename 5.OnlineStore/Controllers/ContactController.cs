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
    public class ContactController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContactController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult SubmitMessage(ContactModel model)
        {
            var data = _service.Contact.Insert(model);

            return Json(new { success = data != null }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult Details(int? contactId)
        {
            if (contactId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            var id = contactId.Value;

            var data = _service.Contact.Details(id);

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? contactIdForDelete)
        {
            try
            {
                if (contactIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = contactIdForDelete.Value;

                _service.Contact.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The contact message is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the contact message!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}