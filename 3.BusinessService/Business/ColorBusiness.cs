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
    public class ColorBusiness : IColorBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ColorBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IColorBusiness

        public CollectionModel<ColorModel> GetAllColorsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Color> list = !string.IsNullOrEmpty(search) ? uow.Repository<Color>().AsNoTracking().Where(x => (x.ColorName.Contains(search) || x.ColorCode.Contains(search) || x.Description.Contains(search) || x.Id.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<Color>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<ColorModel> modelList = new List<ColorModel>();

                    foreach (var c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertColorToColorModel(c));
                    }

                    return new CollectionModel<ColorModel>
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

        public IList<ColorModel> GetAllColors()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<Color> colors = uow.Repository<Color>().AsNoTracking().OrderBy(x => x.ColorName).ToList();

                    if (colors.Any())
                    {
                        List<ColorModel> modelList = new List<ColorModel>();

                        foreach (var color in colors)
                        {
                            modelList.Add(Utility.Mapping.ConvertColorToColorModel(color));
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

        public ColorModel Insert(ColorModel color)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Color insert = new Color()
                    {
                        ColorName = color.ColorName,
                        ColorCode = color.ColorCode,
                        Description = color.Description
                    };

                    Color inserted = uow.Repository<Color>().Add(insert);

                    uow.SaveChanges();

                    ColorModel model = Utility.Mapping.ConvertColorToColorModel(inserted);

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

        public ColorModel Update(ColorModel color)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Color update = uow.Repository<Color>().FirstOrDefault(x => x.Id == color.Id);

                    if (update != null)
                    {
                        update.ColorName = !string.IsNullOrEmpty(color.ColorName) ? color.ColorName : update.ColorName;
                        update.Description = !string.IsNullOrEmpty(color.Description) ? color.Description : update.Description;
                        update.ColorCode = !string.IsNullOrEmpty(color.ColorCode) ? color.ColorCode : update.ColorCode;

                        uow.SaveChanges();

                        ColorModel model = Utility.Mapping.ConvertColorToColorModel(update);

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

        public ColorModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Color detail = uow.Repository<Color>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        ColorModel model = Utility.Mapping.ConvertColorToColorModel(detail);

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
                    Color color = uow.Repository<Color>().FirstOrDefault(x => x.Id == id);

                    if (color == null)
                    {
                        throw new DataLayerException("The color is not found in the system");
                    }

                    uow.Repository<Color>().Remove(color);

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

        public bool CheckValidate(string colorName)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Color color = uow.Repository<Color>().FirstOrDefault(x => x.ColorName.ToLower() == colorName.ToLower());

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

        public async Task<bool> Delete(List<int> ids)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    var colors = await uow.Repository<Color>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in colors)
                    {
                        uow.Repository<Color>().Remove(item);
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
