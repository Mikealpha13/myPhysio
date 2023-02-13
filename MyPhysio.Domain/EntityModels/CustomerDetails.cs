using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.EntityModels
{
    public class CustomerDetails
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string emailid { get; set; }
        public string address { get; set; }
        public string street { get; set; }
        public string pincode { get; set; }

    }
}
