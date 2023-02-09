using MyPhysio.Domain.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Response
{

    /// <summary>
    /// This class is used to fetch the order details
    /// </summary>
    public class OrderResponseViewModel : ResponseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderDetailsServiceModelResponse response { get; set; }

    }
}
