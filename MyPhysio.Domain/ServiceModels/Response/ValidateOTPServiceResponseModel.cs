using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.ServiceModels.Response
{
    public class ValidateOTPServiceResponseModel
    {
        public string status { get; set; }
        public string payee { get; set; }
        public string to { get; set; }
        public string valid { get; set; }

    }
}
