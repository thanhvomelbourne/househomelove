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
    public class NewsletterTemplateController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NewsletterTemplateController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllNewsletterTemplate()
        {
            try
            {
                var tempList = _service.NewsletterTemplate.GetAllNewsletterTemplates();

                return tempList != null ? Json(new { success = true, data = tempList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(NewsletterTemplateModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    _service.NewsletterTemplate.Insert(model);
                    TempData["message"] = "The newsletter template is created successfully!";
                }
                else
                {
                    _service.NewsletterTemplate.Update(model);
                    TempData["message"] = "The newsletter template is updated successfully!";
                }

                TempData["messageClass"] = "alert-success";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = ex.Message;
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        public JsonResult Details(int? templateId)
        {
            try
            {
                if (templateId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                var id = templateId.Value;

                var data = _service.NewsletterTemplate.Details(id);

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? templateIdForDelete)
        {
            try
            {
                if (templateIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = templateIdForDelete.Value;

                _service.NewsletterTemplate.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The category is deleted successfully!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = ex.Message;
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult SendNewsletterToPerson(string toEmail, int? templateId)
        {
            try
            {
                if (templateId == null || string.IsNullOrEmpty(toEmail))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = templateId.Value;

                bool result = _service.NewsletterTemplate.SendNewsletterToPerson(toEmail, id);

                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}