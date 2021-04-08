using BusinessService.Models;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IUserProfileBusiness
    {
        CollectionModel<UserProfileModel> GetAllUsersByFilter(Parameter parameter);
        List<UserProfileModel> GetAll();
        bool VerifyUserIsExist(string value);
        UserProfileModel Insert(UserProfileModel period, string password);
        UserProfileModel Update(UserProfileModel period);
        UserProfileModel UpdateMyAccount(UserProfileModel period);
        UserProfileModel Details(int id);
        string GetUserNameByEmail(string email);
        int GetUserIdWhereEmail(string email);
        bool Delete(int id);
    }
}
