using MyPhysioAPI.v1.ViewModels.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Validation
{
    /// <summary>
    /// Validation class for customer by phone request view model
    /// </summary>
    public class CustomerPhoneRequestValidator:AbstractValidator<CustomerPhoneRequestViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerPhoneRequestValidator()
        {
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("Customer phone number is required");
        }
    }
}
