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
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Business
{
    public class PreOrderBusiness : IPreOrderBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PreOrderBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IOrderBusiness

        public PreOrderModel InsertPreOrder(PreOrderModel preorder)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    if (preorder.UserId != null && preorder.UserId > 0)
                    {
                        UserProfile user = uow.Repository<UserProfile>().FirstOrDefault(o => o.UserId == preorder.UserId);
                        preorder.FirstName = user.FirstName;
                        preorder.LastName = user.LastName;
                        preorder.Email = user.Email;
                        preorder.Phone = user.Phone;
                    }

                    PreOrder existing = uow.Repository<PreOrder>().FirstOrDefault(o => o.FirstName == preorder.FirstName && o.LastName == preorder.LastName && o.Email == preorder.Email && o.Phone == preorder.Phone && o.ProductId == preorder.ProductId && !o.Status.Equals(Constant.PreOrderStatus.ClosePreOrder));

                    PreOrder insert = new PreOrder()
                    {
                        FirstName = preorder.FirstName,
                        LastName = preorder.LastName,
                        Email = preorder.Email,
                        Phone = preorder.Phone,
                        ProductId = preorder.ProductId,
                        UserId = preorder.UserId,
                        Status = Constant.PreOrderStatus.ReceivedPreOrder,
                        CustomerEscalate = 0
                    };

                    if (existing != null)
                    {
                        existing.CustomerEscalate += 1;
                        uow.SaveChanges();

                        PreOrder result = uow.Repository<PreOrder>().Where(x => x.Id == existing.Id).Include("Product").FirstOrDefault();

                        PreOrderModel model = Utility.Mapping.ConvertPreOrderToPreOrderModel(result);

                        return model;
                    }
                    else
                    {
                        PreOrder inserted = uow.Repository<PreOrder>().Add(insert);
                        uow.SaveChanges();

                        PreOrder result = uow.Repository<PreOrder>().Where(x => x.Id == inserted.Id).Include("Product").FirstOrDefault();

                        PreOrderModel model = Utility.Mapping.ConvertPreOrderToPreOrderModel(result);

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public PreOrderModel UpdatePreOrder(PreOrderModel preorder)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    PreOrder existing = uow.Repository<PreOrder>().FirstOrDefault(x => x.Id == preorder.Id);

                    if (existing == null)
                    {
                        throw new DataLayerException("The pre-order is not found in the system!");
                    }

                    existing.Status = preorder.Status;
                    existing.Note = preorder.Note;
                    uow.SaveChanges();

                    PreOrder result = uow.Repository<PreOrder>().Where(x => x.Id == existing.Id).Include("Product").FirstOrDefault();

                    PreOrderModel model = Utility.Mapping.ConvertPreOrderToPreOrderModel(result);

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

        public CollectionModel<PreOrderModel> GetAllPreOrdersByFilter(Parameter parameter)
        {
            try
            {
                string search = parameter.Search;
                int pageNo = parameter.PageNo;

                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    IQueryable<PreOrder> list;

                    if (!string.IsNullOrEmpty(search))
                    {
                        list = uow.Repository<PreOrder>().AsNoTracking().Where(x =>
                        x.UserId.ToString().Contains(search) ||
                        x.ProductId.ToString().Contains(search) ||
                        x.Id.ToString().Contains(search) ||
                        x.FirstName.Contains(search) ||
                        x.LastName.Contains(search) ||
                        x.Email.Contains(search) ||
                        x.Phone.Contains(search));
                    }
                    else
                    {
                        list = uow.Repository<PreOrder>().AsNoTracking();
                    }

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;
                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<PreOrderModel> modelList = new List<PreOrderModel>();

                    foreach (var p in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertPreOrderToPreOrderModel(p));
                    }

                    return new CollectionModel<PreOrderModel>
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

        public PreOrderModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    PreOrder detail = uow.Repository<PreOrder>().AsNoTracking().Include("Product").FirstOrDefault(x => x.Id == id);

                    if (detail == null) return null;

                    PreOrderModel model = Utility.Mapping.ConvertPreOrderToPreOrderModel(detail);

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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    PreOrder preorder = uow.Repository<PreOrder>().FirstOrDefault(x => x.Id == id);

                    if (preorder == null)
                    {
                        throw new DataLayerException("The pre-order is not found in the system!");
                    }
                    else
                    {
                        uow.Repository<PreOrder>().Remove(preorder);
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
