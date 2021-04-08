using BusinessService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IGroupBusiness
    {
        CollectionModel<GroupModel> GetAllGroupsByFilter(Parameter parameter);
        IList<GroupModel> GetAllGroups();
        GroupModel Insert(GroupModel category);
        GroupModel Update(GroupModel category);
        GroupModel Details(int id);
        bool Delete(int id);
        Task<bool> Delete(List<int> ids);
    }
}
