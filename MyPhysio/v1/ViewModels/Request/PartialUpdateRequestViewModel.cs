using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class PartialUpdateRequestViewModel:RequestViewModel
    {
        public DateTime? date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullfillmentId { get; set; }
    }
}
