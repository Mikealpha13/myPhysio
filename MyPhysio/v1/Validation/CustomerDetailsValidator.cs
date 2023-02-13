using FluentValidation;
using MyPhysio.Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDetailsValidator:AbstractValidator<CustomerDetails>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerDetailsValidator()
        {

            RuleFor(x => x.id).NotNull().NotEmpty().WithMessage("Phone number is required").Length(10, 10).WithMessage("Mobile number must be 10 digit").Matches("^[0-9]").WithMessage("Invalid Number");
         

        }

    }
}
