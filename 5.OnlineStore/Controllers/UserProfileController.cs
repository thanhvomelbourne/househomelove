using BusinessService.Models;
using BusinessService.Service;
using CoreLibrary.Data;
using Database.Model;
using log4net;
using OnlineStore.Helper;
using OnlineStore.Models;
using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using UserService.RoleService;
using UserService.UserService;
using WebMatrix.WebData;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace OnlineStore.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserService<UserProfile, int> _userService;
        private readonly IRoleService _roleService;
        private readonly IService _service;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserProfileController(IUserService<UserProfile, int> userService, IRoleService roleService, IService service)
        {
            _userService = userService;
            _roleService = roleService;
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult CreateUpdate(UserProfileModel model)
        {
            model.Gender = model.GenderDisplay == Constant.CommonValue.Male;
            model.UserName = !string.IsNullOrEmpty(model.UserName) ? model.UserName : $"{model.FirstName.Trim()}{model.LastName.Trim()}{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}";
            model.IsLive = (Request.Form["IsLive"] == "on");

            Random rnd = new Random();
            int avaNumber = rnd.Next(0, 2);

            if (model.Gender)
            {
                switch (avaNumber)
                {
                    case 0:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar;
                        break;
                    case 1:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar1;
                        break;
                    case 2:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar2;
                        break;
                }
            }
            else
            {
                switch (avaNumber)
                {
                    case 0:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar;
                        break;
                    case 1:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar1;
                        break;
                    case 2:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar2;
                        break;
                }
            }

            if (model.UserId == 0)
            {
                try
                {
                    if (_service.UserProfile.VerifyUserIsExist(model.UserName))
                    {
                        return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                    }

                    if (_service.UserProfile.VerifyUserIsExist(model.Email))
                    {
                        return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                    }

                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, Gender = model.Gender, CompanyName = model.CompanyName, Email = model.Email, Phone = model.Phone, Address = model.Address, Suburb = model.Suburb, Postcode = model.Postcode, State = model.State, Country = model.Country, IsLive = model.IsLive, Avatar = model.Avatar, CreatedBy = Membership.GetUser().UserName, CreatedAt = DateTime.UtcNow }, false);

                    if (model.Role.Equals(Constant.Roles.Administrator)) { _roleService.AddUserToRole(model.UserName, Constant.Roles.Administrator); }
                    if (model.Role.Equals(Constant.Roles.Customer)) { _roleService.AddUserToRole(model.UserName, Constant.Roles.Customer); }

                    TempData["messageClass"] = "alert-success";
                    TempData["message"] = $"The account of '{model.FirstName} {model.LastName}' is created successfully!";

                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
                catch (Exception ex)
                {
                    log.Debug($"{ex.Message} - {ex}");
                    TempData["messageClass"] = "alert-danger";
                    TempData["message"] = "Cannot create the user profile!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
            }
            else
            {
                try
                {
                    _service.UserProfile.Update(model);

                    if (_roleService.IsUserInRole(model.UserName, Constant.Roles.Administrator)) { _roleService.RemoveUserFromRole(model.UserName, Constant.Roles.Administrator); }
                    if (_roleService.IsUserInRole(model.UserName, Constant.Roles.Customer)) { _roleService.RemoveUserFromRole(model.UserName, Constant.Roles.Customer); }

                    if (model.Role.Equals(Constant.Roles.Administrator)) { _roleService.AddUserToRole(model.UserName, Constant.Roles.Administrator); }
                    if (model.Role.Equals(Constant.Roles.Customer)) { _roleService.AddUserToRole(model.UserName, Constant.Roles.Customer); }

                    string token = WebSecurity.GeneratePasswordResetToken(model.UserName);

                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        WebSecurity.ResetPassword(token, model.Password);
                    }

                    TempData["messageClass"] = "alert-success";
                    TempData["message"] = $"The account of '{model.FirstName} {model.LastName}' is updated successfully!";

                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
                catch (Exception ex)
                {
                    log.Debug($"{ex.Message} - {ex}");
                    TempData["messageClass"] = "alert-danger";
                    TempData["message"] = "Cannot update the user profile!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserProfileModel model)
        {
            model.Gender = model.GenderDisplay == Constant.CommonValue.Male ? true : false;
            model.IsLive = true;
            string currentUser = "System";
            model.UserName = $"{model.FirstName.Trim()}{model.LastName.Trim()}-{DateTime.UtcNow.Date.Hour.ToString()}{DateTime.UtcNow.Date.Minute.ToString()}{DateTime.UtcNow.Date.Day.ToString()}{DateTime.UtcNow.Date.Month.ToString()}{DateTime.UtcNow.Date.Year.ToString()}";

            Random rnd = new Random();
            int avaNumber = rnd.Next(0, 2);

            if (model.Gender)
            {
                switch (avaNumber)
                {
                    case 0:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar;
                        break;
                    case 1:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar1;
                        break;
                    case 2:
                        model.Avatar = Constant.DefaultImagePath.DefaultMaleAvatar2;
                        break;
                }
            }
            else
            {
                switch (avaNumber)
                {
                    case 0:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar;
                        break;
                    case 1:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar1;
                        break;
                    case 2:
                        model.Avatar = Constant.DefaultImagePath.DefaultFemaleAvatar2;
                        break;
                }
            }

            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RetypePassword))
            {
                TempData["error"] = "The password cannot be empty!";
            }

            if (model.Password != model.RetypePassword)
            {
                TempData["error"] = "The new password is not matching with confirm password!";
            }

            if (model.UserId == 0)
            {
                if (_service.UserProfile.VerifyUserIsExist(model.UserName))
                {
                    TempData["error"] = "This username is already existing!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
                else if (_service.UserProfile.VerifyUserIsExist(model.Email))
                {
                    TempData["error"] = "This email is already existing!";
                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }

                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName, Gender = model.Gender, CompanyName = model.CompanyName, Email = model.Email, Phone = model.Phone, Address = model.Address, Suburb = model.Suburb, Postcode = model.Postcode, State = model.State, Country = model.Country, IsLive = model.IsLive, Avatar = model.Avatar, CreatedBy = currentUser, CreatedAt = DateTime.UtcNow }, false);

                _roleService.AddUserToRole(model.UserName, Constant.Roles.Customer);

                ApplicationSettingModel appConfig = _service.ApplicationSetting.AppConfiguration();
                EmailHelper.SendRegistrationEmail(appConfig, $"{model.FirstName} {model.LastName}", model.Email);

                TempData["success"] = "The account has been created!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            else
            {
                _service.UserProfile.Update(model);

                if (_roleService.IsUserInRole(model.UserName, Constant.Roles.Administrator)) { _roleService.RemoveUserFromRole(model.UserName, Constant.Roles.Administrator); }
                if (_roleService.IsUserInRole(model.UserName, Constant.Roles.Customer)) { _roleService.RemoveUserFromRole(model.UserName, Constant.Roles.Customer); }

                _roleService.AddUserToRole(model.UserName, Constant.Roles.Customer);

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpGet]
        public JsonResult GetUserById(int userId)
        {
            UserProfileModel user = _service.UserProfile.Details(userId);

            return user != null ? Json(new { success = true, data = user }, JsonRequestBehavior.AllowGet) : Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constant.Roles.Administrator)]
        public ActionResult DeleteUserProfile(int userIdForDelete)
        {
            try
            {
                UserProfileModel user = _service.UserProfile.Details(userIdForDelete);

                if (_roleService.IsUserInRole(user.UserName, Constant.Roles.Administrator)) { _roleService.RemoveUserFromRole(user.UserName, Constant.Roles.Administrator); }
                if (_roleService.IsUserInRole(user.UserName, Constant.Roles.Customer)) { _roleService.RemoveUserFromRole(user.UserName, Constant.Roles.Customer); }

                _service.UserProfile.Delete(userIdForDelete);

                TempData["messageClass"] = "alert-success";
                TempData["message"] = $"The account of '{user.FirstName} {user.LastName}' is deleted successfully!";

                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                TempData["messageClass"] = "alert-danger";
                TempData["message"] = "Cannot delete the user profile!";
                return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (LoginTroughLocalMembership(model))
                {
                    return RedirectToDashboard(returnUrl);
                }
            }

            return RedirectToAction("Login", "Admin");
        }

        private bool LoginTroughLocalMembership(LoginModel model)
        {
            try
            {
                model.UserName = _service.UserProfile.GetUserNameByEmail(model.UserName);

                log.Info($"User is trying to login Username: {model.UserName}");

                if (string.IsNullOrEmpty(model.UserName))
                {
                    return false;
                }

                if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    UserProfile user = _userService.GetUserByUserName(model.UserName);
                    if (user != null)
                    {
                        UtilityHelper.SetCookie(UtilityHelper.USER_ID, user.UserId.ToString(), DateTime.UtcNow.AddDays(1));
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        
                        return true;
                    }

                    log.Info($"{model.UserName} is inactive user.");
                    return false;
                }

                log.Info("The user email or password provided is incorrect.");
                return false;
            }
            catch (InvalidOperationException)
            {
                log.Info("System user and role setting is incorrectly configured.");
            }

            log.Info("The user email or password provided is incorrect.");

            return false;
        }

        public ActionResult LogOff()
        {

            WebSecurity.Logout();
            if (Request.IsAjaxRequest())
            {
                return JavaScript("document.location.replace('" + Url.Action("Login", "Admin") + "');");
            }

            return RedirectToAction("Login", "Admin");
        }

        private ActionResult RedirectToDashboard(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerLogin(LoginModel model, string returnUrl)
        {
            log.Info($"User is trying to login Email: {model.UserName}");

            model.RememberMe = (Request.Form["RememberMe"] == "on") ? true : false;
            UtilityHelper.SetCookie(UtilityHelper.USER_ID, null, DateTime.UtcNow.AddDays(1));

            if (ModelState.IsValid)
            {
                if (LoginTroughLocalMembership(model))
                {
                    return RedirectToLocal(returnUrl);
                }
            }

            TempData["error"] = "Login failed, please check your email and password and try again!";

            return RedirectToAction("Login", "Client");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Client");
        }

        public ActionResult CustomerLogOff()
        {
            WebSecurity.Logout();
            if (Request.IsAjaxRequest())
            {
                UtilityHelper.SetCookie(UtilityHelper.USER_ID, null, DateTime.UtcNow.AddDays(1));
                return JavaScript("document.location.replace('" + Url.Action("Login", "Client") + "');");
            }

            return RedirectToAction("Login", "Client");
        }

        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                UserProfileModel data = _service.UserProfile.Details(WebSecurity.CurrentUserId);

                if (data != null)
                {
                    return Json(new { success = true, data }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMyAccount(UserProfileModel model)
        {
            model.Gender = model.GenderDisplay == Constant.CommonValue.Male;
            model.UserId = WebSecurity.CurrentUserId;

            model.Avatar = model.Gender ? Constant.DefaultImagePath.DefaultMaleAvatar : Constant.DefaultImagePath.DefaultFemaleAvatar;

            UserProfileModel data = _service.UserProfile.UpdateMyAccount(model);

            if (data == null)
            {
                TempData["error"] = "This account has been updated failed!";
            }
            else
            {
                TempData["success"] = "This account has been updated sucessfully!";
            }

            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserProfileModel model)
        {
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.RetypePassword))
            {
                TempData["error"] = "The password cannot be empty!";
            }

            if (model.Password != model.RetypePassword)
            {
                TempData["error"] = "The new password is not matching with confirm password!";
            }

            bool isSuccess = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.CurrentPassword, model.Password);

            if (isSuccess)
            {
                TempData["success"] = "The password has been updated sucessfully!";
            }
            else
            {
                TempData["error"] = "The password has been updated failed!";
            }

            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LostPassword(string emailLostPassword)
        {
            if (string.IsNullOrEmpty(emailLostPassword))
            {
                TempData["error"] = "The email cannot be empty!";
            }

            string username = _service.UserProfile.GetUserNameByEmail(emailLostPassword);

            if (!string.IsNullOrEmpty(username))
            {
                string token = WebSecurity.GeneratePasswordResetToken(username, 1440);
                string url = string.Format("{0}?token={1}&username={2}", Constant.Path.RecoveryPassword, token, username);

                try
                {
                    ApplicationSettingModel appConfig = _service.ApplicationSetting.AppConfiguration();
                    bool isSuccess = EmailHelper.SendRecoveryPasswordEmail(appConfig, emailLostPassword, url);

                    if (isSuccess)
                    {
                        TempData["success"] = "The recovery password email has been sent to you, please check your email and setup the new password!";
                    }
                    else
                    {
                        TempData["error"] = "The recovery password email has been sent to you failed, please contact to admin for support!";
                    }

                    return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"The recovery password email has been sent to you failed, please contact to admin for support! {ex.Message}";
                    log.Info($"Cannot send recovery password email: {ex.Message}");
                }

            }

            return Redirect(HttpContext.Request.UrlReferrer.OriginalString);
        }
    }
}