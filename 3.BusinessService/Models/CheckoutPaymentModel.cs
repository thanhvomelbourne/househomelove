using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CheckoutPaymentModel
    {
        [Required]
        public string Method { get; set; }
        public string CardFirstName { get; set; }
        public string CardLastName { get; set; }
        public string CardNumber { get; set; }
        public int MonthExpire { get; set; }
        public int YearExpire { get; set; }
        public string CCV { get; set; }
        public string CreditCardType { get; set; }
        public string EVoucherSerialNo { get; set; }
        public string ActivationCode { get; set; }
    }
}
