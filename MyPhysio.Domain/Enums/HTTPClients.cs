using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.Enums
{
    public enum HTTPClients
    {
        //TODO -Rename SToris
        [Display(Name="TwilioAPI")]
        Twilio,
        
    }
}
