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
    public class SubcriptionBusiness : ISubcriptionBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SubcriptionBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public CollectionModel<SubcriptionModel> GetAllSubscriptionsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Subcription> list = !string.IsNullOrEmpty(search) ? uow.Repository<Subcription>().AsNoTracking().Where(x => (x.EmailSubcribed.Contains(search))) : uow.Repository<Subcription>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<SubcriptionModel> modelList = new List<SubcriptionModel>();

                    foreach (var c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertSubcriptionToSubcriptionModel(c));
                    }

                    return new CollectionModel<SubcriptionModel>
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

        public bool Insert(SubcriptionModel subcription)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    int userId = GetCustomerId(subcription.EmailSubcribed);
                    Subcription insert = new Subcription()
                    {
                        UserId = userId,
                        IsCustomer = userId != 0,
                        EmailSubcribed = subcription.EmailSubcribed
                    };

                    Subcription inserted = uow.Repository<Subcription>().Add(insert);

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

        public int GetCustomerId(string email)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile user = uow.Repository<UserProfile>().FirstOrDefault(o => o.Email == email);

                    if (user != null)
                    {
                        return user.UserId;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }

        }

        public bool IsSubcribed(string email)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Subcription sub = uow.Repository<Subcription>().FirstOrDefault(o => o.EmailSubcribed == email);

                    if (sub != null)
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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Subcription sub = uow.Repository<Subcription>().FirstOrDefault(x => x.Id == id);

                    if (sub == null)
                    {
                        throw new DataLayerException("The contact is not found in the system");
                    }

                    uow.Repository<Subcription>().Remove(sub);

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
    }
}
