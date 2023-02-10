using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.Configuration
{
    public class PaymentMethodsDataSource
    {
       public  List<Paymentmethods> paymentMethods { get; set; } = new List<Paymentmethods>();
    }

    public class Paymentmethods
    {
        public string modeId { get; set; }
        public string paymentmode { get; set; }
        public string modeicon { get; set; }
        public string iconpath { get; set; }
        public string active { get; set; }

    }
}
