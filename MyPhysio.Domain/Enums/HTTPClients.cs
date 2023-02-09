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
        [Display(Name="TDGStori API")]
        TDGStori,
        [Display(Name = "DSGStori API")]
        DSGStori,
        [Display(Name = "Podium API")]
        PodiumStori
    }
}
