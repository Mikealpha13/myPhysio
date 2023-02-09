using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels
{
    public class OrderUpdateResponseServiceModel
    {
        public bool success { get; set; }
        public string transactionId { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
