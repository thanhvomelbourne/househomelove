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
    public class GroupBusiness : IGroupBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GroupBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of ICategoryBusiness

        public CollectionModel<GroupModel> GetAllGroupsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Group> list = !string.IsNullOrEmpty(search) ? uow.Repository<Group>().AsNoTracking().Where(x => (x.GroupName.Contains(search) || x.GroupType.ToString().Contains(search) || x.Id.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<Group>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<GroupModel> modelList = new List<GroupModel>();

                    foreach (var g in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertGroupToCategoryModel(g));
                    }

                    return new CollectionModel<GroupModel>
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

        public IList<GroupModel> GetAllGroups()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Group> categories = uow.Repository<Group>().AsNoTracking().OrderBy(x => x.Id).ToList();

                    if (categories.Any())
                    {
                        List<GroupModel> modelList = new List<GroupModel>();

                        foreach (var group in categories)
                        {
                            modelList.Add(Utility.Mapping.ConvertGroupToCategoryModel(group));
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
        
        public GroupModel Insert(GroupModel group)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Group insert = new Group()
                    {
                        GroupType = group.GroupType,
                        GroupName = group.GroupName
                    };

                    Group inserted = uow.Repository<Group>().Add(insert);

                    uow.SaveChanges();

                    GroupModel model = Utility.Mapping.ConvertGroupToCategoryModel(inserted);

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

        public GroupModel Update(GroupModel group)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Group update = uow.Repository<Group>().FirstOrDefault(x => x.Id == group.Id);

                    if (update != null)
                    {
                        update.GroupName = !string.IsNullOrEmpty(group.GroupName) ? group.GroupName : update.GroupName;
                        update.GroupType = group.GroupType;

                        uow.SaveChanges();

                        GroupModel model = Utility.Mapping.ConvertGroupToCategoryModel(update);

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

        public GroupModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Group detail = uow.Repository<Group>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        GroupModel model = Utility.Mapping.ConvertGroupToCategoryModel(detail);

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
                    Group group = uow.Repository<Group>().FirstOrDefault(x => x.Id == id);

                    if (group == null)
                    {
                        throw new DataLayerException("The group is not found in the system");
                    }
                    else
                    {
                        if (group.Products1.Any() || group.Products11.Any())
                        {
                            throw new DataLayerException("The group can not be deleted, it is being used in the system !");
                        }
                    }

                    uow.Repository<Group>().Remove(group);

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
                    var groups = await uow.Repository<Group>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in groups)
                    {
                        uow.Repository<Group>().Remove(item);
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
