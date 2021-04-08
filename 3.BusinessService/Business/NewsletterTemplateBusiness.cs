using BusinessService.IBusiness;
using BusinessService.Models;
using BusinessService.Utility;
using CoreLibrary.Data;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Business
{
    public class NewsletterTemplateBusiness : INewsletterTemplateBusiness
    {
        private readonly IUnitOfWorkFactory<UnitOfWork> _unitOfWorkFactory;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NewsletterTemplateBusiness(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Implementation of INewsletterTemplateBusiness

        public CollectionModel<NewsletterTemplateModel> GetAllNewsletterTemplateByFilter(Parameter parameter)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    string search = parameter.Search;
                    int pageNo = parameter.PageNo;

                    IQueryable<NewsletterTemplate> list = !string.IsNullOrEmpty(search) ? uow.Repository<NewsletterTemplate>().AsNoTracking().Where(x => (x.TemplateName.Contains(search) || x.Description.Contains(search) || x.TemplateSubject.Contains(search) || x.TemplateContent.Contains(search) || x.Id.ToString().Contains(search) || x.CreatedBy.Contains(search) || x.UpdatedBy.Contains(search))) : uow.Repository<NewsletterTemplate>().AsNoTracking();

                    list = list.OrderByDescending(s => s.Id);

                    var totalItems = list.Count();
                    var totalPages = (totalItems % Constant.CommonValue.PageSize20) == 0 ? (totalItems / Constant.CommonValue.PageSize20) : (totalItems / Constant.CommonValue.PageSize20) + 1;

                    list = list.Skip(Constant.CommonValue.PageSize20 * (parameter.PageNo - 1)).Take(Constant.CommonValue.PageSize20);

                    List<NewsletterTemplateModel> modelList = new List<NewsletterTemplateModel>();

                    foreach (var temp in list)
                    {
                        modelList.Add(Utility.Mapping.ConvertNewsletterTemplateToNewsletterTemplateModel(temp));
                    }

                    return new CollectionModel<NewsletterTemplateModel>
                    {
                        Data = modelList,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        CurrenPage = pageNo,
                    };
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public IList<NewsletterTemplateModel> GetAllNewsletterTemplates()
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    List<NewsletterTemplate> temps = uow.Repository<NewsletterTemplate>().AsNoTracking().OrderBy(x => x.Id).ToList();

                    if (temps.Any())
                    {
                        List<NewsletterTemplateModel> modelList = new List<NewsletterTemplateModel>();

                        foreach (var temp in temps)
                        {
                            modelList.Add(Utility.Mapping.ConvertNewsletterTemplateToNewsletterTemplateModel(temp));
                        }

                        return modelList;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public NewsletterTemplateModel Insert(NewsletterTemplateModel template)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    NewsletterTemplate insert = new NewsletterTemplate()
                    {
                        TemplateName = template.TemplateName,
                        Description = template.Description,
                        TemplateSubject = template.TemplateSubject,
                        TemplateContent = template.TemplateContent
                    };

                    NewsletterTemplate inserted = uow.Repository<NewsletterTemplate>().Add(insert);

                    uow.SaveChanges();

                    NewsletterTemplateModel model = Mapping.ConvertNewsletterTemplateToNewsletterTemplateModel(inserted);

                    return model;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public NewsletterTemplateModel Update(NewsletterTemplateModel template)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    NewsletterTemplate update = uow.Repository<NewsletterTemplate>().FirstOrDefault(x => x.Id == template.Id);

                    if (update != null)
                    {
                        update.TemplateName = !string.IsNullOrEmpty(template.TemplateName) ? template.TemplateName : update.TemplateName;
                        update.Description = !string.IsNullOrEmpty(template.Description) ? template.Description : update.Description;
                        update.TemplateSubject = template.TemplateSubject;
                        update.TemplateContent = template.TemplateContent;

                        uow.SaveChanges();

                        NewsletterTemplateModel model = Mapping.ConvertNewsletterTemplateToNewsletterTemplateModel(update);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public NewsletterTemplateModel Details(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    NewsletterTemplate detail = uow.Repository<NewsletterTemplate>().AsNoTracking().FirstOrDefault(x => x.Id == id);

                    if (detail != null)
                    {
                        NewsletterTemplateModel model = Mapping.ConvertNewsletterTemplateToNewsletterTemplateModel(detail);

                        return model;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    NewsletterTemplate template = uow.Repository<NewsletterTemplate>().FirstOrDefault(x => x.Id == id);

                    if (template == null)
                    {
                        throw new DataLayerException("The template is not found in the system");
                    }

                    uow.Repository<NewsletterTemplate>().Remove(template);

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public async Task<bool> Delete(List<int> ids)
        {
            try
            {
                using (UnitOfWork uow = _unitOfWorkFactory.Create())
                {
                    var templates = await uow.Repository<NewsletterTemplate>().Where(x => ids.Contains(x.Id)).ToListAsync();

                    foreach (var item in templates)
                    {
                        uow.Repository<NewsletterTemplate>().Remove(item);
                    }

                    uow.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Debug($"{ex.Message} - {ex}");
                EmailHelper.SendSystemErrorEmail(ex.Message, ex.ToString());
                throw new DataLayerException(ex.Message);
            }
        }

        public bool SendNewsletterToPerson(string email, int templateId)
        {
            NewsletterTemplateModel template = Details(templateId);

            return EmailHelper.SendNewsletterEmail(template.TemplateContent, template.TemplateSubject, email);
        }

        #endregion
    }
}
