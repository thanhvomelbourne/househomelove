using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class EVoucherModel
    {
        public int Id { get; set; }
        public string FromFirstName { get; set; }
        public string FromLastName { get; set; }
        public string FromEmail { get; set; }
        public string FromPhone { get; set; }
        public string Message { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public bool IsGift { get; set; }
        public string ToFirstName { get; set; }
        public string ToLastName { get; set; }
        public string ToEmail { get; set; }
        public string ToPhone { get; set; }
        public string EVoucherSerialNo { get; set; }
        public string ActivationCode { get; set; }
        public bool IsLive { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
