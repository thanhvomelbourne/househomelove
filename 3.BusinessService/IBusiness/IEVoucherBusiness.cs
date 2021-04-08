using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IEVoucherBusiness
    {
        CollectionModel<EVoucherModel> GetAllEVouchersByFilter(Parameter parameter);
        EVoucherModel Insert(EVoucherModel evoucher);
        EVoucherModel Update(EVoucherModel evoucher);
        EVoucherModel Details(int id);
        EVoucherModel CheckBalance(string serialNo, string activationCode);
        bool Delete(int id);
        Task<bool> Delete(List<int> ids);
    }
}
