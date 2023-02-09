using MyPhysio.Domain.ServiceModels;
using MyPhysio.v1.Contracts;
using MyPhysio.v1.ViewModels.Response;
using MyPhysioAPI.v1.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Processor.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerPhoneResponseProcessor : IResponseProcessor<CustomerPhoneResponseViewModel, CustomerPhoneServiceModelResponse>
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CustomerPhoneResponseViewModel ToViewModel(CustomerPhoneServiceModelResponse request)
        {
            var response = new CustomerPhoneResponseViewModel();
            if(request.success && request.data.customers!=null && request.data.customers.Count > 0 )
            {
                request.data.customers.ForEach(z =>
                {
                    var customerdetailsObject = new MyPhysioAPI.v1.ViewModels.Response.CustomerDetails();
                    customerdetailsObject.name = z.name;
                    customerdetailsObject.address1 = z.address1;
                    customerdetailsObject.address2 = z.address2;
                    customerdetailsObject.state = z.state;
                    customerdetailsObject.city = z.city;
                    customerdetailsObject.zipCode = z.zipCode;
                    customerdetailsObject.emailAddress = z.emailAddress;
                    customerdetailsObject.customerId = z.customerId;
                    z.cellPhones.ForEach(x =>
                    {
                        customerdetailsObject.PhoneNumber.Add(x.number);
                    });
                    response.CustomerDetails.Add(customerdetailsObject);

                });
                                
            }
            return response;
        }
    }
}
