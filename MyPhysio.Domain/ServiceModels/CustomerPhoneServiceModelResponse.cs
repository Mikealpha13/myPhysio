using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels
{
    public class CustomerPhoneServiceModelResponse
    {
        public bool success { get; set; }
        public string transactionId { get; set; }

        public string message { get; set; }

        public CustomerPhone data { get; set; }
    }

    public class CustomerPhone
    {
        public List<CustomerPhoneDetails> customers { get; set; }
    }
    public class CustomerPhoneDetails
    {
        public List<ContactDetails> cellPhones { get; set; }
        public string customerId { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string emailAddress { get; set; }
        
    }

    public class ContactDetails
    {
        public string number { get; set; }
        public string description { get; set; }
        
    }
}
