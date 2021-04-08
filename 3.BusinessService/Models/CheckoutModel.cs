using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CheckoutModel
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string UserAvatar { get; set; }
        public string UserFullName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string TrackingNumber { get; set; }
        public bool DeliveryToDifferentAddress { get; set; }
        public int ShippingMethodType { get; set; }
        public string ShippingMethod { get; set; }
        public string DeliveryFirstName { get; set; }
        public string DeliveryLastName { get; set; }
        public string DeliveryCompanyName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliverySuburb { get; set; }
        public string DeliveryPostcode { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryCountry { get; set; }
        public string ConfirmedStatus { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string CompletedAt { get; set; }
    }
}
