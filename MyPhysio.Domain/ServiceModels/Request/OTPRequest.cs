using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels.Request
{
    public class OTPRequest
    {
        public string to { get; set; }
        public string channel { get; set; }
    }
}
