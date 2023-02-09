using MyPhysioAPI.v1.ViewModels.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Validation
{
    public class PartialUpdateRequestValidation:AbstractValidator<PartialUpdateRequestViewModel>
    {
        public PartialUpdateRequestValidation()
        {
            RuleFor(x => x.FullfillmentId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Full fillment Id is Required")
                .Must(ValidFullFillmentId)
                .WithMessage("Inavlid Fullfillment ID");

            RuleFor(x => x.date)               
                .Must(ValidateDate)
                .WithMessage("Invalid Date .The date can not be in past");


           


            RuleFor(x => x.status)
               .NotEmpty()
               .NotNull()
               .WithMessage("status is Required")
               .Must(ValidateStatus)
               .WithMessage("Please enter valid status 1(As soon as possible) 2(CustomerWillCall) 3(Estimated) 4 (Scheduled) 5(Complete) 6(void)");





        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool ValidFullFillmentId(string request)
        {
            var id = new Guid();
            return Guid.TryParse(request,out id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool ValidateDate(DateTime? date)
        {
            if(date.HasValue) return (date > DateTime.Now);
            else return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool ValidateStatus(int status)
        {
            List<int> validStatus = new List<int>() { 1, 2, 3, 4, 5, 6 };
            return  (validStatus.Any(z => z == status));
        }
    }
}
