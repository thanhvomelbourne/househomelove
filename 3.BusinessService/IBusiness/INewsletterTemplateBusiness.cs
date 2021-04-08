using BusinessService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface INewsletterTemplateBusiness
    {
        CollectionModel<NewsletterTemplateModel> GetAllNewsletterTemplateByFilter(Parameter parameter);
        IList<NewsletterTemplateModel> GetAllNewsletterTemplates();
        NewsletterTemplateModel Insert(NewsletterTemplateModel category);
        NewsletterTemplateModel Update(NewsletterTemplateModel category);
        NewsletterTemplateModel Details(int id);
        bool Delete(int id);
        Task<bool> Delete(List<int> ids);
        bool SendNewsletterToPerson(string email, int templateId);
    }
}
