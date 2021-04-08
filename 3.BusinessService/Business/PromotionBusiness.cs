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

namespace BusinessService.Business
{
    public class PromotionBusiness : IPromotionBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PromotionBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IPromotionBusiness

        public IList<PromotionModel> GetAllPromotionsByType(int type)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Promotion> promotions = uow.Repository<Promotion>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive) && x.PromotionType == type).OrderBy(x => x.Id).ToList();

                    if (promotions.Any())
                    {
                        List<PromotionModel> modelList = new List<PromotionModel>();

                        foreach (Promotion promo in promotions)
                        {
                            modelList.Add(Utility.Mapping.ConvertPromotionToPromotionModel(promo));
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

        public CollectionModel<PromotionModel> GetAllPromotionsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    int totalItems = 0;
                    int totalPages = 0;
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Promotion> list = !string.IsNullOrEmpty(search) ? uow.Repository<Promotion>().AsNoTracking().Where(x => (x.NameOfPromotion.Contains(search) || x.Description.Contains(search) || Constant.GetEnumDescription(Constant.PromotionType.DiscountTotalOnPercentage).Contains(search) || Constant.GetEnumDescription(Constant.PromotionType.DiscountTotalOnDollars).Contains((search)) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<Promotion>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    totalItems = list.Count();
                    totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<PromotionModel> modelList = new List<PromotionModel>();

                    foreach (Promotion c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertPromotionToPromotionModel(c));
                    }

                    return new CollectionModel<PromotionModel>
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

        public PromotionModel Insert(PromotionModel promotion)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    double? discount = !string.IsNullOrEmpty(promotion.DiscountValue) ? Math.Round(Convert.ToDouble(promotion.DiscountValue), 2) : 0;

                    Promotion insert = new Promotion()
                    {
                        PromotionType = promotion.PromotionType,
                        NameOfPromotion = promotion.NameOfPromotion,
                        Description = promotion.Description,
                        PromotionCode = promotion.PromotionCode,
                        Percentage = (promotion.PromotionType == Constant.PromotionType.DiscountTotalOnPercentage.GetHashCode() || promotion.PromotionType == Constant.PromotionType.DiscountShipping.GetHashCode()) ? discount : null,
                        Dollars = promotion.PromotionType == Constant.PromotionType.DiscountTotalOnDollars.GetHashCode() ? discount : null,
                        AutoApply = promotion.AutoApply,
                        LimitTime = promotion.LimitTime,
                        StartDate = promotion.StartDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(promotion.StartDate.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null,
                        EndDate = promotion.EndDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(promotion.EndDate.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null,
                        IsLive = promotion.IsLive
                    };

                    Promotion inserted = uow.Repository<Promotion>().Add(insert);

                    uow.SaveChanges();

                    PromotionModel model = Utility.Mapping.ConvertPromotionToPromotionModel(inserted);

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

        public PromotionModel Update(PromotionModel promotion)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    double? discount = !string.IsNullOrEmpty(promotion.DiscountValue) ? Math.Round(Convert.ToDouble(promotion.DiscountValue), 2) : 0;

                    Promotion update = uow.Repository<Promotion>().FirstOrDefault(x => x.Id == promotion.Id);

                    if (update == null)
                    {
                        return null;
                    }

                    update.PromotionType = promotion.PromotionType;
                    update.NameOfPromotion = !string.IsNullOrEmpty(promotion.NameOfPromotion) ? promotion.NameOfPromotion : update.NameOfPromotion;
                    update.PromotionCode = !string.IsNullOrEmpty(promotion.PromotionCode) ? promotion.PromotionCode : update.PromotionCode;
                    update.Description = !string.IsNullOrEmpty(promotion.Description) ? promotion.Description : update.Description;
                    update.Percentage = (promotion.PromotionType == Constant.PromotionType.DiscountTotalOnPercentage.GetHashCode() || promotion.PromotionType == Constant.PromotionType.DiscountShipping.GetHashCode()) ? discount : null;
                    update.Dollars = promotion.PromotionType == Constant.PromotionType.DiscountTotalOnDollars.GetHashCode() ? discount : null;
                    update.AutoApply = promotion.AutoApply;
                    update.LimitTime = promotion.LimitTime;
                    update.StartDate = promotion.StartDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(promotion.StartDate.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null;
                    update.EndDate = promotion.EndDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(promotion.EndDate.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null;
                    update.IsLive = promotion.IsLive;

                    uow.SaveChanges();

                    PromotionModel model = Utility.Mapping.ConvertPromotionToPromotionModel(update);

                    return model;

                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public PromotionModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Promotion detail = uow.Repository<Promotion>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail == null)
                    {
                        return null;
                    }

                    PromotionModel model = Utility.Mapping.ConvertPromotionToPromotionModel(detail);

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

        public PromotionModel GetPromotionByPromotionCode(string promotionCode, bool isAdmin = false)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Promotion detail = uow.Repository<Promotion>().AsNoTracking().FirstOrDefault(x => x.PromotionCode == promotionCode);

                    if (detail == null)
                    {
                        return null;
                    }

                    if (detail.LimitTime != null && !isAdmin)
                    {
                        if (detail.LimitTime <= 0)
                        {
                            throw new DataLayerException("Your promotion code is not valid at the moment. Please try again later.");
                        }
                    }

                    if (detail.StartDate.HasValue && detail.EndDate.HasValue && detail.IsLive && !isAdmin)
                    {
                        int afterStartDate = DateTime.Compare(detail.StartDate.Value, DateTime.UtcNow);
                        int beforeEndDateDate = DateTime.Compare(DateTime.UtcNow, detail.EndDate.Value);

                        if (afterStartDate > 0)
                        {
                            throw new DataLayerException("Your promotion code is not valid at the moment. Please try again later.");
                        }
                        else if (beforeEndDateDate > 0)
                        {
                            throw new DataLayerException("Your promotion code is expired!");
                        }
                    }

                    PromotionModel model = Utility.Mapping.ConvertPromotionToPromotionModel(detail);

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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Promotion promo = uow.Repository<Promotion>().FirstOrDefault(x => x.Id == id);

                    if (promo == null)
                    {
                        throw new DataLayerException("The category is not found in the system");
                    }

                    if (promo.ShoppingCarts.Any())
                    {
                        throw new DataLayerException("The category can not be deleted, it is used in the system !");
                    }

                    uow.Repository<Promotion>().Remove(promo);

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

        #endregion
    }
}
