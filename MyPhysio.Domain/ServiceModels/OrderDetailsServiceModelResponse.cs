using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels
{
    public class OrderDetailsServiceModelResponse
    {
        public bool success { get; set; }
        public string transactionId { get; set; }

        public string message { get; set; }

        public SalesOrder data { get; set; }
    }

    public class SalesOrder
    {
        public List<orderDetails> salesOrders { get; set; }
    }
    public class orderDetails
    {
        public paymentDetails orderTotals { get; set; }

        public List<FullFillmentDetails> fulfillments { get; set; }

        public string orderId { get; set; }
        public string customerId { get; set; }
        public BillingAddressDetails billingAddress { get; set; }
        public string locationId { get; set; }
        public OrderTYpe orderType { get; set; }
        public DateTime orderDate { get; set; }
        public List<salespeopledetails> salespeople { get; set; }
        public string companyId { get; set; }
        public string originalOrderId { get; set; }
    }

    public class paymentDetails
    {
        public decimal balance { get; set; }
        public decimal subTotal { get; set; }
        public decimal discount { get; set; }
        public decimal fees { get; set; }
        public decimal install { get; set; }
        public decimal tax { get; set; }
        public decimal delivery { get; set; }
        public decimal total { get; set; }
        public decimal payments { get; set; }
    }

    public class FullFillmentDetails
    {
        public List<string> schedule { get; set; }
        public string fulfillmentId { get; set; }
        public FullFIllmentAddress address { get; set; }
        public OrderStatus status { get; set; }
        public DateTime date { get; set; }
        //public DateTime dateWithStopTime { get; set; }
        public FullfillmentMethod method { get; set; }
        public string shippingInstructions { get; set; }
        public string locationId { get; set; }
        public bool deliveryTicketPrinted { get; set; }
        public bool pickTicketPrinted { get; set; }
        public bool onManifest { get; set; }
        public fullfillmentTotal totals { get; set; }
        public List<lineItemsDetails> lineItems { get; set; }
        public string description { get; set; }
        public DateTime? requestedDate { get; set; }
        public string routeCode { get; set; }
        public string contactStatus { get; set; }
        public string truckNumber { get; set; }
    }

    public class FullFIllmentAddress
    {
        public string description { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
        public string cellPhone { get; set; }
        public string workPhone { get; set; }
        public string workPhoneExtension { get; set; }
        public string homePhone { get; set; }
        public string emailAddress { get; set; }
    }


    public enum OrderStatus
    {
        AsSoonAsPossible = 1,
        CustomerWillCall = 2,
        Estimated = 3,
        Scheduled = 4,
        Complete = 5,
        Void = 6
    }

    public enum FullfillmentMethod
    {
        Pickup = 1,

        Delivery = 2,

        DirectShip = 3,

        TakeWith = 4
    }

    public class fullfillmentTotal
    {
        public decimal total { get; set; }
        public decimal fees { get; set; }
        public decimal install { get; set; }
        public decimal subTotal { get; set; }
        public decimal tax { get; set; }
        public decimal delivery { get; set; }
        public decimal discount { get; set; }
    }

    public class lineItemsDetails
    {
        public string brandId { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public bool loadedOnTruck { get; set; }
        public string poLineId { get; set; }
        public decimal normalPrice { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public int quantityCommitted { get; set; }
        public string trackingNumber { get; set; }
        public string vendorModelNumber { get; set; }
        public string directShipTrackingNumber { get; set; }
        public List<string> imageURL { get; set; }
        public string comments { get; set; }
        public string group { get; set; }
        public int lineNumber { get; set; }

        public List<string> serialNumbers { get; set; }
        public int serviceLineNumber { get; set; }
        public string stockLocationId { get; set; }
        public int type { get; set; }
        public string vendorId { get; set; }
        public int unloadTime { get; set; }
        public double volume { get; set; }
        public double weight { get; set; }


    }

    public enum OrderTYpe
    {
        Order = 1,

        Exchange = 2,

        Return = 3,

        Quote = 4,

        Service = 5,

        Transfer = 6
    }

    public class BillingAddressDetails
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string prefix { get; set; }
        public string suffix { get; set; }
        public string cellPhone { get; set; }
        public string homePhone { get; set; }
        public string workPhone { get; set; }
        public string workPhoneExtension { get; set; }
        public string emailAddress { get; set; }

    }

    public class salespeopledetails
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
