using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels.Request
{
    public class OrderUpdateRequestServiceModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? deliveryDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string deliveryInstructions { get; set; }
    }
}
