using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models.Common
{
    public class AustraliaPostModel
    {
        public Result postage_result { get; set; }
    }

    public class Result
    {
        public string service { get; set; }
        public string delivery_time { get; set; }
        public string total_cost { get; set; }
        public CostsList costs { get; set; }
    }

    public class CostsList
    {
        public Costs cost { get; set; }
    }

    public class Costs {
        public string item { get; set; }
        public string cost { get; set; }
    }
}
