using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IPromotionBusiness
    {
        IList<PromotionModel> GetAllPromotionsByType(int type);
        CollectionModel<PromotionModel> GetAllPromotionsByFilter(Parameter parameter);
        PromotionModel Insert(PromotionModel promotion);
        PromotionModel Update(PromotionModel promotion);
        PromotionModel Details(int id);
        PromotionModel GetPromotionByPromotionCode(string promotionCode, bool isAdmin = false);
        bool Delete(int id);
    }
}
