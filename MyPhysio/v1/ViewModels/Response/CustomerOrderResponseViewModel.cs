using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerOrderResponseViewModel:ResponseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Orders { get; set; } = new List<string>();
    }
}
