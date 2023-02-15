﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyPhysio.Domain.Configuration;
using MyPhysio.Domain.ServiceModels.Response;
using MyPhysio.v1.Infrastructure.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using MyPhysio.Domain.EntityModels;

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
        private readonly string CosmosDBAccountUri ;
        private readonly string CosmosDBAccountPrimaryKey ;
        private readonly string CosmosDbName ;
        private readonly string CosmosDbContainerName;
   
        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="products"></param>
        /// <param name="paymentMethods"></param>
        /// <param name="configuration"></param>
        public ServiceController(IMediator mediator, IOptions<ProductDataSource> products, IOptions<PaymentMethodsDataSource> paymentMethods, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _products = products ?? throw new ArgumentNullException(nameof(products));
            _paymentmethod = paymentMethods ?? throw new ArgumentNullException(nameof(paymentMethods));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            CosmosDBAccountUri = _configuration.GetSection("CosmosDB:AccountUrl").Value;
            CosmosDBAccountPrimaryKey = _configuration.GetSection("CosmosDB:AuthKey").Value;
            CosmosDbName = _configuration.GetSection("CosmosDB:DatabseId").Value;
            CosmosDbContainerName = _configuration.GetSection("CosmosDB:ContainerName").Value;

        }

        /// <summary>
        /// This endpoint is used to fetch the registered customer profiles
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [HttpGet("GetCustomer/{phoneNumber}")]
        public async Task<IActionResult> CustomerByPhone(string phoneNumber)
        {
            try
            {
                var container = ContainerClient();
                ItemResponse<CustomerDetails> response = await container.ReadItemAsync<CustomerDetails>(phoneNumber, new PartitionKey(phoneNumber));
                return Ok(response.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                string userName = _configuration.GetSection("OTPService:userName").Value;
                string password = _configuration.GetSection("OTPService:password").Value;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    AuthenticationSchemes.Basic.ToString(),
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"))
                    );
                    var pairs = new List<KeyValuePair<string, string>>
                  {
                    new KeyValuePair<string, string>("To",  $"+91{phoneNumber}"),
                    new KeyValuePair<string, string>("Code",  code),
                  };
                    var content = new FormUrlEncodedContent(pairs);
                    var response = await httpClient.PostAsync(_configuration.GetSection("OTPService:validateotpendpoint").Value, content);
                    response.EnsureSuccessStatusCode();
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ValidateOTPServiceResponseModel>(jsonResult);
                    if (
                        result.status.ToLower() == "approved" && 
                        result.valid.ToLower() == "true" && 
                        result.to == ($"+91{phoneNumber}")
                        )
                        return Ok(true);
                    else
                        return Ok(false);
                }
                
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
           
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
                //string userName = _configuration.GetSection("OTPService:userName").Value;
                //string password = _configuration.GetSection("OTPService:password").Value;
                //using (var httpClient = new HttpClient())
                //{
                //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                //    AuthenticationSchemes.Basic.ToString(),
                //    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"))
                //    );
                //  var pairs = new List<KeyValuePair<string, string>>
                //  {
                //    new KeyValuePair<string, string>("To",  $"+91{phoneNumber}"),
                //    new KeyValuePair<string, string>("Channel",  "sms"),
                //  };
                //  var content = new FormUrlEncodedContent(pairs);
                //  var response = await httpClient.PostAsync(_configuration.GetSection("OTPService:baseAddress").Value, content);
                //  response.EnsureSuccessStatusCode();
                //  var result = await response.Content.ReadAsStringAsync();
                //}
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        [HttpPost("UpdateCustomer")]
        [ValidateRequest]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerDetails customerDetails)
        {
            try
            {
                var container = ContainerClient();
                ItemResponse<CustomerDetails> res = await container.ReadItemAsync<CustomerDetails>(customerDetails.id, new PartitionKey(customerDetails.id));
                //Get Existing Item
                var existingItem = res.Resource;
                //Replace existing item values with new values
                existingItem.firstname = customerDetails.firstname;
                existingItem.lastname = customerDetails.lastname;
                existingItem.address = customerDetails.address;
                existingItem.dob = customerDetails.dob;
                existingItem.street = customerDetails.street;
                existingItem.emailid = customerDetails.emailid;
                existingItem.gender = customerDetails.gender;
                existingItem.pincode = customerDetails.pincode;
                var updateRes = await container.ReplaceItemAsync(existingItem, customerDetails.id, new PartitionKey(customerDetails.id));
                return Ok(updateRes.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPost("CreateCustomer")]
        [ValidateRequest]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerDetails customerRequest)
        {
            try
            {
            
                var container = ContainerClient();
                var response = await container.CreateItemAsync(customerRequest, new PartitionKey(customerRequest.id));
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
         
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
