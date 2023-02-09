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
    public class OrderRequestValidator:AbstractValidator<OrderRequestViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public OrderRequestValidator()
        {
            RuleFor(x => x.Orders).Must(ValidateOrder).WithMessage("Order id is required");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        private bool ValidateOrder(List<string> orders)
        {
            return orders.Count > 0;
        }
    }
}
