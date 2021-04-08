using BusinessService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IColorBusiness
    {
        CollectionModel<ColorModel> GetAllColorsByFilter(Parameter parameter);
        IList<ColorModel> GetAllColors();
        ColorModel Insert(ColorModel category);
        ColorModel Update(ColorModel category);
        ColorModel Details(int id);
        bool Delete(int id);
        bool CheckValidate(string colorName);
        Task<bool> Delete(List<int> ids);
    }
}
