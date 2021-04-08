using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class DashboardModel
    {
        public int TotalCategoryItems { get; set; }
        public int TotalProductItems { get; set; }
        public int TotalOrderItems { get; set; }
        public int TotalUserItems { get; set; }
        public int TotalContactMessageUnread { get; set; }
        public string LastContactMessageTime { get; set; }
        public int TotalSubscription { get; set; }
        public string LastSubscriptionTime { get; set; }
        public int TotalPromotionIsRunning { get; set; }
        public string LastOrderPlaced { get; set; }
    }
}
