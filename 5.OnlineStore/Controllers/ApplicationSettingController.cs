using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using OnlineStore.Models;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ApplicationSettingController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ApplicationSettingController(IService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Update(ApplicationSettingModel model, SettingImageFiles settingImageFiles)
        {
            try
            {
                if (settingImageFiles.BannerImage1 != null)
                {
                    try
                    {
                        string banner1FileName = $"Banner1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.BannerImage1.FileName}";
                        banner1FileName = UtilityHelper.RemoveSpecialCharacters(banner1FileName);
                        string banner1Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), banner1FileName);

                        settingImageFiles.BannerImage1.SaveAs(banner1Path);

                        model.BannerImage1 = Constant.Path.AppImagePath.Replace("\\", "/") + banner1FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.BannerImage2 != null)
                {
                    try
                    {
                        string banner2FileName = $"Banner2-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.BannerImage2.FileName}";
                        banner2FileName = UtilityHelper.RemoveSpecialCharacters(banner2FileName);
                        string banner2Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), banner2FileName);

                        settingImageFiles.BannerImage2.SaveAs(banner2Path);

                        model.BannerImage2 = Constant.Path.AppImagePath.Replace("\\", "/") + banner2FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.BannerImage3 != null)
                {
                    try
                    {
                        string banner3FileName = $"Banner3-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.BannerImage3.FileName}";
                        banner3FileName = UtilityHelper.RemoveSpecialCharacters(banner3FileName);
                        string banner3Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), banner3FileName);

                        settingImageFiles.BannerImage3.SaveAs(banner3Path);

                        model.BannerImage3 = Constant.Path.AppImagePath.Replace("\\", "/") + banner3FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage1 != null)
                {
                    try
                    {
                        string promotion1FileName = $"Promotion1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage1.FileName}";
                        promotion1FileName = UtilityHelper.RemoveSpecialCharacters(promotion1FileName);
                        string promotion1Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion1FileName);

                        settingImageFiles.PromotionImage1.SaveAs(promotion1Path);

                        model.PromotionImage1 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion1FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage2 != null)
                {
                    try
                    {
                        string promotion2FileName = $"Promotion2-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage2.FileName}";
                        promotion2FileName = UtilityHelper.RemoveSpecialCharacters(promotion2FileName);
                        string promotion2Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion2FileName);

                        settingImageFiles.PromotionImage2.SaveAs(promotion2Path);

                        model.PromotionImage2 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion2FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage3 != null)
                {
                    try
                    {
                        string promotion3FileName = $"Promotion3-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage3.FileName}";
                        promotion3FileName = UtilityHelper.RemoveSpecialCharacters(promotion3FileName);
                        string promotion3Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion3FileName);

                        settingImageFiles.PromotionImage3.SaveAs(promotion3Path);

                        model.PromotionImage3 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion3FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage4 != null)
                {
                    try
                    {
                        string promotion4FileName = $"Promotion4-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage4.FileName}";
                        promotion4FileName = UtilityHelper.RemoveSpecialCharacters(promotion4FileName);
                        string promotion4Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion4FileName);

                        settingImageFiles.PromotionImage4.SaveAs(promotion4Path);

                        model.PromotionImage4 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion4FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage5 != null)
                {
                    try
                    {
                        string promotion5FileName = $"Promotion5-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage5.FileName}";
                        promotion5FileName = UtilityHelper.RemoveSpecialCharacters(promotion5FileName);
                        string promotion5Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion5FileName);

                        settingImageFiles.PromotionImage5.SaveAs(promotion5Path);

                        model.PromotionImage5 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion5FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PromotionImage6 != null)
                {
                    try
                    {
                        string promotion6FileName = $"Promotion6-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PromotionImage6.FileName}";
                        promotion6FileName = UtilityHelper.RemoveSpecialCharacters(promotion6FileName);
                        string promotion6Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), promotion6FileName);

                        settingImageFiles.PromotionImage6.SaveAs(promotion6Path);

                        model.PromotionImage6 = Constant.Path.AppImagePath.Replace("\\", "/") + promotion6FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PartnerLogo1 != null)
                {
                    try
                    {
                        string partner1FileName = $"Partner1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PartnerLogo1.FileName}";
                        partner1FileName = UtilityHelper.RemoveSpecialCharacters(partner1FileName);
                        string partner1Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), partner1FileName);

                        settingImageFiles.PartnerLogo1.SaveAs(partner1Path);

                        model.PartnerLogo1 = Constant.Path.AppImagePath.Replace("\\", "/") + partner1FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PartnerLogo2 != null)
                {
                    try
                    {
                        string partner2FileName = $"Partner2-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PartnerLogo2.FileName}";
                        partner2FileName = UtilityHelper.RemoveSpecialCharacters(partner2FileName);
                        string partner2Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), partner2FileName);

                        settingImageFiles.PartnerLogo2.SaveAs(partner2Path);

                        model.PartnerLogo2 = Constant.Path.AppImagePath.Replace("\\", "/") + partner2FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PartnerLogo3 != null)
                {
                    try
                    {
                        string partner3FileName = $"Partner3-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PartnerLogo3.FileName}";
                        partner3FileName = UtilityHelper.RemoveSpecialCharacters(partner3FileName);
                        string partner3Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), partner3FileName);

                        settingImageFiles.PartnerLogo3.SaveAs(partner3Path);

                        model.PartnerLogo3 = Constant.Path.AppImagePath.Replace("\\", "/") + partner3FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.PartnerLogo4 != null)
                {
                    try
                    {
                        string partner4FileName = $"Partner4-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.PartnerLogo4.FileName}";
                        partner4FileName = UtilityHelper.RemoveSpecialCharacters(partner4FileName);
                        string partner4Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), partner4FileName);

                        settingImageFiles.PartnerLogo4.SaveAs(partner4Path);

                        model.PartnerLogo4 = Constant.Path.AppImagePath.Replace("\\", "/") + partner4FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon1 != null)
                {
                    try
                    {
                        string adsIcon1FileName = $"AdsIcon1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon1.FileName}";
                        adsIcon1FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon1FileName);
                        string adsIcon1Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon1FileName);

                        settingImageFiles.AdsIcon1.SaveAs(adsIcon1Path);

                        model.AdsIcon1 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon1FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon2 != null)
                {
                    try
                    {
                        string adsIcon2FileName = $"AdsIcon2-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon2.FileName}";
                        adsIcon2FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon2FileName);
                        string adsIcon2Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon2FileName);

                        settingImageFiles.AdsIcon2.SaveAs(adsIcon2Path);

                        model.AdsIcon2 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon2FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon3 != null)
                {
                    try
                    {
                        string adsIcon3FileName = $"AdsIcon3-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon3.FileName}";
                        adsIcon3FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon3FileName);
                        string adsIcon3Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon3FileName);

                        settingImageFiles.AdsIcon3.SaveAs(adsIcon3Path);

                        model.AdsIcon3 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon3FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon4 != null)
                {
                    try
                    {
                        string adsIcon4FileName = $"AdsIcon4-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon4.FileName}";
                        adsIcon4FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon4FileName);
                        string adsIcon4Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon4FileName);

                        settingImageFiles.AdsIcon4.SaveAs(adsIcon4Path);

                        model.AdsIcon4 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon4FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon5 != null)
                {
                    try
                    {
                        string adsIcon5FileName = $"AdsIcon1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon5.FileName}";
                        adsIcon5FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon5FileName);
                        string adsIcon5Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon5FileName);

                        settingImageFiles.AdsIcon5.SaveAs(adsIcon5Path);

                        model.AdsIcon5 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon5FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                if (settingImageFiles.AdsIcon6 != null)
                {
                    try
                    {
                        string adsIcon6FileName = $"AdsIcon1-{DateTime.UtcNow.ToBinary()}-{settingImageFiles.AdsIcon6.FileName}";
                        adsIcon6FileName = UtilityHelper.RemoveSpecialCharacters(adsIcon6FileName);
                        string adsIcon6Path = Path.Combine(Server.MapPath(Constant.Path.AppImagePath), adsIcon6FileName);

                        settingImageFiles.AdsIcon6.SaveAs(adsIcon6Path);

                        model.AdsIcon6 = Constant.Path.AppImagePath.Replace("\\", "/") + adsIcon6FileName;
                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                    }
                }

                ApplicationSettingModel data = _service.ApplicationSetting.Update(model);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The content setting is updated successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot update the content setting!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult UpdateOtherContents(ApplicationSettingModel model)
        {
            try
            {
                _service.ApplicationSetting.UpdatePolicySetting(model);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The policy setting is updated successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot update the policy setting!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult UpdateAppConfiguration(ApplicationSettingModel model)
        {
            try
            {
                _service.ApplicationSetting.UpdateAppConfiguration(model);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The app configuration is updated successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot update the app configuration!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        public JsonResult Details()
        {
            try
            {
                ApplicationSettingModel data = _service.ApplicationSetting.Details();

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
        public JsonResult GetAppConfiguration()
        {
            try
            {
                ApplicationSettingModel data = _service.ApplicationSetting.AppConfiguration();

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult SendTestEmail()
        {
            try
            {
                _service.ApplicationSetting.SendTestEmail();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "Test email is sent successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Info($"Cannot send system test email: {ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot send the test email!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteUnusedProductImages()
        {
            try
            {
                bool data = _service.ApplicationSetting.DeleteUnusedProductImages();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The unused product images are deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the unused product images! There were some error in process!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteUnusedBannersIcons()
        {
            try
            {
                bool data = _service.ApplicationSetting.DeleteUnusedBannersIcons();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The unused banners/icons are deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the unused banners/icons! There were some error in process!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteUnusedQRCodeImages()
        {
            try
            {
                bool data = _service.ApplicationSetting.DeleteUnusedQRCodeImages();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The unused qr code images are deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the unused qr code images! There were some error in process!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteImage(string imageForDelete)
        {
            try
            {
                if (string.IsNullOrEmpty(imageForDelete))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                _service.ApplicationSetting.DeleteImage(imageForDelete);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The image is deleted successfully!";
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