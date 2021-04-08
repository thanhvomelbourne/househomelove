using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class NewsletterTemplateModel
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateContent { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
