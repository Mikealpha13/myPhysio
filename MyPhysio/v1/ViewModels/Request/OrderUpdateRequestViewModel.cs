using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderUpdateRequestViewModel:RequestViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? deliveryDate { get; set; }
       

        /// <summary>
        /// 
        /// </summary>
        public string deliveryInstruction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderId { get; set; }
    }
}
