using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class Parameter
    {
        public int PageNo { get; set; }
        public string Search { get; set; }
        public string CurrentSearch { get; set; }
        public int PageSize { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string SortBy { get; set; }
        public string SearchByLive { get; set; }
        public string SearchByStockLevel { get; set; }
        public string SearchByConfirmedStatus { get; set; }
        public string SearchByShippingMethod { get; set; }
    }
}
