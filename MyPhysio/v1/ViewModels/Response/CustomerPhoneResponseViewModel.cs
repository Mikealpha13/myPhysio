using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerPhoneResponseViewModel:ResponseViewModel
    {
        public List<CustomerDetails> CustomerDetails { get; set; } = new List<CustomerDetails>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomerDetails
    {
        /// <summary>
        /// 
        /// </summary>
        public string customerId { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string emailAddress { get; set; }
        public List<string> PhoneNumber { get; set; } = new List<string>();
    }
}
