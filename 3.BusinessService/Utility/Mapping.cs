using BusinessService.Models;
using CoreLibrary.Data;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessService.Utility
{
    public static class Mapping
    {
        public static CategoryModel ConvertCategoryToCategoryModel(Category category)
        {
            CategoryModel model = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsLive = category.IsLive,
                PrefixForProductCode = category.PrefixForProductCode,
                CreatedAt = category.CreatedAt.HasValue ? Helper.ConvertDateTimeString(category.CreatedAt.Value) : string.Empty,
                UpdatedAt = category.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(category.UpdatedAt.Value) : string.Empty,
                CreatedBy = category.CreatedBy,
                UpdatedBy = category.UpdatedBy
            };

            return model;
        }

        public static GroupModel ConvertGroupToCategoryModel(Group group)
        {
            GroupModel model = new GroupModel
            {
                Id = group.Id,
                GroupName = group.GroupName,
                GroupType = group.GroupType,
                CreatedAt = group.CreatedAt.HasValue ? Helper.ConvertDateTimeString(group.CreatedAt.Value) : string.Empty,
                UpdatedAt = group.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(group.UpdatedAt.Value) : string.Empty,
                CreatedBy = group.CreatedBy,
                UpdatedBy = group.UpdatedBy
            };

            return model;
        }

        public static ColorModel ConvertColorToColorModel(Color color)
        {
            ColorModel model = new ColorModel
            {
                Id = color.Id,
                ColorName = color.ColorName,
                ColorCode = color.ColorCode,
                Description = color.Description,
                CreatedAt = color.CreatedAt.HasValue ? Helper.ConvertDateTimeString(color.CreatedAt.Value) : string.Empty,
                UpdatedAt = color.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(color.UpdatedAt.Value) : string.Empty,
                CreatedBy = color.CreatedBy,
                UpdatedBy = color.UpdatedBy
            };

            return model;
        }

        public static ContactModel ConvertContactToContactModel(Contact contact)
        {
            ContactModel model = new ContactModel
            {
                Id = contact.Id,
                ContactName = contact.ContactName,
                ContactEmail = contact.ContactEmail,
                ContactPhone = contact.ContactPhone,
                Subject = contact.Subject,
                Message = contact.Message,
                IsRead = contact.IsRead,
                CreatedAt = contact.CreatedAt.HasValue ? Helper.ConvertDateTimeString(contact.CreatedAt.Value) : string.Empty,
                UpdatedAt = contact.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(contact.UpdatedAt.Value) : string.Empty,
                CreatedBy = contact.CreatedBy,
                UpdatedBy = contact.UpdatedBy
            };

            return model;
        }

        public static SubcriptionModel ConvertSubcriptionToSubcriptionModel(Subcription sub)
        {
            SubcriptionModel model = new SubcriptionModel
            {
                Id = sub.Id,
                EmailSubcribed = sub.EmailSubcribed,
                IsCustomer = sub.IsCustomer,
                UserId = sub.UserId,
                CreatedAt = sub.CreatedAt.HasValue ? Helper.ConvertDateTimeString(sub.CreatedAt.Value) : string.Empty
            };

            return model;
        }

        public static PromotionModel ConvertPromotionToPromotionModel(Promotion promo)
        {
            PromotionModel model = new PromotionModel()
            {
                Id = promo.Id,
                NameOfPromotion = promo.NameOfPromotion,
                PromotionTypeDescription = Constant.GetEnumDescription((Constant.PromotionType)promo.PromotionType),
                PromotionType = promo.PromotionType,
                PromotionCode = promo.PromotionCode,
                DiscountValue =
                    (Constant.GetEnumDescription((Constant.PromotionType)promo.PromotionType) == Constant.GetEnumDescription(Constant.PromotionType.DiscountTotalOnPercentage) || Constant.GetEnumDescription((Constant.PromotionType)promo.PromotionType) == Constant.GetEnumDescription(Constant.PromotionType.DiscountShipping)) ? $"{promo.Percentage}%" : $"${promo.Dollars}",
                StartDate = promo.StartDate,
                StartDateDisplay = promo.StartDate.HasValue ? Helper.ConvertDateTimeString(promo.StartDate.Value) : string.Empty,
                EndDate = promo.EndDate,
                EndDateDisplay = promo.EndDate.HasValue ? Helper.ConvertDateTimeString(promo.EndDate.Value) : string.Empty,
                Description = promo.Description,
                Percentage = promo.Percentage,
                Dollars = promo.Dollars,
                LimitTime = promo.LimitTime,
                IsLive = promo.IsLive,
                UsedTime = promo.ShoppingCarts.Count() + promo.ShoppingCarts1.Count(),
                AutoApply = promo.AutoApply,
                CreatedAt = promo.CreatedAt.HasValue ? Helper.ConvertDateTimeString(promo.CreatedAt.Value) : string.Empty,
                UpdatedAt = promo.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(promo.UpdatedAt.Value) : string.Empty,
                CreatedBy = promo.CreatedBy,
                UpdatedBy = promo.UpdatedBy,
            };

            return model;
        }

        public static ProductModel ConvertProductToProductModel(Product product, IQueryable<UserProfile> user = null)
        {
            List<LikedProductModel> likedProductList = new List<LikedProductModel>();
            if (product.LikedProducts.Any())
            {
                foreach (LikedProduct lp in product.LikedProducts)
                {
                    likedProductList.Add(new LikedProductModel()
                    {
                        Id = lp.Id,
                        ProductId = lp.ProductId,
                        UserId = lp.UserId
                    });
                }
            }

            List<ReviewedProductModel> reviewedProductList = new List<ReviewedProductModel>();

            if (user != null)
            {
                if (product.ReviewedProducts.Any())
                {
                    foreach (ReviewedProduct rp in product.ReviewedProducts)
                    {
                        reviewedProductList.Add(new ReviewedProductModel()
                        {
                            Id = rp.Id,
                            ProductId = rp.ProductId,
                            UserId = rp.UserId,
                            Comment = rp.Comment,
                            Rating = rp.Rating,
                            UserAvatar = user.FirstOrDefault(x => x.UserId == rp.UserId).Avatar,
                            UserFullname = string.Format("{0} {1}", user.FirstOrDefault(x => x.UserId == rp.UserId).FirstName, user.FirstOrDefault(x => x.UserId == rp.UserId).LastName),
                            CreatedAt = rp.CreatedAt.HasValue ? Helper.ConvertDateTimeString(rp.CreatedAt.Value) : string.Empty
                        });
                    }
                }
            }

            ProductModel model = new ProductModel
            {
                Id = product.Id,
                OriginalProductCode = product.OriginalProductCode,
                Namekey = product.Namekey,
                ColorCode = product.ColorCode,
                ColorCode1 = product.ColorCode1,
                ColorCode2 = product.ColorCode2,
                ProductCode = product.ProductCode,
                Weight = product.Weight,
                Height = product.Height,
                Width = product.Width,
                Length = product.Length,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                SubCategoryId1 = product.SubCategoryId1,
                SubCategoryName1 = product.SubCategory1 != null ? product.SubCategory1.Name : string.Empty,
                SubCategoryId2 = product.SubCategoryId2,
                SubCategoryName2 = product.SubCategory2 != null ? product.SubCategory2.Name : string.Empty,
                Name = product.Name,
                Description = product.Description,
                MoreInfo = product.MoreInfo,
                CreatedAt = product.CreatedAt.HasValue ? Helper.ConvertDateTimeString(product.CreatedAt.Value) : string.Empty,
                UpdatedAt = product.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(product.UpdatedAt.Value) : string.Empty,
                CreatedBy = product.CreatedBy,
                UpdatedBy = product.UpdatedBy,
                Unit = product.Unit,
                UnitOfPrice = product.UnitOfPrice,
                ImportPriceUSD = product.ImportPriceUSD,
                ImportPrice = product.ImportPrice,
                OriginalPrice = product.OriginalPrice,
                DiscountPrice = product.DiscountPrice,
                InStock = product.InStock,
                StockLevel = product.StockLevel,
                IsLive = product.IsLive,
                NumberOfRating = product.NumberOfRating,
                NumberOfLiked = product.NumberOfLiked,
                NumberOfReviewed = product.NumberOfReviewed,
                AverageRating = product.AverageRating,
                PrimaryImage = product.PrimaryImage,
                SubImage1 = product.SubImage1,
                SubImage2 = product.SubImage2,
                SubImage3 = product.SubImage3,
                SubImage4 = product.SubImage4,
                SubImage5 = product.SubImage5,
                LikedProduct = likedProductList,
                ReviewedProduct = reviewedProductList,
                ExchangeRate = product.ExchangeRate,
                OriginalRate = product.OriginalRate,
                DiscountRate = product.DiscountRate,
                Materials = product.Materials,
                Colors = product.Colors,
                QRCode = product.ShareLink,
                GroupSizeId = product.GroupSizeId,
                GroupColorId = product.GroupColorId,
                SizeName = product.SizeName
            };

            return model;
        }

        public static ApplicationSettingModel ConvertApplicationSettingToContentSettingModel(ApplicationSetting setting)
        {
            ApplicationSettingModel model = new ApplicationSettingModel
            {
                Id = setting.Id,
                StoreName = setting.StoreName,
                Address = setting.Address,
                Phone = setting.Phone,
                Email = setting.Email,
                NewslettersDescription = setting.NewslettersDescription,
                BannerQuote1 = setting.BannerQuote1,
                BannerQuote2 = setting.BannerQuote2,
                BannerQuote3 = setting.BannerQuote3,
                BannerImage1 = setting.BannerImage1,
                BannerImage2 = setting.BannerImage2,
                BannerImage3 = setting.BannerImage3,
                PromotionImage1 = setting.PromotionImage1,
                PromotionImage2 = setting.PromotionImage2,
                PromotionImage3 = setting.PromotionImage3,
                PromotionImage4 = setting.PromotionImage4,
                PromotionImage5 = setting.PromotionImage5,
                PromotionImage6 = setting.PromotionImage6,
                AdsIcon1 = setting.AdsIcon1,
                AdsTitle1 = setting.AdsTitle1,
                AdsIcon2 = setting.AdsIcon2,
                AdsTitle2 = setting.AdsTitle2,
                AdsIcon3 = setting.AdsIcon3,
                AdsTitle3 = setting.AdsTitle3,
                AdsIcon4 = setting.AdsIcon4,
                AdsTitle4 = setting.AdsTitle4,
                AdsIcon5 = setting.AdsIcon5,
                AdsTitle5 = setting.AdsTitle5,
                AdsIcon6 = setting.AdsIcon6,
                AdsTitle6 = setting.AdsTitle6,
                ServiceIcon1 = setting.ServiceIcon1,
                ServiceTitle1 = setting.ServiceTitle1,
                ServiceDescription1 = setting.ServiceDescription1,
                ServiceIcon2 = setting.ServiceIcon2,
                ServiceTitle2 = setting.ServiceTitle2,
                ServiceDescription2 = setting.ServiceDescription2,
                ServiceIcon3 = setting.ServiceIcon3,
                ServiceTitle3 = setting.ServiceTitle3,
                ServiceDescription3 = setting.ServiceDescription3,
                ServiceIcon4 = setting.ServiceIcon4,
                ServiceTitle4 = setting.ServiceTitle4,
                ServiceDescription4 = setting.ServiceDescription4,
                PartnerLogo1 = setting.PartnerLogo1,
                PartnerLogo2 = setting.PartnerLogo2,
                PartnerLogo3 = setting.PartnerLogo3,
                PartnerLogo4 = setting.PartnerLogo4,
                TermsAndConditions = setting.TermsAndConditions,
                FAQ = setting.FAQ,
                ReturnPolicy = setting.ReturnPolicy,
                AdminEmail = setting.AdminEmail,
                CustomerServiceEmail = setting.CustomerServiceEmail,
                ECommerceEmail = setting.ECommerceEmail,
                PaymentReceiptEmail = setting.PaymentReceiptEmail,
                GSTPercent = setting.GSTPercent,
                CreditCardSurcharge = setting.CreditCardSurcharge,
                PaypalSurcharge = setting.PaypalSurcharge,
                FreeShippingAusPostFrom = setting.FreeShippingAusPostFrom,
                FreeShippingHomeMailFrom = setting.FreeShippingHomeMailFrom,
                AusPostDescription = setting.AusPostDescription,
                HomeMailDescription = setting.HomeMailDescription,
                HomeMailDefaultPrice1 = setting.HomeMailDefaultPrice1,
                HomeMailDefaultPrice2 = setting.HomeMailDefaultPrice2,
                HomeMailDefaultPrice3 = setting.HomeMailDefaultPrice3,
                PopularSearchTag = setting.PopularSearchTag,
                ClickAndCollectDescription = setting.ClickAndCollectDescription,
                MetaDescription = setting.MetaDescription,
                AboutUs = setting.AboutUs,
                ClickAndCollectPage = setting.ClickAndCollectPage,
                HomeMailPage = setting.HomeMailPage,
                FacebookLink = setting.FacebookLink,
                InstagramLink = setting.InstagramLink
            };

            return model;
        }

        public static ApplicationSettingModel ConvertApplicationSettingToPolicySettingModel(ApplicationSetting setting)
        {
            ApplicationSettingModel model =
                new ApplicationSettingModel
                {
                    TermsAndConditions = setting.TermsAndConditions,
                    FAQ = setting.FAQ,
                    ReturnPolicy = setting.ReturnPolicy,
                    AboutUs = setting.AboutUs,
                    ClickAndCollectPage = setting.ClickAndCollectPage,
                    HomeMailPage = setting.HomeMailPage
                };

            return model;
        }

        public static ApplicationSettingModel ConvertApplicationSettingToAppConfigurationModel(ApplicationSetting setting)
        {
            ApplicationSettingModel model = new ApplicationSettingModel
            {
                AdminEmail = setting.AdminEmail,
                CustomerServiceEmail = setting.CustomerServiceEmail,
                ECommerceEmail = setting.ECommerceEmail,
                PaymentReceiptEmail = setting.PaymentReceiptEmail,
                GSTPercent = setting.GSTPercent,
                CreditCardSurcharge = setting.CreditCardSurcharge,
                PaypalSurcharge = setting.PaypalSurcharge,
                FreeShippingAusPostFrom = setting.FreeShippingAusPostFrom,
                FreeShippingHomeMailFrom = setting.FreeShippingHomeMailFrom,
                AusPostDescription = setting.AusPostDescription,
                HomeMailDescription = setting.HomeMailDescription,
                HomeMailDefaultPrice1 = setting.HomeMailDefaultPrice1,
                HomeMailDefaultPrice2 = setting.HomeMailDefaultPrice2,
                HomeMailDefaultPrice3 = setting.HomeMailDefaultPrice3,
                HomeMailAvailableFrom = setting.HomeMailAvailableFrom,
                WarehousePostcode = setting.WarehousePostcode,
                PopularSearchTag = setting.PopularSearchTag,
                CurrencyRateUSDToAUD = setting.CurrencyRateUSDToAUD,
                RateCalculateOriginalPrice = setting.RateCalculateOriginalPrice,
                RateCalculateDiscountPrice = setting.RateCalculateDiscountPrice,
                ClickAndCollectDescription = setting.ClickAndCollectDescription,
                MetaDescription = setting.MetaDescription,
                FacebookLink = setting.FacebookLink,
                InstagramLink = setting.InstagramLink,
                DatabaseVersion = setting.DatabaseVersion,
                ServerInformation = Utility.Helper.GetSystemInformation()
            };

            return model;
        }

        public static ShoppingCartModel ConvertShoppingCartToShoppingCartModel(ShoppingCart cart)
        {
            ShoppingCartModel model = new ShoppingCartModel
            {
                Id = cart.Id,
                TotalImportPrice = 0,
                TotalProfit = 0,
                TotalAmountExGst = cart.TotalAmountExGst,
                TotalAmountIncGst = cart.TotalAmountIncGst,
                Gst = cart.Gst,
                ShippingFee = cart.ShippingFee,
                Discount = cart.Discount,
                TotalFinalPrice = cart.TotalFinalPrice,
                EstimateDelivery = cart.EstimateDelivery,
                FreeShippingHomeMailFrom = cart.FreeShippingHomeMailFrom,
                FreeShippingAusPostFrom = cart.FreeShippingAusPostFrom,
                HomeMailAvailableFrom = cart.HomeMailAvailableFrom,
                GSTPercent = cart.GSTPercent,
                CreditCardSurcharge = cart.CreditCardSurcharge,
                PaypalSurcharge = cart.PayPalSurcharge,
                CreatedAt = cart.CreatedAt.HasValue ? Helper.ConvertDateTimeString(cart.CreatedAt.Value) : string.Empty,
                UpdatedAt = cart.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(cart.UpdatedAt.Value) : string.Empty,
                CreatedBy = cart.CreatedBy,
                UpdatedBy = cart.UpdatedBy,
                PromotionCode = cart.Promotion != null ? cart.Promotion.PromotionCode : string.Empty,
                ShippingPromotionCode = cart.ShippingPromotion != null ? cart.ShippingPromotion.PromotionCode : string.Empty,
                PromotionId = cart.PromotionId ?? 0,
                ShippingPromotionId = cart.ShippingPromotionId ?? 0,
                HomeMailAvailable = (cart.TotalAmountExGst + cart.Gst) >= cart.HomeMailAvailableFrom,
                HomeMailDefaultPrice1 = cart.HomeMailDefaultPrice1,
                HomeMailDefaultPrice2 = cart.HomeMailDefaultPrice2,
                HomeMailDefaultPrice3 = cart.HomeMailDefaultPrice3,
                WarehousePostcode = cart.WarehousePostcode
            };

            if (cart.CartItems != null && cart.CartItems.Any())
            {
                List<CartItemModel> itemList = new List<CartItemModel>();
                foreach (CartItem i in cart.CartItems)
                {
                    itemList.Add(ConvertCartItemToCartItemModel(i));
                    model.TotalImportPrice += i.Product != null ? i.Product.ImportPrice * i.Quantity : 0;
                }

                model.TotalImportPrice = Math.Round(model.TotalImportPrice, 2);
                model.CartItemList = itemList;
                model.TotalProfit = Math.Round(model.TotalAmountIncGst - model.TotalImportPrice, 2);
            }

            if (cart.Promotion != null && cart.PromotionId != 0)
            {
                model.Promotion = ConvertPromotionToPromotionModel(cart.Promotion);
            }

            return model;
        }

        public static CartItemModel ConvertCartItemToCartItemModel(CartItem item)
        {
            CartItemModel model = new CartItemModel
            {
                Id = item.Id,
                CartId = item.CartId,
                ProductId = item.ProductId,
                Name = item.Name,
                Description = item.Description,
                Unit = item.Unit,
                UnitOfPrice = item.UnitOfPrice,
                Quantity = item.Quantity,
                ImportPrice = item.Product != null ? item.Product.ImportPrice : 0,
                OriginalPrice = item.OriginalPrice,
                DiscountPrice = item.DiscountPrice,
                FinalPrice = item.FinalPrice,
                PrimaryImage = item.PrimaryImage,
                Note = item.Note,
                CreatedAt = item.CreatedAt.HasValue ? Helper.ConvertDateTimeString(item.CreatedAt.Value) : string.Empty,
                UpdatedAt = item.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(item.UpdatedAt.Value) : string.Empty,
                CreatedBy = item.CreatedBy,
                UpdatedBy = item.UpdatedBy
            };

            return model;
        }

        public static List<CartItemModel> ConvertListCartItemToListCartItemModel(ICollection<CartItem> list)
        {
            List<CartItemModel> result = new List<CartItemModel>();

            foreach (CartItem item in list)
            {
                result.Add(ConvertCartItemToCartItemModel(item));
            }

            return result;
        }

        public static OrderModel ConvertOrderToOrderModel(Order order, UserProfile user = null)
        {
            OrderModel model = new OrderModel
            {
                Checkout = new CheckoutModel
                {
                    Id = order.Id,
                    CartId = order.CartId,
                    UserId = order.UserId ?? 0,
                    UserAvatar = order.UserId != 0 && user != null ? user.Avatar : Constant.DefaultImagePath.DefaultGenericAvatar,
                    UserFullName = order.UserId != 0 && user != null ? user.UserName : "Non User",
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    CompanyName = !string.IsNullOrEmpty(order.CompanyName) ? order.CompanyName : string.Empty,
                    Address = order.Address,
                    Suburb = order.Suburb,
                    Postcode = order.Postcode,
                    State = order.State,
                    Country = order.Country,
                    Email = !string.IsNullOrEmpty(order.Email) ? order.Email : string.Empty,
                    Phone = !string.IsNullOrEmpty(order.Phone) ? order.Phone : string.Empty,
                    Note = !string.IsNullOrEmpty(order.Note) ? order.Note : string.Empty,
                    TrackingNumber = order.TrackingNumber,
                    DeliveryToDifferentAddress = (order.FirstName != order.DeliveryFirstName || order.LastName != order.DeliveryLastName || order.Address != order.DeliveryAddress || order.Postcode != order.DeliveryPostcode),
                    ShippingMethod = !string.IsNullOrEmpty(order.ShippingMethod) ? order.ShippingMethod : string.Empty,
                    DeliveryFirstName = order.DeliveryFirstName,
                    DeliveryLastName = order.DeliveryLastName,
                    DeliveryCompanyName = order.DeliveryCompanyName,
                    DeliveryAddress = order.DeliveryAddress,
                    DeliverySuburb = order.DeliverySuburb,
                    DeliveryPostcode = order.DeliveryPostcode,
                    DeliveryState = order.DeliveryState,
                    DeliveryCountry = order.DeliveryCountry,
                    ConfirmedStatus = order.ConfirmedStatus,
                    CreatedAt = order.CreatedAt.HasValue ? Helper.ConvertDateTimeString(order.CreatedAt.Value) : string.Empty,
                    UpdatedAt = order.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(order.UpdatedAt.Value) : string.Empty,
                    CreatedBy = order.CreatedBy,
                    UpdatedBy = order.UpdatedBy,
                    CompletedAt = order.CompletedAt.HasValue ? Helper.ConvertDateTimeString(order.CompletedAt.Value) : string.Empty
                }
            };

            if (order.ShoppingCart != null)
            {
                model.ShoppingCart = ConvertShoppingCartToShoppingCartModel(order.ShoppingCart);
            }

            if (order.Payments != null && order.Payments.Any())
            {
                model.PaymentList = ConvertPaymentsToPaymentList(order.Payments);
            }

            return model;
        }

        public static PaymentModel ConvertPaymentToPaymentModel(Payment payment)
        {
            PaymentModel model = new PaymentModel
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Method = payment.Method,
                Amount = payment.Amount,
                Success = payment.Success,
                IsFull = payment.IsFull,
                Reason = payment.Reason,
                Detail = payment.Detail,
                IPAddress = payment.IPAddress,
                CreatedAt = payment.CreatedAt.HasValue ? Helper.ConvertDateTimeString(payment.CreatedAt.Value) : string.Empty,
                UpdatedAt = payment.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(payment.UpdatedAt.Value) : string.Empty,
                CreatedBy = payment.CreatedBy,
                UpdatedBy = payment.UpdatedBy
            };

            return model;
        }

        public static List<PaymentModel> ConvertListPaymentToListPaymentModel(ICollection<Payment> list)
        {
            List<PaymentModel> result = new List<PaymentModel>();

            foreach (Payment item in list)
            {
                result.Add(ConvertPaymentToPaymentModel(item));
            }

            return result;
        }

        public static List<PaymentModel> ConvertPaymentsToPaymentList(ICollection<Payment> payments)
        {
            return payments.Select(ConvertPaymentToPaymentModel).ToList();
        }

        public static UserProfileModel ConvertUserProfileToUserProfileModel(UserProfile user)
        {
            UserProfileModel model = new UserProfileModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                GenderDisplay = user.Gender ? Constant.CommonValue.Male : Constant.CommonValue.Female,
                CompanyName = user.CompanyName,
                Address = user.Address,
                Suburb = user.Suburb,
                Postcode = user.Postcode,
                State = user.State,
                Country = user.Country,
                Email = user.Email,
                Phone = user.Phone,
                IsLive = user.IsLive,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt.HasValue ? Helper.ConvertDateTimeString(user.CreatedAt.Value) : string.Empty,
                UpdatedAt = user.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(user.UpdatedAt.Value) : string.Empty,
                CreatedBy = user.CreatedBy,
                UpdatedBy = user.UpdatedBy
            };

            return model;
        }

        public static EVoucherModel ConvertEVoucherToEVoucherModel(EVoucher voucher)
        {
            EVoucherModel model = new EVoucherModel
            {
                Id = voucher.Id,
                FromFirstName = voucher.FromFirstName,
                FromLastName = voucher.FromLastName,
                FromEmail = voucher.FromEmail,
                FromPhone = voucher.FromPhone,
                Message = voucher.Message,
                Amount = voucher.Amount,
                Balance = voucher.Balance,
                IsGift = voucher.IsGift,
                ToFirstName = voucher.ToFirstName,
                ToLastName = voucher.ToLastName,
                ToEmail = voucher.ToEmail,
                ToPhone = voucher.ToPhone,
                EVoucherSerialNo = voucher.EVoucherSerialNo,
                ActivationCode = voucher.ActivationCode,
                IsLive = voucher.IsLive,
                CreatedAt = voucher.CreatedAt.HasValue ? Helper.ConvertDateTimeString(voucher.CreatedAt.Value) : string.Empty,
                UpdatedAt = voucher.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(voucher.UpdatedAt.Value) : string.Empty,
                CreatedBy = voucher.CreatedBy,
                UpdatedBy = voucher.UpdatedBy
            };

            return model;
        }

        public static WishlistModel ConvertWishlistToWishlistModel(Wishlist wishlist, Product product)
        {
            WishlistModel model = new WishlistModel()
            {
                Id = wishlist.Id,
                ProductId = wishlist.ProductId,
                UserId = wishlist.UserId,
                Product = ConvertProductToProductModel(product)
            };

            return model;
        }

        public static LikedProductModel ConvertWishlistToWishlistModel(LikedProduct likeProduct)
        {
            LikedProductModel model = new LikedProductModel()
            {
                Id = likeProduct.Id,
                ProductId = likeProduct.ProductId,
                UserId = likeProduct.UserId
            };

            return model;
        }

        public static PreOrderModel ConvertPreOrderToPreOrderModel(PreOrder preorder)
        {
            PreOrderModel model = new PreOrderModel()
            {
                Id = preorder.Id,
                ProductId = preorder.ProductId,
                ProductName = string.Format("{0} - {1}", preorder.ProductId, preorder.Product.Name),
                ProductImage = preorder.Product.PrimaryImage,
                Username = preorder.UserProfile != null ? string.Format("{0} - {1} {2}", preorder.UserId, preorder.UserProfile.FirstName, preorder.UserProfile.LastName) : string.Empty,
                UserId = preorder.UserId,
                FirstName = preorder.FirstName,
                LastName = preorder.LastName,
                Email = preorder.Email,
                Phone = preorder.Phone,
                Status = preorder.Status,
                Note = preorder.Note,
                CustomerEscalate = preorder.CustomerEscalate,
                CreatedAt = preorder.CreatedAt.HasValue ? Helper.ConvertDateTimeString(preorder.CreatedAt.Value) : string.Empty,
                UpdatedAt = preorder.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(preorder.UpdatedAt.Value) : string.Empty,
                CreatedBy = preorder.CreatedBy,
                UpdatedBy = preorder.UpdatedBy
            };

            return model;
        }

        public static OrderForExportModel ConvertOrderToOrderForExportModel(Order order)
        {
            OrderForExportModel model = new OrderForExportModel
            {
                OrderId = order.Id,
                CartId = order.CartId,
                UserId = order.UserId ?? 0,
                FirstName = order.FirstName,
                LastName = order.LastName,
                CompanyName = !string.IsNullOrEmpty(order.CompanyName) ? order.CompanyName : string.Empty,
                Address = order.Address,
                Suburb = order.Suburb,
                Postcode = order.Postcode,
                State = order.State,
                Country = order.Country,
                Email = !string.IsNullOrEmpty(order.Email) ? order.Email : string.Empty,
                Phone = !string.IsNullOrEmpty(order.Phone) ? order.Phone : string.Empty,
                Note = !string.IsNullOrEmpty(order.Note) ? order.Note : string.Empty,
                TrackingNumber = order.TrackingNumber,
                DeliveryToDifferentAddress = (order.FirstName != order.DeliveryFirstName || order.LastName != order.DeliveryLastName || order.Address != order.DeliveryAddress || order.Postcode != order.DeliveryPostcode),
                ShippingMethod = !string.IsNullOrEmpty(order.ShippingMethod) ? order.ShippingMethod : string.Empty,
                DeliveryFirstName = order.DeliveryFirstName,
                DeliveryLastName = order.DeliveryLastName,
                DeliveryCompanyName = order.DeliveryCompanyName,
                DeliveryAddress = order.DeliveryAddress,
                DeliverySuburb = order.DeliverySuburb,
                DeliveryPostcode = order.DeliveryPostcode,
                DeliveryState = order.DeliveryState,
                DeliveryCountry = order.DeliveryCountry,
                ConfirmedStatus = order.ConfirmedStatus,
                OrderCreatedAt = order.CreatedAt.HasValue ? Helper.ConvertDateTimeString(order.CreatedAt.Value) : string.Empty,
                OrderUpdatedAt = order.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(order.UpdatedAt.Value) : string.Empty,
                OrderCreatedBy = order.CreatedBy,
                OrderUpdatedBy = order.UpdatedBy,
                CompletedAt = order.CompletedAt.HasValue ? Helper.ConvertDateTimeString(order.CompletedAt.Value) : string.Empty
            };

            if (order.ShoppingCart != null)
            {
                model.TotalAmountExGst = order.ShoppingCart.TotalAmountExGst;
                model.TotalAmountIncGst = order.ShoppingCart.TotalAmountIncGst;
                model.Gst = order.ShoppingCart.Gst;
                model.ShippingFee = order.ShoppingCart.ShippingFee;
                model.Discount = order.ShoppingCart.Discount;
                model.TotalFinalPrice = order.ShoppingCart.TotalFinalPrice;
                model.EstimateDelivery = order.ShoppingCart.EstimateDelivery;
                model.FreeShippingHomeMailFrom = order.ShoppingCart.FreeShippingHomeMailFrom;
                model.FreeShippingAusPostFrom = order.ShoppingCart.FreeShippingAusPostFrom;
                model.HomeMailAvailableFrom = order.ShoppingCart.HomeMailAvailableFrom;
                model.GSTPercent = order.ShoppingCart.GSTPercent;
                model.CreditCardSurcharge = order.ShoppingCart.CreditCardSurcharge;
                model.PaypalSurcharge = order.ShoppingCart.PayPalSurcharge;
                model.ShoppingCreatedAt = order.ShoppingCart.CreatedAt.HasValue ? Helper.ConvertDateTimeString(order.ShoppingCart.CreatedAt.Value) : string.Empty;
                model.ShoppingUpdatedAt = order.ShoppingCart.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(order.ShoppingCart.UpdatedAt.Value) : string.Empty;
                model.ShoppingCreatedBy = order.ShoppingCart.CreatedBy;
                model.ShoppingUpdatedBy = order.ShoppingCart.UpdatedBy;
                model.PromotionCode = order.ShoppingCart.Promotion != null ? order.ShoppingCart.Promotion.PromotionCode : string.Empty;
                model.ShippingPromotionCode = order.ShoppingCart.ShippingPromotion != null ? order.ShoppingCart.ShippingPromotion.PromotionCode : string.Empty;
                model.PromotionId = order.ShoppingCart.PromotionId ?? 0;
                model.ShippingPromotionId = order.ShoppingCart.ShippingPromotionId ?? 0;
                model.HomeMailAvailable = (order.ShoppingCart.TotalAmountExGst + order.ShoppingCart.Gst) >= order.ShoppingCart.HomeMailAvailableFrom;
                model.HomeMailDefaultPrice1 = order.ShoppingCart.HomeMailDefaultPrice1;
                model.HomeMailDefaultPrice2 = order.ShoppingCart.HomeMailDefaultPrice2;
                model.HomeMailDefaultPrice3 = order.ShoppingCart.HomeMailDefaultPrice3;
                model.WarehousePostcode = order.ShoppingCart.WarehousePostcode;
            }

            if (order.Payments.Any())
            {
                Payment payment = order.Payments.LastOrDefault();

                model.LastPaymentId = payment.Id;
                model.LastPaymentMethod = payment.Method;
                model.LastPaymentAmount = payment.Amount;
                model.LastPaymentSuccess = payment.Success;
                model.LastPaymentIsFull = payment.IsFull;
                model.LastPaymentReason = payment.Reason;
                model.LastPaymentDetail = payment.Detail;
                model.LastPaymentIPAddress = payment.IPAddress;
                model.LastPaymentCreatedAt = payment.CreatedAt.HasValue ? Helper.ConvertDateTimeString(payment.CreatedAt.Value) : string.Empty;
                model.LastPaymentCreatedBy = payment.CreatedBy;
                model.LastPaymentUpdatedAt = payment.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(payment.UpdatedAt.Value) : string.Empty;
                model.LastPaymentUpdatedBy = payment.UpdatedBy;
            }

            return model;
        }

        public static NewsletterTemplateModel ConvertNewsletterTemplateToNewsletterTemplateModel(NewsletterTemplate template)
        {
            NewsletterTemplateModel model = new NewsletterTemplateModel
            {
                Id = template.Id,
                TemplateName = template.TemplateName,
                Description = template.Description,
                TemplateSubject = template.TemplateSubject,
                TemplateContent = template.TemplateContent,
                CreatedAt = template.CreatedAt.HasValue ? Helper.ConvertDateTimeString(template.CreatedAt.Value) : string.Empty,
                UpdatedAt = template.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(template.UpdatedAt.Value) : string.Empty,
                CreatedBy = template.CreatedBy,
                UpdatedBy = template.UpdatedBy
            };

            return model;
        }

        public static OurEventModel ConvertOurEventToOurEventModel(OurEvent ourEvent)
        {
            OurEventModel model = new OurEventModel
            {
                Id = ourEvent.Id,
                EventName = ourEvent.EventName,
                Brief = ourEvent.Brief,
                Avatar = ourEvent.Avatar,
                Description = ourEvent.Description,
                Location = ourEvent.Location,
                IsLive = ourEvent.IsLive,
                Time = ourEvent.Time,
                TimeDisplay = ourEvent.Time.HasValue ? Helper.ConvertDateTime(ourEvent.Time.Value).ToString("dddd, dd MMMM yyyy hh:mm tt") : string.Empty,
                CreatedAt = ourEvent.CreatedAt.HasValue ? Helper.ConvertDateTimeString(ourEvent.CreatedAt.Value) : string.Empty,
                UpdatedAt = ourEvent.UpdatedAt.HasValue ? Helper.ConvertDateTimeString(ourEvent.UpdatedAt.Value) : string.Empty,
                CreatedBy = ourEvent.CreatedBy,
                UpdatedBy = ourEvent.UpdatedBy
            };

            return model;
        }
    }
}
