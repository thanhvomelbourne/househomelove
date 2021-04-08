using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PromotionController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllNormalPromotions()
        {
            PromotionsByTypes data = new PromotionsByTypes();

            IList<PromotionModel> promotionOnDollars = _service.Promotion.GetAllPromotionsByType((int)Constant.PromotionType.DiscountTotalOnDollars);

            if (promotionOnDollars != null)
            {
                data.NormalPromotions = new List<PromotionModel>();

                foreach (PromotionModel promo in promotionOnDollars)
                {
                    data.NormalPromotions.Add(promo);
                }
            }

            IList<PromotionModel> promotionOnPercentage = _service.Promotion.GetAllPromotionsByType((int)Constant.PromotionType.DiscountTotalOnPercentage);

            if (promotionOnPercentage != null)
            {
                if(data.NormalPromotions == null)
                {
                    data.NormalPromotions = new List<PromotionModel>();
                }

                foreach (PromotionModel promo in promotionOnPercentage)
                {
                    data.NormalPromotions.Add(promo);
                }
            }

            List<PromotionModel> shippingPromotion = _service.Promotion.GetAllPromotionsByType((int)Constant.PromotionType.DiscountShipping).ToList();

            if (shippingPromotion != null)
            {
                data.ShippingPromotions = new List<PromotionModel>();

                foreach (PromotionModel promo in shippingPromotion)
                {
                    data.ShippingPromotions.Add(promo);
                }
            }

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllPromotionTypes()
        {
            List<EnumModel> enums = ((Constant.PromotionType[])Enum.GetValues(typeof(Constant.PromotionType))).Select(c => new EnumModel() { Value = (int)c, Name = c.GetDescriptionTextForCurrentEnum() }).ToList();

            return Json(new { success = true, data = enums }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(PromotionModel model)
        {
            try
            {
                model.IsLive = (Request.Form["IsLive"] == "on");
                model.AutoApply = (Request.Form["AutoApply"] == "on");
                model.DiscountValue = UtilityHelper.RemoveSpecialCharacters(model.DiscountValue);

                if (model.Id == 0)
                {
                    try
                    {
                        _service.Promotion.Insert(model);
                        TempData["messageClass"] = "alert-success";
                        TempData["message"] = $"The promotion '{model.NameOfPromotion}' is created successfully!";
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        TempData["messageClass"] = "alert-danger";
                        TempData["message"] = "Cannot create the promotion!";
                        return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                    }
                }
                else
                {
                    try
                    {
                        _service.Promotion.Update(model);

                        TempData["messageClass"] = "alert-success";
                        TempData["message"] = $"The product '{model.NameOfPromotion}' is updated successfully!";
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        TempData["messageClass"] = "alert-danger";
                        TempData["message"] = "Cannot update the promotion!";
                        return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                    }
                }

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot create/update the promotion!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult Details(int? promotionId)
        {
            if (promotionId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            int id = promotionId.Value;

            PromotionModel data = _service.Promotion.Details(id);

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? promotionIdForDelete)
        {
            try
            {
                if (promotionIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int id = promotionIdForDelete.Value;

                _service.Promotion.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = $"The promotion is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the promotion!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }
    }
}