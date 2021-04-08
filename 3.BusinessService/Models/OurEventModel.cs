using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessService.Models
{
    public class OurEventModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? Time { get; set; }
        public string TimeDisplay { get; set; }
        public string Avatar { get; set; }
        public bool IsLive { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public HttpPostedFileBase EventImage { get; set; }
    }
}
