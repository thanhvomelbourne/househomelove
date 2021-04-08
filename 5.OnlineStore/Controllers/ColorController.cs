using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using System;
using System.Net;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ColorController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ColorController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult GetAllColors()
        {
            try
            {
                var colorList = _service.Color.GetAllColors();

                return colorList != null ? Json(new { success = true, data = colorList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(ColorModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    _service.Color.Insert(model);
                    TempData["message"] = "The color is created successfully!";
                }
                else
                {
                    _service.Color.Update(model);
                    TempData["message"] = "The color is updated successfully!";
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
        public JsonResult Details(int? colorId)
        {
            try
            {
                if (colorId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                var id = colorId.Value;

                var data = _service.Color.Details(id);

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult CheckValidate(string colorName)
        {
            try
            {
                var data = _service.Color.CheckValidate(colorName);

                return Json(new { success = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? colorIdForDelete)
        {
            try
            {
                if (colorIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = colorIdForDelete.Value;

                _service.Color.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The color is deleted successfully!";
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