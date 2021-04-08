using BusinessService.Models;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IBusiness
{
    public interface IOrderBusiness
    {
        OrderModel Checkout(CheckoutModel checkout, CheckoutPaymentModel payment);
        OrderModel PlaceOrder(CheckoutModel checkout);
        OrderModel UpdateOrder(CheckoutModel checkout);
        OrderModel UpdateCustomerInfo(CheckoutModel checkout);
        OrderModel PayNext(CheckoutModel checkout, CheckoutPaymentModel payment);
        OrderModel CompletePayment(OrderModel order, string status, string message, bool isFull = true, double amount = 0);
        OrderModel GetOrderConfirmation(int orderId, bool reCalculate = false);
        CollectionModel<OrderModel> GetAllOrdersByFilter(Parameter parameter);
        bool AddTrackingNumber(string trackingNumber, int orderId);
        bool AddNote(string note, int orderId);
        OrderModel CheckoutByEVoucher(OrderModel order, CheckoutPaymentModel payment);
        List<OrderModel> GetHistoryTransaction(int userId);
        bool Delete(int id);
        void ForceToCompleteOrder(int orderId);
        void UpdateStatusForOrder(int orderId, string status);
        List<OrderForExportModel> GetAllOrdersForExportToExcel();
    }
}
