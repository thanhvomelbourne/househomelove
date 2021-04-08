using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; }
        public double Amount { get; set; }
        public bool Success { get; set; }
        public bool IsFull { get; set; }
        public string Reason { get; set; }
        public string Detail { get; set; }
        public string IPAddress { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
