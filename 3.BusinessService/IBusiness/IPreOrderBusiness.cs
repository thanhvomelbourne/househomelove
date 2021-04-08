using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IPreOrderBusiness
    {
        PreOrderModel InsertPreOrder(PreOrderModel preorder);
        PreOrderModel UpdatePreOrder(PreOrderModel preorder);
        CollectionModel<PreOrderModel> GetAllPreOrdersByFilter(Parameter parameter);
        PreOrderModel Details(int id);
        bool Delete(int id);
    }
}
