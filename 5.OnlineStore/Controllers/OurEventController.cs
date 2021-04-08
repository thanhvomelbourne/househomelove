using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class OurEventController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OurEventController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult GetAllOurEvents()
        {
            try
            {
                var eventList = _service.OurEvent.GetAllOurEvents();

                return eventList != null ? Json(new { success = true, data = eventList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(OurEventModel model)
        {
            try
            {
                model.IsLive = (Request.Form["IsLive"] == "on") ? true : false;

                if (model.EventImage != null)
                {
                    try
                    {
                        string eventFileName = $"Event-{DateTime.UtcNow.ToBinary()}-{model.EventImage.FileName}";
                        eventFileName = UtilityHelper.RemoveSpecialCharacters(eventFileName);
                        string eventPath = Path.Combine(Server.MapPath(Constant.Path.EventImagePath), eventFileName);

                        model.EventImage.SaveAs(eventPath);

                        model.Avatar = Constant.Path.EventImagePath.Replace("\\", "/") + eventFileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (model.Id == 0)
                {
                    _service.OurEvent.Insert(model);
                    TempData["message"] = "The event is created successfully!";
                }
                else
                {
                    _service.OurEvent.Update(model);
                    TempData["message"] = "The event is updated successfully!";
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
        public JsonResult Details(int? eventId)
        {
            try
            {
                if (eventId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                var id = eventId.Value;

                var data = _service.OurEvent.Details(id);

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
        public ActionResult Delete(int? eventIdForDelete)
        {
            try
            {
                if (eventIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = eventIdForDelete.Value;

                _service.OurEvent.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The event is deleted successfully!";
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
    }
}