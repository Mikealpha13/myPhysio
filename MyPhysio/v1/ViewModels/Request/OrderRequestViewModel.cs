using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Request
{
    /// <summary>
    /// This class is created to fetch the order details
    /// </summary>
    public class OrderRequestViewModel : RequestViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Orders { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public bool ScheduleDates { get; set; } = true;


    }
}
