using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string UnitOfPrice { get; set; }
        public int Quantity { get; set; }
        public double ImportPrice { get; set; }
        public double OriginalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double FinalPrice { get; set; }
        public string PrimaryImage { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
