using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using log4net;
using OnlineStore.Helper;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using WebMatrix.WebData;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductController(IService service)
        {
            _service = service;
        }

        [HttpPost, ValidateInput(false)]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(ProductModel model, ProductImageFiles productImageFiles)
        {
            model.IsLive = (Request.Form["IsLive"] == "on");

            if (productImageFiles.PrimaryImage != null)
            {
                try
                {
                    string primaryFileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.PrimaryImage.FileName}";
                    primaryFileName = UtilityHelper.RemoveSpecialCharacters(primaryFileName);
                    string primaryPath = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), primaryFileName);

                    productImageFiles.PrimaryImage.SaveAs(primaryPath);

                    model.PrimaryImage = Constant.Path.ProductImagePath.Replace("\\", "/") + primaryFileName;
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                model.PrimaryImage = Constant.DefaultImagePath.DefaultProduct;
            }

            if (productImageFiles.SubImage1 != null)
            {
                try
                {
                    string sub1FileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.SubImage1.FileName}";
                    sub1FileName = UtilityHelper.RemoveSpecialCharacters(sub1FileName);
                    string sub1Path = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), sub1FileName);

                    productImageFiles.SubImage1.SaveAs(sub1Path);

                    model.SubImage1 = Constant.Path.ProductImagePath.Replace("\\", "/") + sub1FileName;

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            if (productImageFiles.SubImage2 != null)
            {
                try
                {
                    string sub2FileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.SubImage2.FileName}";
                    sub2FileName = UtilityHelper.RemoveSpecialCharacters(sub2FileName);
                    string sub2Path = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), sub2FileName);

                    productImageFiles.SubImage2.SaveAs(sub2Path);

                    model.SubImage2 = Constant.Path.ProductImagePath.Replace("\\", "/") + sub2FileName;

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            if (productImageFiles.SubImage3 != null)
            {
                try
                {
                    string sub3FileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.SubImage3.FileName}";
                    sub3FileName = UtilityHelper.RemoveSpecialCharacters(sub3FileName);
                    string sub3Path = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), sub3FileName);

                    productImageFiles.SubImage3.SaveAs(sub3Path);

                    model.SubImage3 = Constant.Path.ProductImagePath.Replace("\\", "/") + sub3FileName;

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            if (productImageFiles.SubImage4 != null)
            {
                try
                {
                    string sub4FileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.SubImage4.FileName}";
                    sub4FileName = UtilityHelper.RemoveSpecialCharacters(sub4FileName);
                    string sub4Path = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), sub4FileName);

                    productImageFiles.SubImage4.SaveAs(sub4Path);

                    model.SubImage4 = Constant.Path.ProductImagePath.Replace("\\", "/") + sub4FileName;

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);

                }
            }

            if (productImageFiles.SubImage5 != null)
            {
                try
                {
                    string sub5FileName = $"{model.Name.Trim()}-{DateTime.UtcNow.ToBinary()}-{productImageFiles.SubImage5.FileName}";
                    sub5FileName = UtilityHelper.RemoveSpecialCharacters(sub5FileName);
                    string sub5Path = Path.Combine(Server.MapPath(Constant.Path.ProductImagePath), sub5FileName);

                    productImageFiles.SubImage5.SaveAs(sub5Path);

                    model.SubImage5 = Constant.Path.ProductImagePath.Replace("\\", "/") + sub5FileName;

                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            if (model.Id == 0)
            {
                try
                {
                    model = _service.Product.Insert(model);

                    model = GenerateQRCode(model);

                    TempData["messageClass"] = "alert-success";
                    TempData["message"] = $"The product '{model.Name}' is created successfully!";
                }
                catch (Exception ex)
                {
                    log.Debug($"{ex.Message} - {ex}");
                    TempData["messageClass"] = "alert-danger";
                    TempData["message"] = "Cannot create this product!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
            }
            else
            {
                try
                {
                    model = _service.Product.Update(model);

                    if (string.IsNullOrEmpty(model.QRCode))
                    {
                        model = GenerateQRCode(model);
                    }

                    TempData["messageClass"] = "alert-success";
                    TempData["message"] = $"The product '{model.Name}' is updated successfully!";
                }
                catch (Exception ex)
                {
                    log.Debug($"{ex.Message} - {ex}");
                    TempData["messageClass"] = "alert-danger";
                    TempData["message"] = "Cannot update this product!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
            }

            string returnUrl = string.Format("{0}?productId={1}", HttpContext.Request.UrlReferrer.AbsolutePath, model.Id);

            return Redirect(returnUrl);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ApplyDiscount(int categoryDiscountId, double amountForDiscount)
        {
            try
            {
                bool applyForSubCategory = (Request.Form["applyForSubCategory"] == "on");
                _service.Product.ApplyDiscount(categoryDiscountId, amountForDiscount, applyForSubCategory);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "Applied discount successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot apply discount!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult GenerateQRCode()
        {
            try
            {
                IList<ProductModel> model = _service.Product.GetAllProducts();

                foreach (ProductModel product in model)
                {
                    GenerateQRCode(product);
                }

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "QR codes for all products are generated successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot generate QR codes for all products!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult GetAllProductForAdmin()
        {
            try
            {
                IList<ProductModel> data = _service.Product.GetAllProducts();

                return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Details(int? productId)
        {
            if (productId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            int id = productId.Value;

            ProductModel data = new ProductModel();

            if (WebSecurity.IsAuthenticated)
            {
                data = _service.Product.Details(id, WebSecurity.CurrentUserId);
            }
            else
            {
                data = _service.Product.Details(id);
            }

            return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult Delete(int? productIdForDelete)
        {
            try
            {
                if (productIdForDelete == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int id = productIdForDelete.Value;

                _service.Product.Delete(id);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "The product is deleted successfully!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "The product is deleted unsuccessfully!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }

        }

        [HttpPost]
        public ActionResult DeleteWishlist(int? wishlistId)
        {
            if (wishlistId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int id = wishlistId.Value;

            bool del = _service.Product.DeleteWishlist(id);

            if (del)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetWishlish()
        {
            if (WebSecurity.IsAuthenticated)
            {
                List<WishlistModel> data = _service.Product.GetAllWishlistProducts(WebSecurity.CurrentUserId).ToList();

                if (data != null)
                {
                    return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddProductToWishlist(int? productId)
        {
            try
            {
                if (productId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                int id = productId.Value;

                if (WebSecurity.IsAuthenticated)
                {
                    bool added = _service.Product.AddProductToWishlist(WebSecurity.CurrentUserId, id);

                    if (added)
                    {
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "If you want to use wishlist feature, you have to login!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LikeProduct(int? productId)
        {
            try
            {
                if (productId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                int id = productId.Value;

                if (WebSecurity.IsAuthenticated)
                {
                    bool added = true;
                    ProductModel result = _service.Product.LikeProduct(WebSecurity.CurrentUserId, id, out added);

                    if (result != null)
                    {
                        return Json(new { success = true, added, value = result }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "If you want to like this product" +
                        ", you have to login!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReviewProduct(int? productId, string comment, int rating = 0)
        {
            try
            {
                if (productId == null)
                {
                    return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
                }

                int id = productId.Value;

                if (WebSecurity.IsAuthenticated)
                {
                    ProductModel result = _service.Product.ReviewProduct(WebSecurity.CurrentUserId, id, rating, comment);

                    if (result != null)
                    {
                        return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "If you want to review this product" +
                        ", you have to login!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RemoveReview(int reviewId, int productId)
        {
            try
            {

                if (WebSecurity.IsAuthenticated)
                {
                    bool del = _service.Product.RemoveReviewProduct(reviewId, WebSecurity.CurrentUserId, productId);

                    if (del)
                    {
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { success = false, message = "You have to login before reivew and can only remove your own review!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GenerateNameKey(int? catId)
        {
            if (catId == null)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }

            int id = catId.Value;

            string namekey = _service.Product.GenerateNameKey(id);


            return Json(new { success = true, namekey }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult ExportToExcel(Parameter parameter)
        {
            try
            {
                IList<ProductModel> data = _service.Product.GetAllProducts();

                System.Web.UI.WebControls.GridView grid = new System.Web.UI.WebControls.GridView
                {
                    DataSource = data
                };
                grid.DataBind();

                Response.ClearContent();
                Response.AddHeader($"content-disposition", "attachement; filename=Products.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

                TempData["messageClass"] = "alert-success";
                TempData["message"] = "Export produc table to excel file successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot export product table to excel file!";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private ProductModel GenerateQRCode(ProductModel model)
        {
            QRCodeWriter qrcode = new QRCodeWriter();
            string qrValue = Constant.Path.QRCodeForProduct + model.Id;

            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 300,
                    Width = 300,
                    Margin = 1
                }
            };

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image
            {
                Height = 150,
                Width = 150
            };

            using (System.Drawing.Bitmap bitmap = barcodeWriter.Write(qrValue))
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(Server.MapPath(Constant.Path.QRCodePath + "Product(" + model.Id + ").png"), ImageFormat.Png);
                byte[] byteImage = stream.ToArray();
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }

            model.QRCode = Constant.Path.QRCodePath + "Product(" + model.Id + ").png";

            model = _service.Product.UpdateQRCode(model);

            return model;
        }

        [HttpGet]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public JsonResult CheckValidate(string productName)
        {
            try
            {
                var data = _service.Product.CheckValidate(productName);

                return Json(new { success = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}