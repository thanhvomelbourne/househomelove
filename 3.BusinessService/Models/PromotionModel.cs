using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class PromotionModel
    {
        public int Id { get; set; }
        public int PromotionType { get; set; }
        public string PromotionTypeDescription { get; set; }
        public string NameOfPromotion { get; set; }
        public string PromotionCode { get; set; }
        public string Description { get; set; }
        public double? Percentage { get; set; }
        public double? Dollars { get; set; }
        public string DiscountValue { get; set; }
        public bool AutoApply { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateDisplay { get; set; }
        public DateTime? EndDate { get; set; }
        public int UsedTime { get; set; }
        public string EndDateDisplay { get; set; }
        public int? LimitTime { get; set; }
        public bool IsLive { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
