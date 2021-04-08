using BusinessService.Models;
using BusinessService.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IApplicationSettingBusiness
    {
        ApplicationSettingModel Update(ApplicationSettingModel setting);
        ApplicationSettingModel UpdatePolicySetting(ApplicationSettingModel setting);
        ApplicationSettingModel UpdateAppConfiguration(ApplicationSettingModel setting);
        ApplicationSettingModel Details();
        ApplicationSettingModel PolicySetting();
        ApplicationSettingModel AppConfiguration();
        HomePageModel GetCommonDataForHomePageClient();
        DashboardModel GetDashboard();
        AdminModel GetCommonDataForAdminPage();
        void SendTestEmail();
        bool DeleteUnusedProductImages();
        bool DeleteUnusedQRCodeImages();
        bool DeleteUnusedBannersIcons();
        bool DeleteImage(string name);
    }
}
