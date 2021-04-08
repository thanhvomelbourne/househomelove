using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class OrderModel
    {
        public CheckoutModel Checkout { get; set; }
        public ShoppingCartModel ShoppingCart { get; set; }
        public List<PaymentModel> PaymentList { get; set; }
    }
}
