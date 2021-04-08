using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Models.Common;
using BusinessService.Utility;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
namespace BusinessService.Business
{
    public class ApplicationSettingBusiness : IApplicationSettingBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IProductBusiness _productBusiness;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ApplicationSettingBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory, IProductBusiness productBusiness)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _productBusiness = productBusiness;
        }

        public ApplicationSettingModel Update(ApplicationSettingModel setting)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting update = uow.Repository<ApplicationSetting>().FirstOrDefault();

                    if (update != null)
                    {
                        update.StoreName = !string.IsNullOrEmpty(setting.StoreName) ? setting.StoreName : update.StoreName;
                        update.Address = !string.IsNullOrEmpty(setting.Address) ? setting.Address : update.Address;
                        update.Phone = !string.IsNullOrEmpty(setting.Phone) ? setting.Phone : update.Phone;
                        update.Email = !string.IsNullOrEmpty(setting.Email) ? setting.Email : update.Email;
                        update.NewslettersDescription = setting.NewslettersDescription;
                        update.BannerQuote1 = setting.BannerQuote1;
                        update.BannerQuote2 = setting.BannerQuote2;
                        update.BannerQuote3 = setting.BannerQuote3;
                        update.BannerImage1 = !string.IsNullOrEmpty(setting.BannerImage1) ? setting.BannerImage1 : !string.IsNullOrEmpty(update.BannerImage1) ? update.BannerImage1 : Constant.DefaultImagePath.DefaultBanner;
                        update.BannerImage2 = !string.IsNullOrEmpty(setting.BannerImage2) ? setting.BannerImage2 : update.BannerImage2;
                        update.BannerImage3 = !string.IsNullOrEmpty(setting.BannerImage3) ? setting.BannerImage3 : update.BannerImage3;
                        update.PromotionImage1 = !string.IsNullOrEmpty(setting.PromotionImage1) ? setting.PromotionImage1 : update.PromotionImage1;
                        update.PromotionImage2 = !string.IsNullOrEmpty(setting.PromotionImage2) ? setting.PromotionImage2 : update.PromotionImage2;
                        update.PromotionImage3 = !string.IsNullOrEmpty(setting.PromotionImage3) ? setting.PromotionImage3 : update.PromotionImage3;
                        update.PromotionImage4 = !string.IsNullOrEmpty(setting.PromotionImage4) ? setting.PromotionImage4 : update.PromotionImage4;
                        update.PromotionImage5 = !string.IsNullOrEmpty(setting.PromotionImage5) ? setting.PromotionImage5 : update.PromotionImage5;
                        update.PromotionImage6 = !string.IsNullOrEmpty(setting.PromotionImage6) ? setting.PromotionImage6 : update.PromotionImage6;
                        update.AdsIcon1 = !string.IsNullOrEmpty(setting.AdsIcon1) ? setting.AdsIcon1 : update.AdsIcon1;
                        update.AdsTitle1 = setting.AdsTitle1;
                        update.AdsIcon2 = !string.IsNullOrEmpty(setting.AdsIcon2) ? setting.AdsIcon2 : update.AdsIcon2;
                        update.AdsTitle2 = setting.AdsTitle2;
                        update.AdsIcon3 = !string.IsNullOrEmpty(setting.AdsIcon3) ? setting.AdsIcon3 : update.AdsIcon3;
                        update.AdsTitle3 = setting.AdsTitle3;
                        update.AdsIcon4 = !string.IsNullOrEmpty(setting.AdsIcon4) ? setting.AdsIcon4 : update.AdsIcon4;
                        update.AdsTitle4 = setting.AdsTitle4;
                        update.AdsIcon5 = !string.IsNullOrEmpty(setting.AdsIcon5) ? setting.AdsIcon5 : update.AdsIcon5;
                        update.AdsTitle5 = setting.AdsTitle5;
                        update.AdsIcon6 = !string.IsNullOrEmpty(setting.AdsIcon6) ? setting.AdsIcon6 : update.AdsIcon6;
                        update.AdsTitle6 = setting.AdsTitle6;
                        update.ServiceIcon1 = setting.ServiceIcon1;
                        update.ServiceTitle1 = setting.ServiceTitle1;
                        update.ServiceDescription1 = setting.ServiceDescription1;
                        update.ServiceIcon2 = setting.ServiceIcon2;
                        update.ServiceTitle2 = setting.ServiceTitle2;
                        update.ServiceDescription2 = setting.ServiceDescription2;
                        update.ServiceIcon3 = setting.ServiceIcon3;
                        update.ServiceTitle3 = setting.ServiceTitle3;
                        update.ServiceDescription3 = setting.ServiceDescription3;
                        update.ServiceIcon4 = setting.ServiceIcon4;
                        update.ServiceTitle4 = setting.ServiceTitle4;
                        update.ServiceDescription4 = setting.ServiceDescription4;
                        update.PartnerLogo1 = !string.IsNullOrEmpty(setting.PartnerLogo1) ? setting.PartnerLogo1 : update.PartnerLogo1;
                        update.PartnerLogo2 = !string.IsNullOrEmpty(setting.PartnerLogo2) ? setting.PartnerLogo2 : update.PartnerLogo2;
                        update.PartnerLogo3 = !string.IsNullOrEmpty(setting.PartnerLogo3) ? setting.PartnerLogo3 : update.PartnerLogo3;
                        update.PartnerLogo4 = !string.IsNullOrEmpty(setting.PartnerLogo4) ? setting.PartnerLogo4 : update.PartnerLogo4;

                        uow.SaveChanges();

                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToContentSettingModel(update);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public ApplicationSettingModel UpdatePolicySetting(ApplicationSettingModel setting)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting update = uow.Repository<ApplicationSetting>().FirstOrDefault();

                    if (update != null)
                    {
                        update.TermsAndConditions = setting.TermsAndConditions;
                        update.FAQ = setting.FAQ;
                        update.ReturnPolicy = setting.ReturnPolicy;
                        update.AboutUs = setting.AboutUs;
                        update.ClickAndCollectPage = setting.ClickAndCollectPage;
                        update.HomeMailPage = setting.HomeMailPage;


                        uow.SaveChanges();

                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToPolicySettingModel(update);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public ApplicationSettingModel UpdateAppConfiguration(ApplicationSettingModel setting)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting update = uow.Repository<ApplicationSetting>().FirstOrDefault();

                    if (update != null)
                    {
                        update.AdminEmail = setting.AdminEmail;
                        update.CustomerServiceEmail = setting.CustomerServiceEmail;
                        update.ECommerceEmail = setting.ECommerceEmail;
                        update.PaymentReceiptEmail = setting.PaymentReceiptEmail;
                        update.GSTPercent = setting.GSTPercent;
                        update.CreditCardSurcharge = setting.CreditCardSurcharge;
                        update.PaypalSurcharge = setting.PaypalSurcharge;
                        update.FreeShippingAusPostFrom = setting.FreeShippingAusPostFrom != null && setting.FreeShippingAusPostFrom != 0 ? setting.FreeShippingAusPostFrom : null;
                        update.FreeShippingHomeMailFrom = setting.FreeShippingHomeMailFrom != null && setting.FreeShippingHomeMailFrom != 0 ? setting.FreeShippingHomeMailFrom : null;
                        update.AusPostDescription = setting.AusPostDescription;
                        update.HomeMailDescription = setting.HomeMailDescription;
                        update.HomeMailDefaultPrice1 = setting.HomeMailDefaultPrice1;
                        update.HomeMailDefaultPrice2 = setting.HomeMailDefaultPrice2;
                        update.HomeMailDefaultPrice3 = setting.HomeMailDefaultPrice3;
                        update.HomeMailAvailableFrom = setting.HomeMailAvailableFrom;
                        update.WarehousePostcode = setting.WarehousePostcode;
                        update.PopularSearchTag = setting.PopularSearchTag;
                        update.CurrencyRateUSDToAUD = setting.CurrencyRateUSDToAUD;
                        update.RateCalculateOriginalPrice = setting.RateCalculateOriginalPrice;
                        update.RateCalculateDiscountPrice = setting.RateCalculateDiscountPrice;
                        update.ClickAndCollectDescription = setting.ClickAndCollectDescription;
                        update.MetaDescription = setting.MetaDescription;
                        update.FacebookLink = setting.FacebookLink;
                        update.InstagramLink = setting.InstagramLink;

                        uow.SaveChanges();

                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToAppConfigurationModel(update);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public ApplicationSettingModel Details()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting detail = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (detail != null)
                    {
                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToContentSettingModel(detail);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ApplicationSettingModel PolicySetting()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting detail = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (detail != null)
                    {
                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToPolicySettingModel(detail);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ApplicationSettingModel AppConfiguration()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting detail = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (detail != null)
                    {
                        ApplicationSettingModel model = Mapping.ConvertApplicationSettingToAppConfigurationModel(detail);
                        SearchFileModel productImages = new SearchFileModel();
                        SearchFileModel bannerImages = new SearchFileModel();
                        SearchFileModel qrCodeImages = new SearchFileModel();

                        string searchProductImageFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.ProductImagePath);
                        string searchBannerImageFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.AppImagePath);
                        string searchQRCodeImageFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.QRCodePath);
                        string[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                        productImages = Helper.GetFilesFrom(searchProductImageFolder, filters, false);
                        bannerImages = Helper.GetFilesFrom(searchBannerImageFolder, filters, false);
                        qrCodeImages = Helper.GetFilesFrom(searchQRCodeImageFolder, filters, false);
                        IList<ProductModel> products = _productBusiness.GetAllProducts();

                        if (productImages.FileNames.Any())
                        {
                            model.TotalProductImages = productImages.FileNames.Count();
                            model.TotalProductImagesSize = productImages.TotalFileSize;

                            int usedProduct = 0;

                            foreach (string img in productImages.FileNames)
                            {
                                foreach (ProductModel p in products)
                                {
                                    if ((!string.IsNullOrEmpty(p.PrimaryImage) && p.PrimaryImage.Contains(img))
                                        || (!string.IsNullOrEmpty(p.SubImage1) && p.SubImage1.Contains(img))
                                        || (!string.IsNullOrEmpty(p.SubImage2) && p.SubImage2.Contains(img))
                                        || (!string.IsNullOrEmpty(p.SubImage3) && p.SubImage3.Contains(img))
                                        || (!string.IsNullOrEmpty(p.SubImage4) && p.SubImage4.Contains(img))
                                        || (!string.IsNullOrEmpty(p.SubImage5) && p.SubImage5.Contains(img)))
                                    {
                                        usedProduct++;
                                        break;
                                    }
                                }
                            }

                            model.TotalUsedProductImages = usedProduct;
                            model.TotalUnusedProductImages = model.TotalProductImages - model.TotalUsedProductImages;
                        }

                        if (qrCodeImages.FileNames.Any())
                        {
                            model.TotalQRCodeImages = qrCodeImages.FileNames.Count();
                            model.TotalQRCodeImagesSize = qrCodeImages.TotalFileSize;

                            int usedQRCode = 0;

                            foreach (string img in qrCodeImages.FileNames)
                            {
                                foreach (ProductModel p in products)
                                {
                                    if (!string.IsNullOrEmpty(p.QRCode) && p.QRCode.Contains(img))
                                    {
                                        usedQRCode++;
                                        break;
                                    }
                                }
                            }

                            model.TotalUsedQRCodeImages = usedQRCode;
                            model.TotalUnusedQRCodeImages = model.TotalQRCodeImages - model.TotalUsedQRCodeImages;
                        }

                        if (bannerImages.FileNames.Any())
                        {
                            model.TotalAppBannerImages = bannerImages.FileNames.Count();
                            model.TotalAppBannerImagesSize = bannerImages.TotalFileSize;

                            int usedBanner = 0;

                            foreach (string img in bannerImages.FileNames)
                            {
                                if ((!string.IsNullOrEmpty(detail.BannerImage1) && detail.BannerImage1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.BannerImage2) && detail.BannerImage2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.BannerImage3) && detail.BannerImage3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon1) && detail.AdsIcon1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon2) && detail.AdsIcon2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon3) && detail.AdsIcon3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon4) && detail.AdsIcon4.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon5) && detail.AdsIcon5.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon6) && detail.AdsIcon6.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage1) && detail.PromotionImage1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage2) && detail.PromotionImage2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage3) && detail.PromotionImage3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo1) && detail.PartnerLogo1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo2) && detail.PartnerLogo2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo3) && detail.PartnerLogo3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo4) && detail.PartnerLogo4.Contains(img)))
                                {
                                    usedBanner++;
                                    continue;
                                }
                            }

                            model.TotalUsedAppBannerImages = usedBanner;
                            model.TotalUnusedAppBannerImages = model.TotalAppBannerImages - model.TotalUsedAppBannerImages;
                        }

                        model.TotalShoppingCart = uow.Repository<ShoppingCart>().AsNoTracking().Count();
                        DateTime yesterday = DateTime.UtcNow.AddDays(-1);
                        model.TotalUnusedShoppingCart = uow.Repository<ShoppingCart>().AsNoTracking().Where(x => !x.Orders.Any() && x.CreatedAt.Value <= yesterday).Count();
                        model.TotalUsedShoppingCart = uow.Repository<ShoppingCart>().AsNoTracking().Where(x=>x.Orders.Any()).Count();

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public HomePageModel GetCommonDataForHomePageClient()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    HomePageModel homeModel = new HomePageModel();

                    ApplicationSetting detail = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (detail != null)
                    {
                        ApplicationSettingModel model = Utility.Mapping.ConvertApplicationSettingToContentSettingModel(detail);

                        homeModel.PopularSearchs = detail.PopularSearchTag.Contains(';') ? detail.PopularSearchTag.Trim().Split(';') : detail.PopularSearchTag.Trim().Split(',');
                        homeModel.ApplicationSetting = model;
                    }

                    List<Category> categories = uow.Repository<Category>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive)).OrderBy(x => x.Id).ToList();
                    List<CategoryModel> modelList = new List<CategoryModel>();

                    if (categories.Any())
                    {
                        foreach (Category category in categories)
                        {
                            modelList.Add(Utility.Mapping.ConvertCategoryToCategoryModel(category));
                        }

                        homeModel.CategoryList = modelList;
                    }

                    return homeModel;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public DashboardModel GetDashboard()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Order lastOrder = uow.Repository<Order>().AsNoTracking().OrderByDescending(x => x.CompletedAt).FirstOrDefault();

                    DashboardModel model = new DashboardModel
                    {
                        TotalCategoryItems = uow.Repository<Category>().AsNoTracking().Count(),
                        TotalProductItems = uow.Repository<Product>().AsNoTracking().Count(),
                        TotalOrderItems = uow.Repository<Order>().AsNoTracking().Count(),
                        TotalUserItems = uow.Repository<UserProfile>().AsNoTracking().Count(),
                        TotalContactMessageUnread = uow.Repository<Contact>().AsNoTracking().Count(x => !x.IsRead),
                        TotalSubscription = uow.Repository<Subcription>().AsNoTracking().Count(),
                        LastOrderPlaced = lastOrder != null ? $"{lastOrder.CompletedAt.Value.ToLongDateString()} {lastOrder.CompletedAt.Value.ToLongTimeString()}" : string.Empty
                    };

                    Contact lastContactMessage = uow.Repository<Contact>().AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefault();

                    if (lastContactMessage?.CreatedAt != null)
                    {
                        TimeSpan travelTime = DateTime.UtcNow - lastContactMessage.CreatedAt.Value;
                        model.LastContactMessageTime = $"{travelTime.Hours} hours {travelTime.Minutes} minutes ago";
                    }

                    Subcription lastSubscription = uow.Repository<Subcription>().AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefault();

                    if (lastSubscription?.CreatedAt != null)
                    {
                        TimeSpan travelTime = DateTime.UtcNow - lastSubscription.CreatedAt.Value;
                        model.LastSubscriptionTime = $"{travelTime.Hours} hours {travelTime.Minutes} minutes ago";
                    }

                    model.TotalPromotionIsRunning = uow.Repository<Promotion>().AsNoTracking().Count(x => x.IsLive);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public AdminModel GetCommonDataForAdminPage()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    AdminModel adminModel = new AdminModel();

                    IQueryable<Contact> listContactMessage = uow.Repository<Contact>().AsNoTracking().OrderByDescending(x => x.Id).Skip(0).Take(3);

                    if (listContactMessage == null)
                    {
                        return null;
                    }

                    adminModel.ContactMessageList = new List<ContactModel>();

                    foreach (Contact c in listContactMessage)
                    {
                        adminModel.ContactMessageList.Add(Utility.Mapping.ConvertContactToContactModel(c));
                    }

                    return adminModel;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public void SendTestEmail()
        {
            try
            {
                EmailHelper.SendSystemTestEmail();
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool DeleteUnusedProductImages()
        {
            SearchFileModel productImages = new SearchFileModel();
            string searchFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.ProductImagePath);
            string[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            productImages = Helper.GetFilesFrom(searchFolder, filters, false);
            List<string> unusedFileName = new List<string>();

            if (productImages.FileNames.Any())
            {
                IList<ProductModel> products = _productBusiness.GetAllProducts();

                int usedProduct = 0;

                foreach (string img in productImages.FileNames)
                {
                    bool isUsed = false;

                    foreach (ProductModel p in products)
                    {
                        if ((!string.IsNullOrEmpty(p.PrimaryImage) && p.PrimaryImage.Contains(img))
                            || (!string.IsNullOrEmpty(p.SubImage1) && p.SubImage1.Contains(img))
                            || (!string.IsNullOrEmpty(p.SubImage2) && p.SubImage2.Contains(img))
                            || (!string.IsNullOrEmpty(p.SubImage3) && p.SubImage3.Contains(img))
                            || (!string.IsNullOrEmpty(p.SubImage4) && p.SubImage4.Contains(img))
                            || (!string.IsNullOrEmpty(p.SubImage5) && p.SubImage5.Contains(img)))
                        {
                            usedProduct++;
                            isUsed = true;
                            break;
                        }
                    }

                    if (!isUsed)
                    {
                        unusedFileName.Add(img);
                    }
                }

                if (unusedFileName.Any())
                {
                    try
                    {
                        foreach (string fileName in unusedFileName)
                        {
                            if (File.Exists(Path.Combine(searchFolder, fileName)))
                            {
                                File.Delete(Path.Combine(searchFolder, fileName));
                            }
                        }

                        return true;

                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        return false;
                    }
                }

                return true;
            }

            return true;
        }

        public bool DeleteUnusedQRCodeImages()
        {
            SearchFileModel productImages = new SearchFileModel();
            string searchFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.QRCodePath);
            string[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            productImages = Helper.GetFilesFrom(searchFolder, filters, false);
            List<string> unusedFileName = new List<string>();

            if (productImages.FileNames.Any())
            {
                IList<ProductModel> products = _productBusiness.GetAllProducts();

                int usedProduct = 0;

                foreach (string img in productImages.FileNames)
                {
                    bool isUsed = false;

                    foreach (ProductModel p in products)
                    {
                        if (!string.IsNullOrEmpty(p.QRCode) && p.QRCode.Contains(img))
                        {
                            usedProduct++;
                            isUsed = true;
                            break;
                        }
                    }

                    if (!isUsed)
                    {
                        unusedFileName.Add(img);
                    }
                }

                if (unusedFileName.Any())
                {
                    try
                    {
                        foreach (string fileName in unusedFileName)
                        {
                            if (File.Exists(Path.Combine(searchFolder, fileName)))
                            {
                                File.Delete(Path.Combine(searchFolder, fileName));
                            }
                        }

                        return true;

                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        return false;
                    }
                }

                return true;
            }

            return true;
        }

        public bool DeleteUnusedBannersIcons()
        {
            SearchFileModel bannerImages = new SearchFileModel();
            string searchFolder = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, Constant.Path.AppImagePath);
            string[] filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
            bannerImages = Helper.GetFilesFrom(searchFolder, filters, false);
            List<string> unusedFileName = new List<string>();

            ApplicationSettingModel detail = Details();

            if (bannerImages.FileNames.Any())
            {
                int usedProduct = 0;

                foreach (string img in bannerImages.FileNames)
                {
                    bool isUsed = false;

                    if ((!string.IsNullOrEmpty(detail.BannerImage1) && detail.BannerImage1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.BannerImage2) && detail.BannerImage2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.BannerImage3) && detail.BannerImage3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon1) && detail.AdsIcon1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon2) && detail.AdsIcon2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon3) && detail.AdsIcon3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon4) && detail.AdsIcon4.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon5) && detail.AdsIcon5.Contains(img))
                                || (!string.IsNullOrEmpty(detail.AdsIcon6) && detail.AdsIcon6.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage1) && detail.PromotionImage1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage2) && detail.PromotionImage2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage3) && detail.PromotionImage3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage4) && detail.PromotionImage4.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage5) && detail.PromotionImage5.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PromotionImage6) && detail.PromotionImage6.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo1) && detail.PartnerLogo1.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo2) && detail.PartnerLogo2.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo3) && detail.PartnerLogo3.Contains(img))
                                || (!string.IsNullOrEmpty(detail.PartnerLogo4) && detail.PartnerLogo4.Contains(img)))
                    {
                        usedProduct++;
                        isUsed = true;
                        continue;
                    }

                    if (!isUsed)
                    {
                        unusedFileName.Add(img);
                    }
                }

                if (unusedFileName.Any())
                {
                    try
                    {
                        foreach (string fileName in unusedFileName)
                        {
                            if (File.Exists(Path.Combine(searchFolder, fileName)))
                            {
                                File.Delete(Path.Combine(searchFolder, fileName));
                            }
                        }

                        return true;

                    }
                    catch (Exception ex)
                    {
                        log.Debug($"{ex.Message} - {ex}");
                        EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                        return false;
                    }
                }

                return true;
            }

            return true;
        }

        public bool DeleteImage(string name)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ApplicationSetting detail = uow.Repository<ApplicationSetting>().AsNoTracking().FirstOrDefault();

                    if (detail != null)
                    {
                        if(name == "Banner Image 1")
                        {
                            detail.BannerImage1 = Constant.DefaultImagePath.DefaultBanner;
                        }
                        else if (name == "Banner Image 2")
                        {
                            detail.BannerImage2 = null;
                        }
                        else if(name == "Banner Image 3")
                        {
                            detail.BannerImage3 = null;
                        }
                        else if (name == "Promotion Image 1")
                        {
                            detail.PromotionImage1 = null;
                        }
                        else if (name == "Promotion Image 2")
                        {
                            detail.PromotionImage2 = null;
                        }
                        else if (name == "Promotion Image 3")
                        {
                            detail.PromotionImage3 = null;
                        }

                        uow.SaveChanges();

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }
    }
}
