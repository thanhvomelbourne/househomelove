using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class GroupController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GroupController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public JsonResult GetAllGroups()
        {
            try
            {
                var cateList = _service.Group.GetAllGroups();

                return cateList != null ? Json(new { success = true, data = cateList }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllGroupType()
        {
            List<EnumModel> enums = ((Constant.GroupType[])Enum.GetValues(typeof(Constant.GroupType))).Select(c => new EnumModel() { Value = (int)c, Name = c.GetDescriptionTextForCurrentEnum() }).ToList();

            return Json(new { success = true, data = enums }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(GroupModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    _service.Group.Insert(model);
                    TempData["message"] = "The group is created successfully!";
                }
                else
                {
                    _service.Group.Update(model);
                    TempData["message"] = "The group is updated successfully!";
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
        public JsonResult Details(int? groupId)
        {
            try
            {
                if (groupId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                var id = groupId.Value;

                var data = _service.Group.Details(id);

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
        public ActionResult Delete(int? groupIdForDelete)
        {
            try
            {
                if (groupIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var id = groupIdForDelete.Value;

                _service.Group.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The group is deleted successfully!";
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