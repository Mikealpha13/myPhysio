using MyPhysio.Domain.Enums;
using MyPhysio.v1.Command;
using MyPhysio.v1.Infrastructure.Filters;
using MyPhysio.v1.ViewModels.Request;
using MyPhysioAPI.v1.Command;
using MyPhysioAPI.v1.ViewModels.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyPhysio.Domain.Configuration;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Microsoft.Extensions.Configuration;

namespace MyPhysio.v1.Controllers
{
    /// <summary>
    /// Master API Controller
    /// </summary>
    [ApiController]
    [Route("v{version:apiVersion}/MyPhysio")]
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(ExceptionFilter))]
    [EnableCors("AllowOrigin")]
    [AllowAnonymous]
    public class ServiceController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IOptions<ProductDataSource> _products;
        private readonly IOptions<PaymentMethodsDataSource> _paymentmethod;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="products"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="configuration"></param>
        public ServiceController(IMediator mediator, IOptions<ProductDataSource> products, IOptions<PaymentMethodsDataSource> paymentMethods,IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _products = products ?? throw new ArgumentNullException(nameof(products));
            _paymentmethod = paymentMethods ?? throw new ArgumentNullException(nameof(paymentMethods));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// This endpoint is used to fetch the registered customer profiles
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("GetCustomer/{phoneNumber}")]
        public async Task<IActionResult> CustomerByPhone(string phoneNumber)
        {
            return Ok();
        }

        /// <summary>
        /// This endpoint is used to fetch the existing customer bookings
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerBookings/{phoneNumber}")]
        [ValidateRequest]
        public async Task<IActionResult> CustomerBooking(string phoneNumber)
        {
            return Ok();
        }


        /// <summary>
        /// This endpoint is used to verify the customer by OTP
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerifyCustomer/{phoneNumber}/{code}")]
        [ValidateRequest]
        public async Task<IActionResult> VerifyCustomer(string phoneNumber, string code)
        {
            if (code.Equals("0995") && phoneNumber.Equals("8888888888"))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        /// <summary>
        /// This endpoint is used to sent  the OTP at customer registered mobile number
        /// </summary>
        /// <returns></returns>
        [HttpGet("SendOTP/{phoneNumber}")]
        public async Task<IActionResult> SendOTP(string phoneNumber)
        {

            try
            {
                string accountSid = _configuration.GetSection("OTPService:accountID").Value;
                string authToken = _configuration.GetSection("OTPService:accountToken").Value;

                TwilioClient.Init(accountSid, authToken);

                var smsverification = VerificationResource.Create(
                    to: $"+91{phoneNumber}",
                    channel: "sms",
                    pathServiceSid: _configuration.GetSection("OTPService:serviceID").Value
                );
               // var whatsappVerification = VerificationResource.Create(
               //    to: $"+91{phoneNumber}",
               //    channel: "whatsapp",
               //    pathServiceSid: _configuration.GetSection("OTPService:serviceID").Value
               //);

                return Ok(true);
            }
            catch (Exception)
            {

                return Ok(false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateProfile")]
        [ValidateRequest]
        public async Task<IActionResult> UpdateProfile()
        {
            return Ok();
        }

        /// <summary>
        /// This method is used to fetch the enabled payment methods
        /// </summary>
        /// <returns></returns>
        [HttpGet("PaymentMethods")]
        public async Task<IActionResult> PaymentMethods()
        {
            return Ok(_paymentmethod.Value.paymentMethods.ToList());
        }

        /// <summary>
        /// This method is used to fetch the list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        {

            return Ok(_products.Value.Products.ToList());
        }

        /// <summary>
        /// This method is used to fetch the list of products.
        /// </summary>
        /// <returns></returns>
        [HttpPost("BookTherapy")]
        public async Task<IActionResult> Booking()
        {
            return Ok();
        }

        /// <summary>
        /// This method is used to fetch the list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProductCategories")]
        public async Task<IActionResult> Categories()
        {
            return Ok();
        }
    }
}
