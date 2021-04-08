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
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CategoryBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of ICategoryBusiness

        public CollectionModel<CategoryModel> GetAllCategoriesByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Category> list = !string.IsNullOrEmpty(search) ? uow.Repository<Category>().AsNoTracking().Where(x => (x.Name.Contains(search) || x.Description.Contains(search) || x.Id.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<Category>().AsNoTracking();

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

                    List<CategoryModel> modelList = new List<CategoryModel>();

                    foreach (var c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertCategoryToCategoryModel(c));
                    }

                    return new CollectionModel<CategoryModel>
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

        public IList<CategoryModel> GetAllCategories()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Category> categories = uow.Repository<Category>().AsNoTracking().Where(x => !x.IsLive.Equals(Constant.EntityStatus.Inactive)).OrderBy(x => x.Id).ToList();

                    if (categories.Any())
                    {
                        List<CategoryModel> modelList = new List<CategoryModel>();

                        foreach (var category in categories)
                        {
                            modelList.Add(Utility.Mapping.ConvertCategoryToCategoryModel(category));
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

        public IList<CategoryModel> GetAllCategoriesForAdmin()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Category> categories = uow.Repository<Category>().AsNoTracking().Where(x => x.Name != "System" && x.Name != "Sale").OrderBy(x => x.Id).ToList();
                    List<CategoryModel> modelList = new List<CategoryModel>();

                    if (categories.Any())
                    {
                        foreach (var category in categories)
                        {
                            modelList.Add(Utility.Mapping.ConvertCategoryToCategoryModel(category));
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

        public CategoryModel Insert(CategoryModel category)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Category insert = new Category()
                    {
                        Name = category.Name,
                        Description = category.Description,
                        IsLive = category.IsLive,
                        PrefixForProductCode = category.PrefixForProductCode
                    };

                    Category inserted = uow.Repository<Category>().Add(insert);

                    uow.SaveChanges();

                    CategoryModel model = Utility.Mapping.ConvertCategoryToCategoryModel(inserted);

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

        public CategoryModel Update(CategoryModel category)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Category update = uow.Repository<Category>().FirstOrDefault(x => x.Id == category.Id);

                    if (update != null)
                    {
                        update.Name = !string.IsNullOrEmpty(category.Name) ? category.Name : update.Name;
                        update.Description = !string.IsNullOrEmpty(category.Description) ? category.Description : update.Description;
                        update.IsLive = category.IsLive;
                        update.PrefixForProductCode = category.PrefixForProductCode;

                        uow.SaveChanges();

                        CategoryModel model = Utility.Mapping.ConvertCategoryToCategoryModel(update);

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

        public CategoryModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Category detail = uow.Repository<Category>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        CategoryModel model = Utility.Mapping.ConvertCategoryToCategoryModel(detail);

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
                    Category category = uow.Repository<Category>().FirstOrDefault(x => x.Id == id);

                    if (category == null)
                    {
                        throw new DataLayerException("The category is not found in the system");
                    }
                    else
                    {
                        if (category.Products.Any())
                        {
                            throw new DataLayerException("The category can not be deleted, it is being used in the system !");
                        }
                    }

                    uow.Repository<Category>().Remove(category);

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
                    var categories = await uow.Repository<Category>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in categories)
                    {
                        uow.Repository<Category>().Remove(item);
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
