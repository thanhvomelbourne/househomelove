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

namespace BusinessService.Business
{
    public class OurEventBusiness : IOurEventBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OurEventBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IOurEventBusiness

        public CollectionModel<OurEventModel> GetAllOurEventsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<OurEvent> list = !string.IsNullOrEmpty(search) ? uow.Repository<OurEvent>().AsNoTracking().Where(x => (x.Brief.Contains(search) || x.EventName.Contains(search) || x.Description.Contains(search) || x.Id.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<OurEvent>().AsNoTracking();

                    if (parameter.SearchByLive == Constant.CommonValue.IsLive)
                    {
                        list = list.Where(x => x.IsLive);
                    }
                    else if (parameter.SearchByLive == Constant.CommonValue.NotLive)
                    {
                        list = list.Where(x => !x.IsLive);
                    }

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<OurEventModel> modelList = new List<OurEventModel>();

                    foreach (var c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertOurEventToOurEventModel(c));
                    }

                    return new CollectionModel<OurEventModel>
                    {
                        Data = modelList,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        CurrenPage = pageNo,
                    };
                }
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<OurEventModel> GetAllOurEvents()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<OurEvent> categories = uow.Repository<OurEvent>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive)).OrderBy(x => x.Id).ToList();

                    if (categories.Any())
                    {
                        List<OurEventModel> modelList = new List<OurEventModel>();

                        foreach (var category in categories)
                        {
                            modelList.Add(Mapping.ConvertOurEventToOurEventModel(category));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public OurEventModel Insert(OurEventModel eventModel)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    OurEvent insert = new OurEvent()
                    {
                        EventName = eventModel.EventName,
                        Brief = eventModel.Brief,
                        Avatar = eventModel.Avatar,
                        Description = eventModel.Description,
                        IsLive = eventModel.IsLive,
                        Location = eventModel.Location,
                        Time = eventModel.Time.HasValue ? TimeZoneInfo.ConvertTimeToUtc(eventModel.Time.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null,
                    };

                    OurEvent inserted = uow.Repository<OurEvent>().Add(insert);

                    uow.SaveChanges();

                    OurEventModel model = Mapping.ConvertOurEventToOurEventModel(inserted);

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

        public OurEventModel Update(OurEventModel eventModel)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    OurEvent update = uow.Repository<OurEvent>().FirstOrDefault(x => x.Id == eventModel.Id);

                    if (update != null)
                    {
                        update.EventName = !string.IsNullOrEmpty(eventModel.EventName) ? eventModel.EventName : update.EventName;
                        update.Brief = !string.IsNullOrEmpty(eventModel.Brief) ? eventModel.Brief : update.Brief;
                        update.Description = !string.IsNullOrEmpty(eventModel.Description) ? eventModel.Description : update.Description;
                        update.IsLive = eventModel.IsLive;
                        update.Avatar = !string.IsNullOrEmpty(eventModel.Avatar) ? eventModel.Avatar : update.Avatar;
                        update.Location = eventModel.Location;
                        update.Time = eventModel.Time.HasValue ? TimeZoneInfo.ConvertTimeToUtc(eventModel.Time.Value, Constant.CommonValue.AustralianEasternTimeZoneInfo) : (DateTime?)null;

                        uow.SaveChanges();

                        OurEventModel model = Mapping.ConvertOurEventToOurEventModel(update);

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

        public OurEventModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    OurEvent detail = uow.Repository<OurEvent>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        OurEventModel model = Mapping.ConvertOurEventToOurEventModel(detail);

                        return model;
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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    OurEvent eve = uow.Repository<OurEvent>().FirstOrDefault(x => x.Id == id);

                    if (eve == null)
                    {
                        throw new DataLayerException("The event is not found in the system");
                    }

                    uow.Repository<OurEvent>().Remove(eve);

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
                    var categories = await uow.Repository<OurEvent>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in categories)
                    {
                        uow.Repository<OurEvent>().Remove(item);
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

        #endregion
    }
}
