using BusinessService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface ICategoryBusiness
    {
        CollectionModel<CategoryModel> GetAllCategoriesByFilter(Parameter parameter);
        IList<CategoryModel> GetAllCategories();
        IList<CategoryModel> GetAllCategoriesForAdmin();
        CategoryModel Insert(CategoryModel category);
        CategoryModel Update(CategoryModel category);
        CategoryModel Details(int id);
        bool Delete(int id);
        Task<bool> Delete(List<int> ids);
    }
}
