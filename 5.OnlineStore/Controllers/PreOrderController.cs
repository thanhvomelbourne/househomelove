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
using WebMatrix.WebData;

namespace OnlineStore.Controllers
{
    public class PreOrderController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PreOrderController(IService service)
        {
            this._service = service;
        }

        [HttpPost]
        public ActionResult CreatePreOrder(PreOrderModel preorder)
        {
            if (string.IsNullOrEmpty(preorder.FirstName) || string.IsNullOrEmpty(preorder.LastName) || string.IsNullOrEmpty(preorder.Email) || string.IsNullOrEmpty(preorder.Phone))
            {
                return Json(new { success = false, message = "Please fill all your information to pre-order!" }, JsonRequestBehavior.AllowGet);
            }

            if (WebSecurity.IsAuthenticated)
            {
                preorder.UserId = WebSecurity.CurrentUserId;
            }

            try
            {
                var data = _service.PreOrder.InsertPreOrder(preorder);

                if (data != null)
                {
                    return Json(new { success = true, message = "Your pre-order has been sent successfully to househomelove. We will contact you soon!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Cannot create this pre-order. There were some errors in the process!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult GetAllStatus()
        {
            List<string> statusList = new List<string>();
            statusList.Add(Constant.PreOrderStatus.ReceivedPreOrder);
            statusList.Add(Constant.PreOrderStatus.ApprovedPreOrder);
            statusList.Add(Constant.PreOrderStatus.DeclinedPreOrder);
            statusList.Add(Constant.PreOrderStatus.PreOrderIsReady);
            statusList.Add(Constant.PreOrderStatus.ClosePreOrder);

            return Json(new { success = true, data = statusList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Update(PreOrderModel preorder)
        {
            var data = _service.PreOrder.UpdatePreOrder(preorder);

            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult Details(int? preOrderId)
        {
            if (preOrderId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            int id = preOrderId.Value;

            var data = _service.PreOrder.Details(id);

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? preorderIdForDelete)
        {
            try
            {
                if (preorderIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int id = preorderIdForDelete.Value;

                _service.PreOrder.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The pre-order is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the pre-order!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}