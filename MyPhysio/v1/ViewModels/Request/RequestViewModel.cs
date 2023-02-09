using MyPhysio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.ViewModels.Request
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RequestViewModel
    {
        /// <summary>
        /// Client Indicator
        /// </summary>
        public HTTPClients Client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RequestID { get{ return Guid.NewGuid().ToString(); } }
    }
}
