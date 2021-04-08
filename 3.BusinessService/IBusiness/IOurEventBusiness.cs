using BusinessService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IOurEventBusiness
    {
        CollectionModel<OurEventModel> GetAllOurEventsByFilter(Parameter parameter);
        IList<OurEventModel> GetAllOurEvents();
        OurEventModel Insert(OurEventModel category);
        OurEventModel Update(OurEventModel category);
        OurEventModel Details(int id);
        bool Delete(int id);
        Task<bool> Delete(List<int> ids);
    }
}
