using MyPhysio.v1.ViewModels.Request;
using MyPhysio.v1.ViewModels.Response;
using MediatR;
using MyPhysio.v1.ViewModels.Request;
using MyPhysio.v1.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Command
{

    /// <summary>
    /// tt
    /// </summary>
    public class CustomerDetailCommand:IRequest<CustomerDetailResponseViewModel>
    {

        /// <summary>
        /// 
        /// </summary>
        public readonly CustomerDetailsRequestViewModel _customerRequest;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRequest"></param>
        public CustomerDetailCommand(CustomerDetailsRequestViewModel customerRequest)
        {
            _customerRequest = customerRequest ?? throw new ArgumentNullException(nameof(customerRequest));
        }
    }
}
