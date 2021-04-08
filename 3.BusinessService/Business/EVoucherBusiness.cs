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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Business
{
    public class EVoucherBusiness : IEVoucherBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EVoucherBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IEvoucherBusiness

        public CollectionModel<EVoucherModel> GetAllEVouchersByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<EVoucher> list = !string.IsNullOrEmpty(search)
                        ? uow.Repository<EVoucher>().AsNoTracking().Where(x =>
                            (x.EVoucherSerialNo.Contains(search) ||
                             x.ActivationCode.Contains(search) ||
                             x.Id.ToString().Contains(search) ||
                             x.FromEmail.Contains(search) ||
                             x.FromPhone.Contains(search) ||
                             x.ToEmail.Contains(search) ||
                             x.ToPhone.Contains(search) ||
                             x.Message.Contains(search) ||
                             x.Amount.ToString(CultureInfo.InvariantCulture).Contains(search) ||
                             x.CreatedBy.Contains(search) ||
                             x.UpdatedBy.Contains(search)))
                        : uow.Repository<EVoucher>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<EVoucherModel> modelList = new List<EVoucherModel>();

                    foreach (var ev in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertEVoucherToEVoucherModel(ev));
                    }

                    return new CollectionModel<EVoucherModel>
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

        public EVoucherModel Insert(EVoucherModel evoucher)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    EVoucher insert = new EVoucher()
                    {
                        FromFirstName = evoucher.FromFirstName,
                        FromLastName = evoucher.FromLastName,
                        FromEmail = evoucher.FromEmail,
                        FromPhone = evoucher.FromPhone,
                        Message = evoucher.Message,
                        Amount = evoucher.Amount,
                        Balance = evoucher.Amount,
                        IsGift = evoucher.IsGift,
                        ToFirstName = evoucher.ToFirstName,
                        ToLastName = evoucher.ToLastName,
                        ToEmail = evoucher.ToEmail,
                        ToPhone = evoucher.ToPhone,
                        EVoucherSerialNo = CreateActivationCode(),
                        ActivationCode = Guid.NewGuid().ToString(),
                        IsLive = evoucher.IsLive
                    };

                    EVoucher inserted = uow.Repository<EVoucher>().Add(insert);

                    uow.SaveChanges();

                    EVoucherModel model = Utility.Mapping.ConvertEVoucherToEVoucherModel(inserted);

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

        public EVoucherModel Update(EVoucherModel evoucher)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    EVoucher update = uow.Repository<EVoucher>().FirstOrDefault(x => x.Id == evoucher.Id);

                    if (update == null) return null;

                    update.FromFirstName = evoucher.FromFirstName;
                    update.FromLastName = evoucher.FromLastName;
                    update.FromEmail = evoucher.FromEmail;
                    update.FromPhone = evoucher.FromPhone;
                    update.Message = evoucher.Message;
                    update.Balance = update.Balance;
                    update.Amount = evoucher.Amount;
                    update.IsGift = evoucher.IsGift;
                    update.ToFirstName = evoucher.ToFirstName;
                    update.ToLastName = evoucher.ToLastName;
                    update.ToEmail = evoucher.ToEmail;
                    update.ToPhone = evoucher.ToPhone;
                    update.IsLive = evoucher.IsLive;

                    uow.SaveChanges();

                    EVoucherModel model = Utility.Mapping.ConvertEVoucherToEVoucherModel(update);

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

        public EVoucherModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    EVoucher detail = uow.Repository<EVoucher>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        EVoucherModel model = Utility.Mapping.ConvertEVoucherToEVoucherModel(detail);

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

        public EVoucherModel CheckBalance(string serialNo, string activationCode)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    EVoucher detail = uow.Repository<EVoucher>().AsNoTracking().FirstOrDefault(x => x.EVoucherSerialNo == serialNo && x.ActivationCode == activationCode);

                    if (detail == null) return null;

                    EVoucherModel model = Utility.Mapping.ConvertEVoucherToEVoucherModel(detail);

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
                    EVoucher evoucher = uow.Repository<EVoucher>().FirstOrDefault(x => x.Id == id);

                    if (evoucher == null)
                    {
                        throw new DataLayerException("The evoucher is not found in the system");
                    }

                    uow.Repository<EVoucher>().Remove(evoucher);

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
                    var evouchers = await uow.Repository<EVoucher>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in evouchers)
                    {
                        uow.Repository<EVoucher>().Remove(item);
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

        private string CreateActivationCode()
        {
            Random random = new Random();
            string result = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10).Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }

        #endregion
    }
}
