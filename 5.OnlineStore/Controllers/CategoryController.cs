using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using System;
using System.Net;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CategoryController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            try
            {
                var cateList = _service.Category.GetAllCategories();

                return cateList != null ? Json(new { success = true, data = cateList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllCategoriesForAdmin()
        {
            try
            {
                var cateList = _service.Category.GetAllCategoriesForAdmin();

                return cateList != null ? Json(new { success = true, data = cateList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(CategoryModel model)
        {
            try
            {
                model.IsLive = (Request.Form["IsLive"] == "on") ? true : false;

                if (model.Id == 0)
                {
                    _service.Category.Insert(model);
                    TempData["message"] = "The category is created successfully!";
                }
                else
                {
                    _service.Category.Update(model);
                    TempData["message"] = "The category is updated successfully!";
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
        public JsonResult Details(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                var id = categoryId.Value;

                var data = _service.Category.Details(id);

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
        public ActionResult Delete(int? categoryIdForDelete)
        {
            try
            {
                if (categoryIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = categoryIdForDelete.Value;

                _service.Category.Delete(id);

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
    }
}