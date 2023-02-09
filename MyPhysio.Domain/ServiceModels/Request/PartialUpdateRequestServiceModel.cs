using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels.Request
{
    public class PartialUpdateRequestServiceModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string instructions { get; set; }
    }
}
