using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface ISubcriptionBusiness
    {
        CollectionModel<SubcriptionModel> GetAllSubscriptionsByFilter(Parameter parameter);
        bool Insert(SubcriptionModel subcription);
        bool IsSubcribed(string email);
        bool Delete(int id);
    }
}
