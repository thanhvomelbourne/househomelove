using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IContactBusiness
    {
        CollectionModel<ContactModel> GetAllContactsByFilter(Parameter parameter);
        ContactModel Insert(ContactModel contact);
        ContactModel Details(int id);
        bool Delete(int id);
    }
}
