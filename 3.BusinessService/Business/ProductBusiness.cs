using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Utility;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UserService.RoleService;

namespace BusinessService.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IRoleService _roleService;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProductBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory, IRoleService roleService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _roleService = roleService;
        }

        #region Implementation of IProductBusiness

        public CollectionModel<ProductModel> GetAllProductsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    int pageNo = parameter.PageNo;
                    string sortBy = parameter.SortBy;

                    IQueryable<Product> list;
                    list = !string.IsNullOrEmpty(parameter.Search) || Math.Abs(parameter.MinPrice) > 0 || Math.Abs(parameter.MaxPrice - 1000) > 0
                    ? parameter.Search.Equals("Sale") ? uow.Repository<Product>().AsNoTracking().Where(x => x.IsLive == Constant.EntityStatus.Active && x.OriginalPrice > x.DiscountPrice) : uow.Repository<Product>().AsNoTracking().Where(x => x.IsLive == Constant.EntityStatus.Active &&
                                                                          (x.Name.Trim().Contains(parameter.Search.Trim()) ||
                                                                           x.Namekey.Trim().Contains(parameter.Search.Trim()) ||
                                                                           x.Description.Trim().Contains(parameter.Search.Trim()) ||
                                                                           x.Id.ToString().Contains(parameter.Search.Trim()) ||
                                                                           x.Category.Name.Trim().Contains(parameter.Search.Trim()) ||
                                                                           x.SubCategory1.Name.Trim().Contains(parameter.Search.Trim()) ||
                                                                           x.SubCategory2.Name.Trim().Contains(parameter.Search.Trim()))
                                                                          && x.DiscountPrice >= parameter.MinPrice &&
                                                                          x.DiscountPrice <= parameter.MaxPrice)
                    : uow.Repository<Product>().AsNoTracking().Where(x => x.IsLive == Constant.EntityStatus.Active);

                    if (sortBy.Equals(Constant.CommonValue.SortByPriceLowToHigh))
                    {
                        list = list.OrderBy(s => s.DiscountPrice);
                    }
                    else if (sortBy.Equals(Constant.CommonValue.SortByPriceHighToLow))
                    {
                        list = list.OrderByDescending(s => s.DiscountPrice);
                    }
                    else
                    {
                        list = list.OrderByDescending(s => s.Id);
                    }

                    int totalItems = list.Count();
                    int totalPages = (totalItems % parameter.PageSize) == 0 ? (totalItems / parameter.PageSize) : (totalItems / parameter.PageSize) + 1;
                    list = list.Skip(parameter.PageSize * (parameter.PageNo - 1)).Take(parameter.PageSize);

                    List<ProductModel> modelList = new List<ProductModel>();

                    foreach (Product p in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertProductToProductModel(p));
                    }

                    return new CollectionModel<ProductModel>
                    {
                        Data = modelList,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        CurrenPage = pageNo,
                    };
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public CollectionModel<ProductModel> GetAllProductsByFilterForAdmin(Parameter parameter)
        {
            try
            {
                string search = parameter.Search;
                int pageNo = parameter.PageNo;

                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    IQueryable<Product> list;

                    if (!string.IsNullOrEmpty(search) || Math.Abs(parameter.MinPrice) > 0 || Math.Abs(parameter.MaxPrice - 500) > 0)
                    {
                        list = uow.Repository<Product>().AsNoTracking().Where(x =>
                        (x.OriginalProductCode.Trim().Contains(search.Trim()) || x.Name.Trim().Contains(search.Trim()) || x.Namekey.Trim().Contains(parameter.Search.Trim()) || x.ProductCode.Trim().Contains(parameter.Search.Trim()) || x.OriginalProductCode.Trim().Contains(parameter.Search.Trim()) || x.Description.Trim().Contains(search.Trim()) || x.Id.ToString().Contains(search) || x.Category.Name.Trim().Contains(search.Trim()) || x.SubCategory1.Name.Trim().Contains(parameter.Search.Trim()) || x.SubCategory2.Name.Trim().Contains(parameter.Search.Trim())) && x.DiscountPrice >= parameter.MinPrice && x.DiscountPrice <= parameter.MaxPrice);
                    }
                    else
                    {
                        list = uow.Repository<Product>().AsNoTracking();
                    }

                    if (parameter.SearchByLive == Constant.CommonValue.IsLive)
                    {
                        list = list.Where(x => x.IsLive);
                    }
                    else if (parameter.SearchByLive == Constant.CommonValue.NotLive)
                    {
                        list = list.Where(x => !x.IsLive);
                    }

                    if (parameter.SearchByStockLevel == Constant.CommonValue.InStock)
                    {
                        list = list.Where(x => x.StockLevel > 2);
                    }
                    else if (parameter.SearchByStockLevel == Constant.CommonValue.LowStock)
                    {
                        list = list.Where(x => x.StockLevel > 0 && x.StockLevel <= 2);
                    }
                    else if (parameter.SearchByStockLevel == Constant.CommonValue.OutOfStock)
                    {
                        list = list.Where(x => x.StockLevel <= 0);
                    }

                    list = list.OrderByDescending(s => s.Id);

                    int totalItems = list.Count();
                    int totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;
                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<ProductModel> modelList = new List<ProductModel>();

                    foreach (Product p in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertProductToProductModel(p));
                    }

                    return new CollectionModel<ProductModel>
                    {
                        Data = modelList,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        CurrenPage = pageNo,
                    };
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<ProductModel> GetAllProducts()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Product> products = uow.Repository<Product>().AsNoTracking().Where(x => x.StockLevel > 0).OrderBy(x => x.Namekey).ToList();
                    List<ProductModel> modelList = new List<ProductModel>();

                    if (products.Any())
                    {
                        foreach (Product product in products)
                        {
                            modelList.Add(Utility.Mapping.ConvertProductToProductModel(product));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<WishlistModel> GetAllWishlistProducts(int userId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    IOrderedQueryable<Wishlist> wishlist = uow.Repository<Wishlist>().AsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.Id);

                    if (wishlist == null)
                    {
                        return null;
                    }

                    List<WishlistModel> wlistModel = new List<WishlistModel>();

                    foreach (Wishlist wl in wishlist)
                    {
                        wlistModel.Add(Utility.Mapping.ConvertWishlistToWishlistModel(wl, wl.Product));
                    }

                    return wlistModel;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<ProductModel> GetLatestProductsForHomePage()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Product> products = uow.Repository<Product>().AsNoTracking().Where(x => x.IsLive == Constant.EntityStatus.Active && (x.InStock.Equals(Constant.CommonValue.InStock) || x.InStock.Equals(Constant.CommonValue.LowStock))).OrderByDescending(x => x.Id).Take(12).ToList();
                    List<ProductModel> modelList = new List<ProductModel>();

                    if (products.Any())
                    {
                        foreach (Product product in products)
                        {
                            modelList.Add(Utility.Mapping.ConvertProductToProductModel(product));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<ProductModel> GetRelatedProductsForDetailPage(int categoryId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Product> products = uow.Repository<Product>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive) && (x.CategoryId == categoryId || x.SubCategoryId1 == categoryId || x.SubCategoryId2 == categoryId)).OrderByDescending(x => x.Id).Take(6).ToList();
                    List<ProductModel> modelList = new List<ProductModel>();

                    if (products.Any())
                    {
                        foreach (Product product in products)
                        {
                            modelList.Add(Utility.Mapping.ConvertProductToProductModel(product));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public ProductModel Insert(ProductModel product)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product insert = new Product()
                    {
                        Name = product.Name,
                        OriginalProductCode = product.OriginalProductCode,
                        Namekey = product.Namekey,
                        ColorCode = product.ColorCode,
                        ColorCode1 = product.ColorCode1,
                        ColorCode2 = product.ColorCode2,
                        ProductCode = $"{product.Namekey}.{product.ColorCode}",
                        Weight = product.Weight,
                        Length = product.Length,
                        Height = product.Height,
                        Width = product.Width,
                        Description = product.Description,
                        MoreInfo = product.MoreInfo,
                        CategoryId = product.CategoryId,
                        Unit = product.Unit,
                        UnitOfPrice = product.UnitOfPrice,
                        ImportPrice = product.ImportPrice,
                        OriginalPrice = product.OriginalPrice,
                        DiscountPrice = product.DiscountPrice,
                        IsLive = product.IsLive,
                        InStock = product.InStock = product.StockLevel > 2 ? Constant.CommonValue.InStock : product.StockLevel > 0 ? Constant.CommonValue.LowStock : Constant.CommonValue.OutOfStock,
                        StockLevel = product.StockLevel,
                        PrimaryImage = product.PrimaryImage,
                        SubImage1 = product.SubImage1,
                        SubImage2 = product.SubImage2,
                        SubImage3 = product.SubImage3,
                        SubImage4 = product.SubImage4,
                        SubImage5 = product.SubImage5,
                        NumberOfRating = 0,
                        NumberOfLiked = 0,
                        AverageRating = 0,
                        ExchangeRate = product.ExchangeRate,
                        OriginalRate = product.OriginalRate,
                        DiscountRate = product.DiscountRate,
                        ImportPriceUSD = product.ImportPriceUSD,
                        SubCategoryId1 = product.SubCategoryId1,
                        SubCategoryId2 = product.SubCategoryId2,
                        Materials = product.Materials,
                        Colors = product.Colors,
                        GroupSizeId = product.GroupSizeId,
                        GroupColorId = product.GroupColorId,
                        SizeName = product.SizeName
                    };

                    Product inserted = uow.Repository<Product>().Add(insert);

                    uow.SaveChanges();

                    Product result = uow.Repository<Product>().Where(x => x.Id == inserted.Id).Include("Category").FirstOrDefault();

                    ProductModel model = Utility.Mapping.ConvertProductToProductModel(result);

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

        public ProductModel Update(ProductModel product)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product update = uow.Repository<Product>().FirstOrDefault(x => x.Id == product.Id);

                    if (update != null)
                    {
                        update.OriginalProductCode = product.OriginalProductCode;
                        update.Namekey = !string.IsNullOrEmpty(product.Namekey) ? product.Namekey : update.Namekey;
                        update.ColorCode = !string.IsNullOrEmpty(product.ColorCode) ? product.ColorCode : update.ColorCode;
                        update.ColorCode1 = product.ColorCode1;
                        update.ColorCode2 = product.ColorCode2;
                        update.ProductCode = $"{product.Namekey}.{product.ColorCode}";
                        update.Weight = product.Weight;
                        update.Length = product.Length;
                        update.Height = product.Height;
                        update.Width = product.Width;
                        update.Name = !string.IsNullOrEmpty(product.Name) ? product.Name : update.Name;
                        update.Description = product.Description;
                        update.MoreInfo = product.MoreInfo;
                        update.CategoryId = product.CategoryId != 0 ? product.CategoryId : update.CategoryId;
                        update.Unit = !string.IsNullOrEmpty(product.Unit) ? product.Unit : update.Unit;
                        update.UnitOfPrice = !string.IsNullOrEmpty(product.UnitOfPrice) ? product.UnitOfPrice : update.UnitOfPrice;
                        update.ImportPrice = product.ImportPrice;
                        update.OriginalPrice = product.OriginalPrice;
                        update.DiscountPrice = product.DiscountPrice;
                        update.InStock = product.InStock = product.StockLevel > 2 ? Constant.CommonValue.InStock : product.StockLevel > 0 ? Constant.CommonValue.LowStock : Constant.CommonValue.OutOfStock;
                        update.StockLevel = product.StockLevel;
                        update.IsLive = product.IsLive;
                        update.PrimaryImage = !string.IsNullOrEmpty(product.PrimaryImage) && product.PrimaryImage != Constant.DefaultImagePath.DefaultProduct ? product.PrimaryImage : update.PrimaryImage;
                        update.SubImage1 = !string.IsNullOrEmpty(product.SubImage1) ? product.SubImage1 : update.SubImage1;
                        update.SubImage2 = !string.IsNullOrEmpty(product.SubImage2) ? product.SubImage2 : update.SubImage2;
                        update.SubImage3 = !string.IsNullOrEmpty(product.SubImage3) ? product.SubImage3 : update.SubImage3;
                        update.SubImage4 = !string.IsNullOrEmpty(product.SubImage4) ? product.SubImage4 : update.SubImage4;
                        update.SubImage5 = !string.IsNullOrEmpty(product.SubImage5) ? product.SubImage5 : update.SubImage5;
                        update.ExchangeRate = product.ExchangeRate;
                        update.OriginalRate = product.OriginalRate;
                        update.DiscountRate = product.DiscountRate;
                        update.ImportPriceUSD = product.ImportPriceUSD;
                        update.SubCategoryId1 = product.SubCategoryId1;
                        update.SubCategoryId2 = product.SubCategoryId2;
                        update.Materials = product.Materials;
                        update.Colors = product.Colors;
                        update.ShareLink = product.QRCode;
                        update.GroupSizeId = product.GroupSizeId;
                        update.GroupColorId = product.GroupColorId;
                        update.SizeName = product.SizeName;

                        if (update.InStock == Constant.CommonValue.OutOfStock)
                        {
                            update.DiscountPrice = update.OriginalPrice;
                            update.DiscountRate = update.OriginalRate;
                        }

                        uow.SaveChanges();

                        ProductModel model = Utility.Mapping.ConvertProductToProductModel(update);

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

        public ProductModel UpdateQRCode(ProductModel product)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product update = uow.Repository<Product>().FirstOrDefault(x => x.Id == product.Id);

                    if (update != null)
                    {
                        update.ShareLink = product.QRCode;

                        uow.SaveChanges();

                        ProductModel model = Utility.Mapping.ConvertProductToProductModel(update);

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

        public ProductModel Details(int id, int userId = 0)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product detail = uow.Repository<Product>().AsNoTracking().Include("LikedProduct").Include("ReviewedProduct").Include("Wishlist").FirstOrDefault(x => x.Id == id);

                    IQueryable<UserProfile> user = uow.Repository<UserProfile>().AsNoTracking();

                    if (detail == null)
                    {
                        return null;
                    }

                    detail.NumberOfLiked = uow.Repository<LikedProduct>().Count(x => x.ProductId == id);
                    detail.NumberOfRating = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == id && x.Rating > 0);
                    detail.NumberOfReviewed = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == id);
                    detail.AverageRating = uow.Repository<ReviewedProduct>().Where(x => x.ProductId == id && x.Rating > 0).Any() ? uow.Repository<ReviewedProduct>().Where(x => x.ProductId == id && x.Rating > 0).Average(x => x.Rating) : 0;
                    detail.AverageRating = Math.Round(detail.AverageRating, 1);

                    uow.SaveChanges();

                    ProductModel model = Mapping.ConvertProductToProductModel(detail, user);
                    model.RatingStatistic = CalculateRatingStatistic(model);
                    model.ReviewedProduct = model.ReviewedProduct.OrderBy(x => x.Id).ToList();

                    if (userId != 0)
                    {
                        model.IsLikedForCurrentUser = detail.LikedProducts.Any(x => x.ProductId == id && x.UserId == userId);
                        model.IsInWishlist = detail.Wishlists.Any(x => x.ProductId == id && x.UserId == userId);
                        model.IsReviewedForCurrentUser = detail.ReviewedProducts.Any(x => x.ProductId == id && x.UserId == userId);

                        UserProfile currentUser = user.FirstOrDefault(x => x.UserId == userId);

                        if (currentUser != null)
                        {
                            if (_roleService.IsUserInRole(currentUser.UserName, Constant.Roles.Administrator))
                            {
                                Product nextProduct = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id > id);
                                Product previousProduct = uow.Repository<Product>().AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id < id);

                                model.NextProductId = nextProduct != null ? nextProduct.Id : 0;
                                model.PreviousProductId = previousProduct != null ? previousProduct.Id : 0;
                            }
                        }

                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public string GenerateNameKey(int catId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Category cat = uow.Repository<Category>().AsNoTracking().Where(x => x.Id == catId).FirstOrDefault();

                    if (cat == null || string.IsNullOrEmpty(cat.PrefixForProductCode))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        Product lastest = uow.Repository<Product>().AsNoTracking().Where(x => x.Namekey.Contains(cat.PrefixForProductCode)).OrderByDescending(x => x.CreatedAt).FirstOrDefault();

                        if (lastest == null)
                        {
                            return $"{cat.PrefixForProductCode}1001";
                        }

                        string number = lastest.Namekey.Replace(cat.PrefixForProductCode, string.Empty);

                        int latestId = Convert.ToInt32(number);

                        return $"{cat.PrefixForProductCode}{latestId + 1}";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product product = uow.Repository<Product>().FirstOrDefault(x => x.Id == id);

                    if (product == null)
                    {
                        throw new DataLayerException("The product is not found in the system");
                    }
                    else
                    {
                        if (product.CartItems.Any() || product.LikedProducts.Any() || product.ReviewedProducts.Any())
                        {
                            throw new DataLayerException("The product can not be deleted, it is being used in the system!");
                        }
                    }

                    uow.Repository<Product>().Remove(product);

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool DeleteWishlist(int wishlistId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Wishlist wishlist = uow.Repository<Wishlist>().FirstOrDefault(x => x.Id == wishlistId);

                    uow.Repository<Wishlist>().Remove(wishlist);

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool AddProductToWishlist(int userId, int productId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Wishlist existing = uow.Repository<Wishlist>().SingleOrDefault(x => x.ProductId == productId && x.UserId == userId);

                    if (existing != null)
                    {
                        throw new DataLayerException("This product is already existing in your wishlist!");
                    }

                    Wishlist insert = new Wishlist()
                    {
                        UserId = userId,
                        ProductId = productId
                    };

                    Wishlist inserted = uow.Repository<Wishlist>().Add(insert);

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public ProductModel LikeProduct(int userId, int productId, out bool result)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    LikedProduct existing = uow.Repository<LikedProduct>().SingleOrDefault(x => x.ProductId == productId && x.UserId == userId);
                    LikedProduct insert = new LikedProduct();

                    //Like or unlike
                    if (existing != null)
                    {
                        uow.Repository<LikedProduct>().Remove(existing);
                        result = false;
                    }
                    else
                    {
                        insert.UserId = userId;
                        insert.ProductId = productId;
                        uow.Repository<LikedProduct>().Add(insert);
                        result = true;
                    }

                    uow.SaveChanges();

                    //Re-count like number
                    Product detail = uow.Repository<Product>().AsNoTracking().FirstOrDefault(x => x.Id == productId);
                    detail.NumberOfLiked = uow.Repository<LikedProduct>().Count(x => x.ProductId == productId);

                    uow.SaveChanges();

                    ProductModel model = Utility.Mapping.ConvertProductToProductModel(detail);

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

        public ProductModel ReviewProduct(int userId, int productId, int rating, string comment)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ReviewedProduct insert = new ReviewedProduct()
                    {
                        UserId = userId,
                        ProductId = productId,
                        Comment = comment,
                        Rating = rating
                    };

                    uow.Repository<ReviewedProduct>().Add(insert);

                    uow.SaveChanges();

                    //Re-count review and rating number
                    Product detail = uow.Repository<Product>().AsNoTracking().Include("LikedProduct").Include("ReviewedProduct").Include("UserProfiles").Include("Wishlist").FirstOrDefault(x => x.Id == productId);

                    IQueryable<UserProfile> user = uow.Repository<UserProfile>().AsNoTracking();

                    detail.NumberOfReviewed = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == productId);
                    detail.NumberOfRating = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == productId && x.Rating > 0);
                    detail.AverageRating = uow.Repository<ReviewedProduct>().Where(x => x.ProductId == productId && x.Rating > 0).Any() ? uow.Repository<ReviewedProduct>().Where(x => x.ProductId == productId && x.Rating > 0).Average(x => x.Rating) : 0;
                    detail.AverageRating = Math.Round(detail.AverageRating, 1);
                    uow.SaveChanges();

                    ProductModel model = Utility.Mapping.ConvertProductToProductModel(detail, user);
                    model.RatingStatistic = CalculateRatingStatistic(model);
                    model.ReviewedProduct = model.ReviewedProduct.OrderBy(x => x.Id).ToList();
                    model.IsLikedForCurrentUser = detail.LikedProducts.Any(x => x.ProductId == productId && x.UserId == userId);
                    model.IsInWishlist = detail.Wishlists.Any(x => x.ProductId == productId && x.UserId == userId);
                    model.IsReviewedForCurrentUser = detail.ReviewedProducts.Any(x => x.ProductId == productId && x.UserId == userId);

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

        public bool RemoveReviewProduct(int id, int userId, int productId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    ReviewedProduct review = uow.Repository<ReviewedProduct>().FirstOrDefault(x => x.Id == id && x.UserId == userId && x.ProductId == productId);

                    if (review == null)
                    {
                        return true;
                    }

                    uow.Repository<ReviewedProduct>().Remove(review);

                    uow.SaveChanges();

                    Product detail = uow.Repository<Product>().AsNoTracking().Include("LikedProduct").Include("ReviewedProduct").Include("UserProfiles").Include("Wishlist").FirstOrDefault(x => x.Id == productId);

                    IQueryable<UserProfile> user = uow.Repository<UserProfile>().AsNoTracking();

                    detail.NumberOfReviewed = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == productId);
                    detail.NumberOfRating = uow.Repository<ReviewedProduct>().Count(x => x.ProductId == productId && x.Rating > 0);
                    detail.AverageRating = uow.Repository<ReviewedProduct>().Where(x => x.ProductId == productId && x.Rating > 0).Any() ? uow.Repository<ReviewedProduct>().Where(x => x.ProductId == productId && x.Rating > 0).Average(x => x.Rating) : 0;
                    detail.AverageRating = Math.Round(detail.AverageRating, 1);
                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public async Task<bool> Delete(List<int> ids)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Product> items = await uow.Repository<Product>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (Product item in items)
                    {
                        uow.Repository<Product>().Remove(item);
                    }

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        private RatingStatisticModel CalculateRatingStatistic(ProductModel productModel)
        {
            try
            {
                return new RatingStatisticModel()
                {
                    OneStar = string.Format("{0}%", CalculatePercentOfRate(1, productModel)),
                    TwoStar = string.Format("{0}%", CalculatePercentOfRate(2, productModel)),
                    ThreeStar = string.Format("{0}%", CalculatePercentOfRate(3, productModel)),
                    FourStar = string.Format("{0}%", CalculatePercentOfRate(4, productModel)),
                    FiveStar = string.Format("{0}%", CalculatePercentOfRate(5, productModel)),
                    OneStarCount = productModel.ReviewedProduct.Where(x => x.Rating == 1).Count(),
                    TwoStarCount = productModel.ReviewedProduct.Where(x => x.Rating == 2).Count(),
                    ThreeStarCount = productModel.ReviewedProduct.Where(x => x.Rating == 3).Count(),
                    FourStarCount = productModel.ReviewedProduct.Where(x => x.Rating == 4).Count(),
                    FiveStarCount = productModel.ReviewedProduct.Where(x => x.Rating == 5).Count()
                };
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        private double CalculatePercentOfRate(int star, ProductModel productModel)
        {
            if (productModel.ReviewedProduct.Where(x => x.Rating == star).Any())
            {
                double percent = (productModel.ReviewedProduct.Where(x => x.Rating == star).Count() / (double)productModel.ReviewedProduct.Where(x => x.Rating > 0).Count() * 100);
                return Math.Round(percent, 2);
            }
            else
            {
                return 0;
            }
        }

        public void ApplyDiscount(int categoryId, double percent, bool applyForSubCategory)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    IQueryable<Product> list;

                    if (categoryId == 0)
                    {
                        list = uow.Repository<Product>();
                    }
                    else
                    {
                        if (applyForSubCategory)
                        {
                            list = uow.Repository<Product>().Where(x => x.CategoryId == categoryId || x.SubCategoryId1 == categoryId || x.SubCategoryId2 == categoryId);
                        }
                        else
                        {
                            list = uow.Repository<Product>().Where(x => x.CategoryId == categoryId);
                        }
                    }

                    if (list == null)
                    {
                        return;
                    }

                    foreach (Product item in list)
                    {
                        double discount = Math.Round((item.OriginalPrice * percent / 100), 2);
                        item.DiscountPrice = Math.Round(item.OriginalPrice - discount, 2);
                    }

                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
            }
        }

        public IList<ProductModel> GetProductsInSameGroup(int groupId, int productId)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Product> products = uow.Repository<Product>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive) && (x.GroupSizeId == groupId || x.GroupColorId == groupId)).OrderByDescending(x => x.Id).ToList();

                    List<ProductModel> modelList = new List<ProductModel>();

                    if (products.Any())
                    {
                        foreach (Product product in products)
                        {
                            modelList.Add(Utility.Mapping.ConvertProductToProductModel(product));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public bool CheckValidate(string productName)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Product color = uow.Repository<Product>().FirstOrDefault(x => x.Name.ToLower() == productName.ToLower());

                    if (color == null)
                    {
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

        #endregion
    }
}
