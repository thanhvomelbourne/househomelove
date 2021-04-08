using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserService.RoleService;
using WebMatrix.WebData;

namespace OnlineStore.App_Start
{
    public class DbInitializer
    {
        public static void Initialize()
        {
            WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfiles", "UserId", "UserName", autoCreateTables: true);

            var roleService = DependencyResolver.Current.GetService<IRoleService>();
            var uowFactory = DependencyResolver.Current.GetService<IUnitOfWorkFactory<UnitOfWork>>();

            string username = "admin";
            string password = "King123$";
            string firstName = "Thanh";
            string lastName = "Vo";
            string email = "thanhvo.melbourne@gmail.com";

            if (roleService.GetAllRoles().ToList().Count == 0)
            {
                roleService.AddRole(Constant.Roles.Administrator);
            }

            if (!WebSecurity.UserExists(username))
            {
                WebSecurity.CreateUserAndAccount(username, password, new { UserName = username, FirstName = firstName, LastName = lastName, Email = email, Avatar = Constant.DefaultImagePath.DefaultAdminAvatar, IsLive = Constant.EntityStatus.Active, CreatedBy = username, CreatedAt = DateTime.UtcNow }, false);
                roleService.AddUserToRole(username, Constant.Roles.Administrator);
            }
        }
    }
}