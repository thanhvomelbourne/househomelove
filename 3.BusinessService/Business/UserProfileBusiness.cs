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
using UserService.RoleService;

namespace BusinessService.Business
{
    public class UserProfileBusiness : IUserProfileBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private readonly IRoleService _roleService;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserProfileBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory, IRoleService roleService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _roleService = roleService;
        }

        #region Implementation of IUserProfileBusiness

        public CollectionModel<UserProfileModel> GetAllUsersByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<UserProfile> list = !string.IsNullOrEmpty(search) ? uow.Repository<UserProfile>().AsNoTracking().Where(x => (x.UserName.Contains(search) || x.FirstName.Contains(search) || x.LastName.ToString().Contains(search) || x.UserId.ToString().Contains(search) || x.CompanyName.Contains(search))) : uow.Repository<UserProfile>().AsNoTracking();

                    list = list.OrderByDescending(s => s.UserId);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<UserProfileModel> modelList = new List<UserProfileModel>();

                    foreach (var c in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertUserProfileToUserProfileModel(c));
                    }

                    return new CollectionModel<UserProfileModel>
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

        public List<UserProfileModel> GetAll()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    var list = uow.Repository<UserProfile>().OrderBy(x => x.UserId);

                    return list.Select(c => Utility.Mapping.ConvertUserProfileToUserProfileModel(c)).ToList();
                }
            }
            catch(Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public bool VerifyUserIsExist(string value)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile user =
                        uow.Repository<UserProfile>().FirstOrDefault(o => o.Email == value || o.UserName == value);

                    if (user != null)
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

        public UserProfileModel Insert(UserProfileModel user, string password)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile up = new UserProfile()
                    {
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        CompanyName = user.CompanyName,
                        Address = user.Address,
                        Suburb = user.Suburb,
                        Postcode = user.Postcode,
                        State = user.State,
                        Country = user.Country,
                        IsLive = user.IsLive,
                        Email = user.Email,
                        Phone = user.Phone,
                        Avatar = user.Avatar
                    };

                    UserProfile inserted = uow.Repository<UserProfile>().Add(up);

                    uow.SaveChanges();

                    webpages_Membership membership = new webpages_Membership() { UserId = inserted.UserId, Password = password };
                    webpages_Membership insertedMembership = uow.Repository<webpages_Membership>().Add(membership);

                    Subcription subDetail = uow.Repository<Subcription>().FirstOrDefault(o => o.EmailSubcribed == inserted.Email);

                    if (subDetail == null)
                    {
                        Subcription sub = new Subcription()
                        {
                            UserId = inserted.UserId,
                            IsCustomer = true,
                            EmailSubcribed = inserted.Email
                        };

                        uow.Repository<Subcription>().Add(sub);

                    }
                    else
                    {
                        subDetail.IsCustomer = true;
                    }

                    uow.SaveChanges();

                    UserProfileModel model = Utility.Mapping.ConvertUserProfileToUserProfileModel(inserted);

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

        public UserProfileModel Update(UserProfileModel user)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile existing = uow.Repository<UserProfile>().FirstOrDefault(o => o.UserId == user.UserId);

                    if (existing != null)
                    {
                        existing.UserName = user.UserName;
                        existing.FirstName = user.FirstName;
                        existing.LastName = user.LastName;
                        existing.Gender = user.Gender;
                        existing.CompanyName = user.CompanyName;
                        existing.Address = user.Address;
                        existing.Suburb = user.Suburb;
                        existing.Postcode = user.Postcode;
                        existing.State = user.State;
                        existing.Country = user.Country;
                        existing.IsLive = user.IsLive;
                        existing.Email = user.Email;
                        existing.Phone = user.Phone;
                        existing.Avatar = user.Avatar;

                        uow.SaveChanges();
                    }

                    UserProfileModel model = Utility.Mapping.ConvertUserProfileToUserProfileModel(existing);

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

        public UserProfileModel UpdateMyAccount(UserProfileModel user)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile existing = uow.Repository<UserProfile>().FirstOrDefault(o => o.UserId == user.UserId);

                    if (existing != null)
                    {
                        existing.FirstName = user.FirstName;
                        existing.LastName = user.LastName;
                        existing.Gender = user.Gender;
                        existing.CompanyName = user.CompanyName;
                        existing.Address = user.Address;
                        existing.Suburb = user.Suburb;
                        existing.Postcode = user.Postcode;
                        existing.State = user.State;
                        existing.Country = user.Country;
                        existing.Email = user.Email;
                        existing.Phone = user.Phone;
                        existing.Avatar = user.Avatar;

                        uow.SaveChanges();
                    }

                    UserProfileModel model = Utility.Mapping.ConvertUserProfileToUserProfileModel(existing);

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

        public UserProfileModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile user = uow.Repository<UserProfile>().FirstOrDefault(o => o.UserId == id);

                    if (user != null)
                    {
                        UserProfileModel model = Utility.Mapping.ConvertUserProfileToUserProfileModel(user);
                        model.IsAdmin = _roleService.IsUserInRole(user.UserName, Constant.Roles.Administrator);

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

        public string GetUserNameByEmail(string email)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile user = uow.Repository<UserProfile>().FirstOrDefault(o => o.Email == email);

                    if (user != null)
                    {
                        return user.UserName;
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

        public int GetUserIdWhereEmail(string email)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    UserProfile user =
                        uow.Repository<UserProfile>().FirstOrDefault(o => o.Email == email);

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

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    //Get existing Period
                    UserProfile userProfile =
                        uow.Repository<UserProfile>().FirstOrDefault(o => o.UserId == id);

                    if (userProfile == null)
                    {
                        throw new DataLayerException("UserProfile is not found in the system");
                    }

                    //Delete
                    uow.Repository<webpages_Membership>().Remove(userProfile.webpages_Membership);
                    uow.Repository<UserProfile>().Remove(userProfile);

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
