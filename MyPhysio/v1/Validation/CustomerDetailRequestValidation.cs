
using MyPhysio.v1.ViewModels.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerDetailRequestValidation:AbstractValidator<CustomerDetailsRequestViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerDetailRequestValidation()
        {
            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer ID is required")
                .GreaterThan
                (0).WithMessage("Invalid Customer ID");

                

        }
    }
}
