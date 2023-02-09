using MyPhysioAPI.v1.ViewModels.Request;
using MyPhysioAPI.v1.ViewModels.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysioAPI.v1.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerPhoneCommand:IRequest<CustomerPhoneResponseViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly CustomerPhoneRequestViewModel _customerPhoneRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerPhoneRequestViewModel"></param>
        public CustomerPhoneCommand(CustomerPhoneRequestViewModel customerPhoneRequestViewModel)
        {
            _customerPhoneRequest = customerPhoneRequestViewModel ?? throw new ArgumentNullException(nameof(customerPhoneRequestViewModel));
        }
    }
}
