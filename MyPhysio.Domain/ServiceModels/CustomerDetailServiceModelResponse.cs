using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels
{
    public class CustomerDetailServiceModelResponse
    {
        public bool success { get; set; }
        public string transactionId { get; set; }

        public string message { get; set; }

        public Customer data { get; set; }


    }


    public class Customer
    {
        public List<CustomerDetails> customers { get; set; }
    }

    public class CustomerDetails
    {
        public BillingDetails billingAddress { get; set; }

        public DateTime? inactiveDate { get; set; }

        public string customerId { get; set; }

        public string customerType { get; set; }
    }

    public class BillingDetails
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
}
