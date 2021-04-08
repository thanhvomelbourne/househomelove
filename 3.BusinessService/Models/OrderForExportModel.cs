using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class OrderForExportModel
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TrackingNumber { get; set; }
        public bool DeliveryToDifferentAddress { get; set; }
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
        public string OrderCreatedAt { get; set; }
        public string OrderCreatedBy { get; set; }
        public string OrderUpdatedAt { get; set; }
        public string OrderUpdatedBy { get; set; }
        public string CompletedAt { get; set; }
        public double TotalAmountExGst { get; set; }
        public double TotalAmountIncGst { get; set; }
        public double Gst { get; set; }
        public double Discount { get; set; }
        public double ShippingFee { get; set; }
        public double TotalFinalPrice { get; set; }
        public string EstimateDelivery { get; set; }
        public int PromotionId { get; set; }
        public int ShippingPromotionId { get; set; }
        public string PromotionCode { get; set; }
        public string ShippingPromotionCode { get; set; }
        public double GSTPercent { get; set; }
        public double CreditCardSurcharge { get; set; }
        public double PaypalSurcharge { get; set; }
        public double? FreeShippingAusPostFrom { get; set; }
        public double? FreeShippingHomeMailFrom { get; set; }
        public double? HomeMailDefaultPrice1 { get; set; }
        public double? HomeMailDefaultPrice2 { get; set; }
        public double? HomeMailDefaultPrice3 { get; set; }
        public double? HomeMailAvailableFrom { get; set; }
        public int? WarehousePostcode { get; set; }
        public bool HomeMailAvailable { get; set; }
        public string ShoppingCreatedAt { get; set; }
        public string ShoppingCreatedBy { get; set; }
        public string ShoppingUpdatedAt { get; set; }
        public string ShoppingUpdatedBy { get; set; }
        public int LastPaymentId { get; set; }
        public string LastPaymentMethod { get; set; }
        public double LastPaymentAmount { get; set; }
        public bool LastPaymentSuccess { get; set; }
        public bool LastPaymentIsFull { get; set; }
        public string LastPaymentReason { get; set; }
        public string LastPaymentDetail { get; set; }
        public string LastPaymentIPAddress { get; set; }
        public string LastPaymentCreatedAt { get; set; }
        public string LastPaymentCreatedBy { get; set; }
        public string LastPaymentUpdatedAt { get; set; }
        public string LastPaymentUpdatedBy { get; set; }
    }
}
