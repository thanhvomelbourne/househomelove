using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessService.Models
{
    public class AddItemToShoppingCartModel
    {
        public int CurrentOrderId { get; set; }
        public int CurrentUserId { get; set; }
        public string CartEncryptId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int[] ProductIds { get; set; }
        public int Quantity { get; set; }
        public double CustomPrice { get; set; }
    }
}