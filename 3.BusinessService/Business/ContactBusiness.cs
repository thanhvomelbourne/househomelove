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
    public class ContactBusiness : IContactBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContactBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of IContactBusiness

        public CollectionModel<ContactModel> GetAllContactsByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<Contact> list = !string.IsNullOrEmpty(search) ? uow.Repository<Contact>().AsNoTracking().Where(x => (x.ContactName.Contains(search) || x.ContactEmail.Contains(search) || x.Id.ToString().Contains(search) || x.ContactPhone.ToString().Contains(search) || x.Subject.ToString().Contains(search) || x.Message.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<Contact>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    int totalItems = list.Count();
                    int totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<ContactModel> modelList = new List<ContactModel>();

                    foreach (Contact c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertContactToContactModel(c));
                    }

                    return new CollectionModel<ContactModel>
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

        public ContactModel Insert(ContactModel contact)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Contact insert = new Contact()
                    {
                        ContactName = contact.ContactName,
                        ContactEmail = contact.ContactEmail,
                        ContactPhone = contact.ContactPhone,
                        IsRead = false,
                        Subject = contact.Subject,
                        Message = contact.Message
                    };

                    Contact inserted = uow.Repository<Contact>().Add(insert);

                    uow.SaveChanges();

                    ContactModel model = Utility.Mapping.ConvertContactToContactModel(inserted);

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

        public ContactModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    Contact detail = uow.Repository<Contact>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        detail.IsRead = true;
                        uow.SaveChanges();

                        ContactModel model = Utility.Mapping.ConvertContactToContactModel(detail);

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
                    Contact contact = uow.Repository<Contact>().FirstOrDefault(x => x.Id == id);

                    if (contact == null)
                    {
                        throw new DataLayerException("The contact is not found in the system");
                    }

                    uow.Repository<Contact>().Remove(contact);

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
