using MyPhysioAPI.v1.ViewModels.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderUpdateRequestValidator:AbstractValidator<OrderUpdateRequestViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderUpdateRequestValidator()
        {
           

            RuleFor(x => x.deliveryDate)
                .Must(ValidateDate)
                .WithMessage("Invalid Date .The date can not be in past");


            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Order Id is Required");


            
               
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool ValidateDate(DateTime? date)
        {
            if (date.HasValue) return (date > DateTime.Now);
            else return true;

        }
    }

    
}
